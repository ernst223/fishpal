import { ChangeDetectorRef, Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Observable } from 'rxjs';
import { FederationDTO, MessageDTO, ProvinceDTO } from 'src/shared/shared.models';
import { AdministrationService } from '../administration.service';


export interface Card {
  title: string;
  subtitle: string;
  text: string;
}

const DATA: Tile[] = [
  { text: 'One', price: 700000, bed: 2, bath: 1, parking: 2 },
  { text: 'One', price: 700000, bed: 2, bath: 1, parking: 2 }, { text: 'One', price: 700000, bed: 2, bath: 1, parking: 2 },
  { text: 'One', price: 700000, bed: 2, bath: 1, parking: 2 },
  { text: 'One', price: 700000, bed: 2, bath: 1, parking: 2 },
  { text: 'One', price: 700000, bed: 2, bath: 1, parking: 2 },
  { text: 'One', price: 700000, bed: 2, bath: 1, parking: 2 }
];


export interface Tile {
  text: string;
  price: number;
  bed: number;
  bath: number;
  parking: number;
}

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
  messages: MessageDTO[];
  loggedInUserEmail: string;
  constructor(private adminService: AdministrationService, private formBuilder: FormBuilder, private changeDetectorRef: ChangeDetectorRef, public dialog: MatDialog) { }

  ngOnInit() {
    this.changeDetectorRef.detectChanges();
    this.loggedInUserEmail = localStorage.getItem('loggedInUserEmail');
    this.createForm();
    this.getInboxMessages();
    this.getFederations();
  }

  test() {
    console.log("this is the test", this.selectedFederations);
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
    this.adminService.getAllFederations(this.loggedInUserEmail).subscribe(feds => {
      console.log("this is the federations", feds);
      this.allFederations = feds;
      this.getProvinces();
    });
  }

  getProvinces() {
    let myInterfacesArray = this.selectedFederations.map(value => {

      return <any>{
        Id: null,
        Name: value
      };

    });

    console.log("this is the 777 get provinces", myInterfacesArray);

    this.adminService.getAllProvinces(this.loggedInUserEmail, myInterfacesArray).subscribe(feds => {
      console.log("this is the federations", feds);
      this.allProvinces = feds;
    });
  }

  /*getClubs() {
    this.adminService.getAllFederations(this.loggedInUserEmail).subscribe(feds => {
      console.log("this is the federations",feds);
      this.allFederations = feds;
    });
  }

  getMembers() {
    this.adminService.getAllFederations(this.loggedInUserEmail).subscribe(feds => {
      console.log("this is the federations",feds);
      this.allFederations = feds;
    });
  }*/

  sendMessages() {
    if (this.newMessageForm.invalid) {
      this.validateAllFormFields(this.newMessageForm);
      return;
    }

    const formData = this.newMessageForm.getRawValue();
    console.log("order form data test", formData);

    let today: object = new Date();

    //this.myModel.Province = formData.the_Province;
    //this.myModel.photos = this.photosObject;

    /*console.log(this.myModel);
    this.http.post('https://localhost:44351/InsertListing', this.myModel).pipe().subscribe(a => {

    }, (error) => {

    });
    console.log(formData);*/
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
    this.adminService.getAllMessages(0, this.loggedInUserEmail).subscribe(message => {
      console.log("this is the INBOX messages", message);
      this.dataSourceInBox = new MatTableDataSource<MessageDTO>(message);
      this.dataSourceInBox.paginator = this.paginator;
      this.obsInBox = this.dataSourceInBox.connect();

      this.getOutboxMessages();
    });
  }
  getOutboxMessages() {
    this.adminService.getAllMessages(1, this.loggedInUserEmail).subscribe(message => {
      this.dataSourceOutBox = new MatTableDataSource<MessageDTO>(message);
      this.dataSourceOutBox.paginator = this.paginator;
      this.obsOutBox = this.dataSourceOutBox.connect();

      console.log("this is the OUTBOX messages", message);
      this.getRequestMessages();
    });
  }
  getRequestMessages() {
    this.adminService.getAllMessages(2, this.loggedInUserEmail).subscribe(message => {
      this.dataSourcePending = new MatTableDataSource<MessageDTO>(message);
      this.dataSourcePending.paginator = this.paginator;
      this.obsPending = this.dataSourcePending.connect();

      console.log("this is the PENDING messages", message);
    });
  }

  openModal(templateRef, card?: any) {
    if (card) {
      this.messages = card;
    }

    console.log("this is the card", this.messages);

    let dialogRef = this.dialog.open(templateRef, {
      width: '550px',
    });

    dialogRef.afterClosed().subscribe(result => {
    });
  }

  closeModal(templateRef) {
    this.dialog.closeAll();
  }

  onFederationInputChange(event: any) {
    const searchInput = event.target.value.toLowerCase();

    this.filteredFederations = this.allFederations.filter(({ name }) => {
      const fed = name.toLowerCase();
      return fed.includes(searchInput);

    });
  }

  onProvinceInputChange(event: any) {
    const searchInput = event.target.value.toLowerCase();

    this.filteredProvinces = this.allProvinces.filter(({ name }) => {
      const provc = name.toLowerCase();
      return provc.includes(searchInput);
    });
  }

  onClubInputChange(event: any) {
    const searchInput = event.target.value.toLowerCase();

    this.filteredClubs = this.allClubs.filter(({ name }) => {
      const clb = name.toLowerCase();
      return clb.includes(searchInput);
    });
  }

  onFMemberInputChange(event: any) {
    const searchInput = event.target.value.toLowerCase();

    this.filteredMembers = this.allMembers.filter(({ name }) => {
      const mmber = name.toLowerCase();
      return mmber.includes(searchInput);
    });
  }

  onFederationOpenChange(searchInput: any) {
    searchInput.value = "";
    this.filteredFederations = this.allFederations;
  }

  onProvinceOpenChange(searchInput: any) {
    searchInput.value = "";
    this.filteredProvinces = this.allProvinces;
  }

  onClubOpenChange(searchInput: any) {
    searchInput.value = "";
    this.filteredClubs = this.allClubs;
  }

  onMemberOpenChange(searchInput: any) {
    searchInput.value = "";
    this.filteredMembers = this.allMembers;
  }

  sendMessage() {
    console.log("this is the test", this.selectedFederations);
    this.getProvinces();
  }

}
