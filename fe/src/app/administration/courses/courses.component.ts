import { Component, OnInit } from '@angular/core';
import { MatSnackBar, MatSort } from '@angular/material';
import { SharedService } from 'src/shared/shared.serice';
import { ChangeDetectorRef, ViewChild } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { Observable, ReplaySubject, Subscriber } from 'rxjs';
import { Courses, MessageDTO, MyDocumentMessages, RoleManagementUsersDTO, UpdateCourse, UploadDocumentMessage, UserCoursesDTO } from 'src/shared/shared.models';
import { AdministrationService } from '../administration.service';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-courses',
  templateUrl: './courses.component.html',
  styleUrls: ['./courses.component.scss']
})
export class CoursesComponent implements OnInit {

  @ViewChild(MatSort, { static: false }) outboxSort: MatSort;
  @ViewChild(MatPaginator, { static: false }) outboxPaginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) inboxSort: MatSort;
  @ViewChild(MatPaginator, { static: false }) inboxPaginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) pendingSort: MatSort;
  @ViewChild(MatPaginator, { static: false }) pendingPaginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sendSort: MatSort;
  @ViewChild(MatPaginator, { static: false }) sendPaginator: MatPaginator;

  sendData: RoleManagementUsersDTO[];

  myCourses: Courses[];
  allCourses: Courses[];
  pendingData: Courses[];
  courseEnrolments: UserCoursesDTO[];
  myCoursesDataSource: MatTableDataSource<object> = new MatTableDataSource();
  allCoursesDataSource: MatTableDataSource<object> = new MatTableDataSource();
  pendingDataSource: MatTableDataSource<object> = new MatTableDataSource();
  courseEnrolmentsDataSource: MatTableDataSource<object> = new MatTableDataSource();
  sendDataSource: MatTableDataSource<object> = new MatTableDataSource();
  displayedSendColumns = ['Id', 'Username', 'FullName', 'Facet', 'Role', 'Actions'];
  displayedColumns = ['Id', 'SendFrom', 'Title', 'Note', 'Actions'];
  displayedMyCourses = ['Id', 'Name', 'Description', 'Actions'];
  displayedMyCoursesWithUrl = ['Id', 'Name', 'Description', 'URL', 'Actions'];
  displayedPendingEnrolments = ['Id', 'userName', 'userEmail', 'memberNumber', 'courseName', 'Actions']

  sendOptions: SendOptions[] = [
    { value: 0, viewValue: 'Send to all in same role' },
    { value: 1, viewValue: 'Send to all in lower roles' }
  ];

  member: boolean = true;
  management: boolean = false;
  chair: boolean = false;

  selectedProfiles: number[] = [];

  selectedSendOption: number;
  theFile: File;
  fileName = '';
  title: string;
  note: string;
  url: string;
  uploadData: UpdateCourse = {
    id: null,
    name: null,
    description: null,
    url: null,
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

  applySendFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.pendingDataSource.filter = filterValue;
  }

  contains(str, arr) {
    var value = 0;
    arr.forEach(function (word) {
      value = value + str.includes(word);
    });
    return (value === 1)
  }

  setupDataStream() {
    if (this.member === true) {
      this.service.getMyCourses().subscribe(a => {
        this.myCourses = a;
        this.myCoursesDataSource.data = this.myCourses;
      });
    }
    if (this.management === true) {
      this.service.getApprovedCourses().subscribe(a => {
        this.allCourses = a;
        this.allCoursesDataSource.data = this.allCourses;
      });
    }
    if (this.chair === true) {
      this.service.getUnApprovedCourses().subscribe(a => {
        this.pendingData = a;
        this.pendingDataSource.data = this.pendingData;
      });
      this.service.getPendingEnrollments().subscribe(a => {
        this.courseEnrolments = a;
        this.courseEnrolmentsDataSource.data = this.courseEnrolments;
      });
    }

    this.service.getAccessableUsersToMessage().subscribe(a => {
      this.sendData = a;
      this.sendDataSource.data = this.sendData;
    });
  }

  openDocument(id) {
    window.open(environment.apiUrl + "courses/" + id + ".pdf", '_blank');
  }

  selectProfile(id: any) {

    let index = this.selectedProfiles.indexOf(id, 0);
    if (index > -1) {
      this.selectedProfiles.splice(index, 1);
    } else {
      this.selectedProfiles.push(id);
    }
  }

  ngAfterViewInit() {
    this.allCoursesDataSource.sort = this.outboxSort;
    this.allCoursesDataSource.paginator = this.outboxPaginator;
    this.myCoursesDataSource.sort = this.inboxSort;
    this.myCoursesDataSource.paginator = this.inboxPaginator;
    this.pendingDataSource.sort = this.pendingSort;
    this.pendingDataSource.paginator = this.pendingPaginator;
    this.sendDataSource.sort = this.sendSort;
    this.sendDataSource.paginator = this.sendPaginator;
  }

  applyInboxFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.myCoursesDataSource.filter = filterValue;
  }

  applyOutboxFilter(filterValue: string) {
    filterValue = filterValue.trim();
    filterValue = filterValue.toLowerCase();
    this.allCoursesDataSource.filter = filterValue;
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

  aproveDocument(id) {
    this.service.approveCourse(id).subscribe(a => {
      this.setupDataStream();
      this.openSnackBar("Course status has been updated", "close");
    });
  }

  declineDocument(id) {
    this.service.declineCourse(id).subscribe(a => {
      this.setupDataStream();
      this.openSnackBar("Course status has been updated", "close");
    });
  }

  aproveEnrolment(id) {
    this.service.approveCourseEnrollment(id).subscribe(a => {
      this.setupDataStream();
      this.openSnackBar("Enrolment status has been updated", "close");
    });
  }

  declineEnrolment(id) {
    this.service.declineCourseEnrollment(id).subscribe(a => {
      this.setupDataStream();
      this.openSnackBar("Enrolment status has been updated", "close");
    });
  }

  enrollForCourse(id) {
    this.service.enrollForCourse(id).subscribe(a => {
      this.setupDataStream();
      this.openSnackBar("Enrolment Successful. Please wait for Approval.", "close");
    });
  }

  sendMessage() {
    this.openSnackBar('This may take a while, Please wait...', 'Close');
    this.uploadData.name = this.title;
    this.uploadData.description = this.note;
    this.uploadData.url = this.url;

    if (this.title === null || this.title === undefined || this.title === "" ||
      this.theFile === null || this.theFile === undefined) {
      this.openSnackBar("Please fill in all the values", "close");
    } else {
      this.service.uploadCourse(this.theFile).subscribe(a => {
        this.uploadData.id = a;
        this.service.updateCourse(this.uploadData).subscribe(a => {
          this.openSnackBar('Process Completed', 'Close');
          this.note = "";
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

