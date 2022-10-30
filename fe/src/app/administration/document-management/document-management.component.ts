import { Component, OnInit } from '@angular/core';
import { MatSnackBar, MatSort } from '@angular/material';
import { SharedService } from 'src/shared/shared.serice';
import { ChangeDetectorRef, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Observable, ReplaySubject, Subscriber } from 'rxjs';
import { MessageDTO, MyDocumentMessages, UploadDocumentMessage } from 'src/shared/shared.models';
import { AdministrationService } from '../administration.service';
import { environment } from 'src/environments/environment';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';

@Component({
  selector: 'app-document-management',
  templateUrl: './document-management.component.html',
  styleUrls: ['./document-management.component.scss']
})
export class DocumentManagementComponent implements OnInit {

  @ViewChild(MatSort, { static: false }) outboxSort: MatSort;
  @ViewChild(MatPaginator, { static: false }) outboxPaginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) inboxSort: MatSort;
  @ViewChild(MatPaginator, { static: false }) inboxPaginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) pendingSort: MatSort;
  @ViewChild(MatPaginator, { static: false }) pendingPaginator: MatPaginator;

  inboxData: MyDocumentMessages[];
  outboxData: MyDocumentMessages[];
  pendingData: MyDocumentMessages[];
  inboxDataSource: MatTableDataSource<object> = new MatTableDataSource();
  outboxDataSource: MatTableDataSource<object> = new MatTableDataSource();
  pendingDataSource: MatTableDataSource<object> = new MatTableDataSource();
  displayedColumns = ['Id', 'SendFrom', 'Title', 'Note', 'Actions'];

  sendOptions: SendOptions[] = [
    { value: 0, viewValue: 'Send to all in same role' },
    { value: 1, viewValue: 'Send to all in lower roles' }
  ];

  member: boolean = true;
  management: boolean = false;
  chair: boolean = false;

  selectedSendOption: number;
  theFile: File;
  fileName = '';
  title: string;
  note: string
  uploadData: UploadDocumentMessage = {
    note: null,
    title: null,
    documentId: null
  }

  messages: MessageDTO[];

  constructor(private snackBar: MatSnackBar, private service: SharedService, private adminService: AdministrationService,
    private formBuilder: FormBuilder, private changeDetectorRef: ChangeDetectorRef, public dialog: MatDialog) { }

  ngOnInit() {
    this.changeDetectorRef.detectChanges();
    this.setPrivileges();
    this.setupDataStream();
  }

  setPrivileges() {
    const role = localStorage.getItem('role');
    if(this.contains(role, ['A0', 'A1', 'B0', 'B1', 'C0', 'C1', 'D0', 'D1'])) {
      this.management = true;
    }
    if(this.contains(role, ['A1', 'B1', 'C1','D1'])) {
      this.chair = true;
    }
  }

  contains(str, arr){
    var value = 0;
    arr.forEach(function(word){
      value = value + str.includes(word);
    });
    return (value === 1)
  }

  setupDataStream() {
    if (this.member === true) {
      this.service.getDocumentInboxMessages().subscribe(a => {
        this.inboxData = a;
        this.inboxDataSource.data = this.inboxData;
      });
    }
    if (this.management === true) {
      this.service.getDocumentOutboxMessages().subscribe(a => {
        this.outboxData = a;
        this.outboxDataSource.data = this.outboxData;
      });
    }
    if (this.chair === true) {
      this.service.getPendingDocumentMessages().subscribe(a => {
        this.pendingData = a;
        this.pendingDataSource.data = this.pendingData;
      });
    }
  }

  openDocument(id) {
    window.open(environment.apiUrl + "documents/" + id + ".pdf", '_blank');
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
      width: '550px',
    });

    dialogRef.afterClosed().subscribe(result => {
    });
  }

  aproveDocument(id) {
    this.service.aprovePendingDocumentMessage(id).subscribe(a => {
      this.setupDataStream();
      this.openSnackBar("Document status has been updated", "close");
    });
  }

  declineDocument(id) {
    this.service.declinePendingDocumentMessage(id).subscribe(a => {
      this.setupDataStream();
      this.openSnackBar("Document status has been updated", "close");
    });
  }

  sendMessage() {
    this.openSnackBar('This may take a while, Please wait...', 'Close');
    this.uploadData.note = this.note;
    this.uploadData.title = this.title;

    if (this.title === null || this.title === undefined || this.title === "" ||
      this.selectedSendOption === null || this.selectedSendOption === undefined ||
      this.theFile === null || this.theFile === undefined) {
      this.openSnackBar("Please fill in all the values", "close");
    } else {
      this.service.uploadDocumentMessage(this.theFile, this.selectedSendOption).subscribe(a => {
        console.log(a);
        this.uploadData.documentId = a;
        this.service.updateDocumentMessage(this.uploadData).subscribe(a => {
          this.openSnackBar('Process Completed', 'Close');
          this.note = "";
          this.title = "";
          this.theFile = null;
          this.dialog.closeAll();
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
  value: number;
  viewValue: string;
}

export interface Card {
  title: string;
  subtitle: string;
  text: string;
}

export interface Tile {
  text: string;
  price: number;
  bed: number;
  bath: number;
  parking: number;
}

