import { ChangeDetectorRef, Component, OnInit, ViewChild } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { MatDialog, MatPaginator, MatSnackBar, MatSort, MatTableDataSource } from '@angular/material';
import { Observable, ReplaySubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { EventDTO, MessageDTO, UploadEventDTO } from 'src/shared/shared.models';
import { SharedService } from 'src/shared/shared.serice';

@Component({
  selector: 'app-calendar-of-events',
  templateUrl: './calendar-of-events.component.html',
  styleUrls: ['./calendar-of-events.component.scss']
})
export class CalendarOfEventsComponent implements OnInit {

  @ViewChild(MatSort, { static: false }) outboxSort: MatSort;
  @ViewChild(MatPaginator, { static: false }) outboxPaginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) inboxSort: MatSort;
  @ViewChild(MatPaginator, { static: false }) inboxPaginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) pendingSort: MatSort;
  @ViewChild(MatPaginator, { static: false }) pendingPaginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sendSort: MatSort;
  @ViewChild(MatPaginator, { static: false }) sendPaginator: MatPaginator;

  inboxData: EventDTO[];
  outboxData: EventDTO[];
  pendingData: EventDTO[];
  inboxDataSource: MatTableDataSource<object> = new MatTableDataSource();
  outboxDataSource: MatTableDataSource<object> = new MatTableDataSource();
  pendingDataSource: MatTableDataSource<object> = new MatTableDataSource();

  displayedColumns = ['Id', 'Title', 'Description', 'CreatedBy', 'MemberNumber', 'StartDate', 'EndDate', 'TypeOfEvent', 'Actions'];

  member: boolean = true;
  management: boolean = false;
  chair: boolean = false;

  sendOptions: SendOptions[] = [
    { value: 'Internation Event', viewValue: 'Internation Event' },
    { value: 'South African Championships', viewValue: 'South African Championships' },
    { value: 'Club and Provincial Leagues', viewValue: 'Club and Provincial Leagues' },
    { value: 'General events', viewValue: 'General events' },
  ];

  messages: MessageDTO[];
  
  theFile: File;
  fileName = '';
  title: string = '';
  description: string = '';
  startDate: Date = new Date();
  endDate: Date = new Date();
  selectedSendOption: string;

  uploadData: UploadEventDTO = {
    eventId: null,
    title: null,
    description: null,
    startDate: null,
    endDate: null,
    TypeOfEvent: null,
  }

  constructor(private snackBar: MatSnackBar, private service: SharedService, private changeDetectorRef: ChangeDetectorRef, public dialog: MatDialog) { }

  ngOnInit() {
    this.changeDetectorRef.detectChanges();
    this.setPrivileges();
    this.setupDataStream();
  }

  setPrivileges() {
    const role = localStorage.getItem('role');
    if (this.contains(role, ['A1', 'A2', 'A3', 'A4', 'A5', 'A7', 'A8', 'A9', 'A10', 'A13',
      'B1', 'B2', 'B3', 'B4', 'B5', 'B7', 'B8', 'B9', 'B10', 'B13',
      'C1', 'C2', 'C3', 'C4', 'C5', 'C7', 'C8', 'C9', 'C10', 'C13',
      'D1', 'D2', 'D3', 'D4', 'D5', 'D7', 'D8', 'D9', 'D10', 'D13',])) {
      this.management = true;
    }
    if (this.contains(role, ['A1', 'B1', 'C1', 'D1'])) {
      this.chair = true;
    }
  }

  setupDataStream() {
    if (this.member === true) {
      this.service.getEventInbox().subscribe(a => {
        this.inboxData = a;
        this.inboxDataSource.data = this.inboxData;
      });
    }
    if (this.management === true) {
      this.service.getEventOutbox().subscribe(a => {
        this.outboxData = a;
        this.outboxDataSource.data = this.outboxData;
      });
    }
    if (this.chair === true) {
      this.service.getEventPending().subscribe(a => {
        this.pendingData = a;
        this.pendingDataSource.data = this.pendingData;
      });
    }
  }

  ngAfterViewInit() {
    this.outboxDataSource.sort = this.outboxSort;
    this.outboxDataSource.paginator = this.outboxPaginator;
    this.inboxDataSource.sort = this.inboxSort;
    this.inboxDataSource.paginator = this.inboxPaginator;
    this.pendingDataSource.sort = this.pendingSort;
    this.pendingDataSource.paginator = this.pendingPaginator;
  }

  applyInboxFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.inboxDataSource.filter = filterValue;
  }

  applyOutboxFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.outboxDataSource.filter = filterValue;
  }

  applyPendingFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.pendingDataSource.filter = filterValue;
  }

  OpenFile(event: any) {
    this.theFile = event.target.files[0];
    this.fileName = event.target.files[0].name;
    this.openSnackBar('File Ready For Processing', 'Close');
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

  public validationMessages = {
    required: 'Please complete required field.',
  };

  openModal(templateRef, card?: any) {
    if (card) {
      this.messages = card;
    }

    let dialogRef = this.dialog.open(templateRef, {
      width: '1500px',
      height: '700px'
    });

    dialogRef.afterClosed().subscribe(result => {
    });
  }

  openDocument(id) {
    window.open(environment.apiUrl + "events/" + id + ".pdf", '_blank');
  }

  contains(str, arr) {
    var value = 0;
    arr.forEach(function (word) {
      value = value + str.includes(word);
    });
    return (value === 1)
  }

  aproveDocument(id) {
    this.service.aprovePendingEvent(id).subscribe(a => {
      this.setupDataStream();
      this.openSnackBar("Document status has been updated", "close");
    });
  }

  declineDocument(id) {
    this.service.declinePendingEvent(id).subscribe(a => {
      this.setupDataStream();
      this.openSnackBar("Document status has been updated", "close");
    });
  }

  sendMessage() {
    this.openSnackBar('This may take a while, Please wait...', 'Close');
    this.uploadData.description = this.description;
    this.uploadData.title = this.title;
    this.uploadData.TypeOfEvent = this.selectedSendOption;
    this.uploadData.startDate = this.startDate;
    this.uploadData.endDate = this.endDate;

    if (this.title === null || this.title === undefined || this.title === "" ||
      this.theFile === null || this.theFile === undefined) {
      this.openSnackBar("Please fill in all the values", "close");
    } else {
      this.service.uploadEvent(this.theFile).subscribe(a => {
        this.uploadData.eventId = a;
        this.service.updateEvent(this.uploadData).subscribe(a => {
          this.openSnackBar('Process Completed', 'Close');
          this.description = "";
          this.title = "";
          this.theFile = null;
          this.dialog.closeAll();
          this.setupDataStream();
        });
      });
    }
  }

  convertFile(file: File): Observable<string> {
    const result = new ReplaySubject<string>(1);
    const reader = new FileReader();
    reader.readAsBinaryString(file);
    reader.onload = (event) => result.next(btoa(reader.result.toString()));
    return result;
  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }

}

interface SendOptions {
  value: string;
  viewValue: string;
}
