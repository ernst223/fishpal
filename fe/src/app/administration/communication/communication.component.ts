import { ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatSnackBar } from '@angular/material';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Observable } from 'rxjs';
import { ClubDTO, FacetDTO, FederationDTO, MessageDTO, ProvinceDTO } from 'src/shared/shared.models';
import { AdministrationService } from '../administration.service';

@Component({
  selector: 'app-communication',
  templateUrl: './communication.component.html',
  styleUrls: ['./communication.component.scss']
})
export class CommunicationComponent implements OnInit {
  @ViewChild(MatPaginator, { static: false })

  paginator!: MatPaginator;
  obsInBox!: Observable<any>;
  obsOutBox!: Observable<any>;
  obsPending!: Observable<any>;

  dataSourceInBox!: MatTableDataSource<MessageDTO>;
  dataSourceOutBox!: MatTableDataSource<MessageDTO>;
  dataSourcePending!: MatTableDataSource<MessageDTO>;

  member: boolean = true;
  management: boolean = true;
  chair: boolean = true;

  newMessageForm!: FormGroup;
  feders = new FormControl();
  Provns = new FormControl();
  Clbs = new FormControl();
  membrs = new FormControl();

  selectedFederations: FederationDTO[];
  selectedProvinces: ProvinceDTO[] = [];
  selectedClubs: any[] = [];
  selectedMembers: any[] = [];
  provincesForFederation:ProvinceDTO[] = [];
  clubsForSelectedprovince:ClubDTO[] = [];

  federations: FederationDTO[];
  message: MessageDTO = {} as any;
  userRole: string;
  userEmail: string;
  selectedFederation: string;
  facet: FacetDTO[] = [];
  roles: string[] = [];
  selectedFacet: any;
  selectedRoles: any;
  federationId: number;
  profileId: number;
  typedMessage: string;
  newRoles = {} as any;
  rolesToDisplay = [];
  sendEmail: boolean = false;
  selectedProvince:number[];

  selectedProvincesForClubs:ProvinceDTO = {
    id:null,
    name:null,
    selectedProvinceIds:[]
  };

  constructor(public snackBar: MatSnackBar, private adminService: AdministrationService, private formBuilder: FormBuilder, private changeDetectorRef: ChangeDetectorRef, public dialog: MatDialog) { }

  ngOnInit() {
    this.changeDetectorRef.detectChanges();
    this.userRole = localStorage.getItem('role');
    this.testWhichTypeOfUserIsLoggedIn(this.userRole);
    this.userEmail = localStorage.getItem('loggedInUserEmail');
    this.profileId = Number(localStorage.getItem('profileId'));
    this.createForm();
    this.getInboxMessages();
    this.getFederations();
  }

  testWhichTypeOfUserIsLoggedIn(theUserRole: string) {
    if (theUserRole == "A1" || theUserRole == "B1" || theUserRole == "C1" || theUserRole == "D1") {
      this.chair = true;
      this.member = true;
      this.management = true;
    } else if (theUserRole == "A0" || theUserRole == "B0" || theUserRole == "C0" || theUserRole == "D0") {
      this.chair = false;
      this.member = true;
      this.management = true;
    } else {
      this.chair = false;
      this.member = true;
      this.management = false;
    }
  }

  createForm(): void {
    this.newMessageForm = this.formBuilder.group({
      the_Federation: ['', Validators.required],
      the_Province: ['', Validators.required],
      the_Club: ['', Validators.required],
      the_Member: ['', Validators.required],
      the_Message: ['', Validators.required]
    });
  }

  getFederations() {
    if (this.userRole != "A2" && this.userRole != "A3") {
      this.adminService.getAllFederations(this.userRole).subscribe(feds => {
        this.facet = feds;
        console.log("this is the returned facets", this.facet);
        if (this.facet == null) {
          this.federationId = Number(localStorage.getItem('federationId'));
          this.getAllProvincesForSelectedFederation();
        }
        this.getRolesCurrentRoleCanSendTo();
      });
    } else {
      this.facet = undefined;
    }
  }

  setSelectedProvince(){
    console.log("this is the selected province",this.selectedProvince);
    this.getAllClubsForSelectedProvinces();
  }

  getAllProvincesForSelectedFederation() {
     this.adminService.getAllProvincesForSelectedFederation(this.userRole, this.federationId).subscribe(provs => {
      this.provincesForFederation = provs;
      console.log("this is the returned provinces", this.provincesForFederation);
    });
  }

  getAllClubsForSelectedProvinces() {
    this.selectedProvincesForClubs.selectedProvinceIds = this.selectedProvince;

     this.adminService.getAllClubsForSelectedProvinces(this.selectedProvincesForClubs).subscribe(provs => {
      this.clubsForSelectedprovince = provs;
      console.log("this is the returned clubs", this.provincesForFederation);
    });
  }

