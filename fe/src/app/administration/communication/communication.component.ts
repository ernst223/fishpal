import { ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatSnackBar } from '@angular/material';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Observable } from 'rxjs';
import { FacetDTO, FederationDTO, MessageDTO, ProvinceDTO } from 'src/shared/shared.models';
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

  allFederations: any[] = [{ fed: "default" }];
  allProvinces: any[] = [{ provc: "default" }];
  allClubs: any[] = [{ clb: "default" }];
  allMembers: any[] = [{ mmber: "default" }];

  filteredFederations: any[] = this.allFederations;
  filteredProvinces: any[] = this.allFederations;
  filteredClubs: any[] = this.allFederations;
  filteredMembers: any[] = this.allFederations;

  selectedFederations: FederationDTO[];
  selectedProvinces: ProvinceDTO[] = [];
  selectedClubs: any[] = [];
  selectedMembers: any[] = [];

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

  constructor(public snackBar: MatSnackBar,private adminService: AdministrationService, private formBuilder: FormBuilder, private changeDetectorRef: ChangeDetectorRef, public dialog: MatDialog) { }

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
        }
        this.getRolesCurrentRoleCanSendTo();
      });
    } else {
      this.facet = undefined;
    }
  }

  getRolesCurrentRoleCanSendTo() {
    this.adminService.getAllRolesCurrentRoleCanSendTo(this.userRole).subscribe(feds => {
      this.roles = feds;
      console.log("this is the returned roles", this.roles);
    });
  }

  setSelectedFacet() {
    this.federationId = this.selectedFacet;
    console.log("this is the slected facet", this.federationId);
  }

  sendMessages() {
    this.message.Message = this.typedMessage;
    this.message.rolesToSendTo = this.selectedRoles;

    this.adminService.sendMessages(this.message, this.federationId, this.profileId).subscribe(result => {
      this.getInboxMessages();
      this.dialog.closeAll();
      this.openSnackBar("Message sent successfully","Close");
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

  openModal(templateRef, card?: any) {
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

}