  getRolesCurrentRoleCanSendTo() {

    this.adminService.getAllRolesCurrentRoleCanSendTo(this.userRole).subscribe(feds => {
      this.roles = feds;
      this.roles.forEach(value => {
        var _displayValue = this.getNewDisplayValue(value);

        this.newRoles = {
          theValue: value,
          displayValue: _displayValue
        }
        this.rolesToDisplay.push(this.newRoles);
      });

    });
  }

  setSelectedFacet() {
    this.federationId = this.selectedFacet;
    this.getAllProvincesForSelectedFederation();
  }
  
  sendMessages() {
    this.message.Message = this.typedMessage;
    this.message.rolesToSendTo = this.selectedRoles;

    this.adminService.sendMessages(this.message, this.federationId, this.profileId, this.sendEmail).subscribe(result => {
      this.getInboxMessages();
      this.dialog.closeAll();
      this.openSnackBar("Message sent successfully", "Close");
      console.log("this is the result from sending message", result);
    });
  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }

  validateAllFormFields(formGroup: FormGroup) {
    Object.keys(formGroup.controls).forEach(field => {
      const control = formGroup.get(field);
      if (control instanceof FormControl) {
        control.markAsTouched({ onlySelf: true });
      } else if (control instanceof FormGroup) {
        this.validateAllFormFields(control);
      }
    });
  }

  componentIsInvalid(control: string): boolean {
    return (this.newMessageForm.get(control)!.touched || this.newMessageForm.get(control)!.dirty) && !this.newMessageForm.get(control)!.valid;
  }

  public validationMessages = {
    required: 'Please complete required field.',
  };

  getInboxMessages() {
    this.adminService.getAllMessages(0, this.profileId).subscribe(message => {
      console.log("this is the INBOX messages", message);
      this.dataSourceInBox = new MatTableDataSource<MessageDTO>(message);
      this.dataSourceInBox.paginator = this.paginator;
      this.obsInBox = this.dataSourceInBox.connect();
      this.getOutboxMessages();
    });
  }
  getOutboxMessages() {
    this.adminService.getAllMessages(1, this.profileId).subscribe(message => {
      console.log("this is the OUTBOX messages", message);
      this.dataSourceOutBox = new MatTableDataSource<MessageDTO>(message);
      this.dataSourceOutBox.paginator = this.paginator;
      this.obsOutBox = this.dataSourceOutBox.connect();
      this.getRequestMessages();
    });
  }
  getRequestMessages() {
    this.adminService.getAllMessages(2, this.profileId).subscribe(message => {
      console.log("this is the PENDING messages", message);
      this.dataSourcePending = new MatTableDataSource<MessageDTO>(message);
      this.dataSourcePending.paginator = this.paginator;
      this.obsPending = this.dataSourcePending.connect();
    });
  }

  clearMessageForm() {
    this.selectedFacet = undefined;
    this.selectedRoles = undefined;
    this.typedMessage = undefined;
  }

  openModal(templateRef, card?: any) {
    this.clearMessageForm();
    console.log("this is the card", card);
    if (card != undefined) {
      this.message = card;
    }

    let dialogRef = this.dialog.open(templateRef, {
      width: '550px',
    });

    dialogRef.afterClosed().subscribe(result => {
    });
  }

  closeModal(templateRef) {
    this.dialog.closeAll();
  }

  approveDeclineMessage(approveDecline: number, messageId: number) {
    this.adminService.approveDeclineMessage(approveDecline, messageId).subscribe(response => {
      this.getInboxMessages();
      this.dialog.closeAll();
    });
  }

  deleteMessage(messageId: number) {
    this.adminService.deleteMessage(messageId).subscribe(response => {
      this.getInboxMessages();
      this.dialog.closeAll();
    });
  }

  getNewDisplayValue(providedRole: string) {
    if (providedRole == "A0") {
      return "SASACC Admin (A0)";
    } else if (providedRole == "A1") {
      return "SASACC Chair (A1)";
    } else if (providedRole == "A2") {
      return "SASACC Action com (A2)";
    } else if (providedRole == "A3") {
      return "SASACC Management com (A3)";
    } else if (providedRole == "B0") {
      return "Federation Admin (B0)";
    } else if (providedRole == "B1") {
      return "Federation Chair (B1)";
    } else if (providedRole == "B2") {
      return "Federation Action com (B2)";
    } else if (providedRole == "B3") {
      return "Federation Management com (B3)";
    } else if (providedRole == "C0") {
      return "Province Admin (C0)";
    } else if (providedRole == "C1") {
      return "Province Chair (C1)";
    } else if (providedRole == "C2") {
      return "Province Action com (C2)";
    } else if (providedRole == "C3") {
      return "Province Management com (C3)";
    } else if (providedRole == "D0") {
      return "Club Admin (D0)";
    } else if (providedRole == "D1") {
      return "Club Chair (D1)";
    } else if (providedRole == "D2") {
      return "Club Action com (D2)";
    } else if (providedRole == "D3") {
      return "Club Management com (D3)";
    } else if (providedRole == "E0") {
      return "Club member (E0)";
    } else if (providedRole == "E1") {
      return "Social Member (E1)";
    }
  }

}
