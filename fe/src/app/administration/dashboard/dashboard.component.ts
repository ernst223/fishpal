import { Component, OnInit } from '@angular/core';
import { MatSnackBar, ProgressSpinnerMode, ThemePalette } from '@angular/material';
import { SharedService } from 'src/shared/shared.serice';
import { AdministrationService } from '../administration.service';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  loggedInProfileId: number;
  mode: ProgressSpinnerMode = 'determinate';
  _number: number = 7;
  eventsPanelOpenState = false;
  daysBeforePaymentRemaining = 70;
  paymentCircleBackground = 100;

  //document
  documentsExpiryPanelOpenState = false;
  documentAknowledgedPositiveValue: number;
  aknowledged: number = 0;
  notAcknowledged: number = 0;
  totalDocs: number = 0;

  //ccourse enrollment
  coursesEnrolledPositiveValue = 20;
  enrolled: number = 0;
  approvedEnrolled: number = 0;
  unapprovedEnrollments: number = 0;

  constructor(private adminService: AdministrationService) { };

  ngOnInit() {
    this.loggedInProfileId = Number(localStorage.getItem('profileId'));
    this.getDocumentAknowledgementsTrueCount();
    this.getCoursesEnrolledCount();
  }

  getDocumentAknowledgementsTrueCount() {
    this.aknowledged = 0;
    this.adminService.getDocumentAknowledgementsTrueCount(this.loggedInProfileId).subscribe(result => {
      console.log("result", result);
      this.aknowledged = result;
      this.getDocumentAknowledgementsFalseCount();
    });
  }

  getDocumentAknowledgementsFalseCount() {
    this.notAcknowledged = 0;
    this.adminService.getDocumentAknowledgementsFalseCount(this.loggedInProfileId).subscribe(result => {
      console.log("result", result);
      this.notAcknowledged = result;
      this.documentAknowledgedPercentage();
    });
  }

  documentAknowledgedPercentage() {
    this.totalDocs = 0;
    this.totalDocs = this.aknowledged + this.notAcknowledged;
    this.documentAknowledgedPositiveValue = Number(((this.aknowledged / this.totalDocs) * 100).toFixed(2));
  }

  getCoursesEnrolledCount() {
    this.enrolled = 0;
    this.adminService.getEnrolledCoursesCount(this.loggedInProfileId).subscribe(result => {
      this.enrolled = result;
      this.getEnrolledCoursesApprovedCount();
    });
  }

  getEnrolledCoursesApprovedCount() {
    this.approvedEnrolled = 0;
    this.adminService.getEnrolledCoursesApprovedCount(this.loggedInProfileId).subscribe(result => {
      this.approvedEnrolled = result;
      this.courseEnrollmentPercentage();
    });
  }

  courseEnrollmentPercentage() {
    this.unapprovedEnrollments = 0;
    this.unapprovedEnrollments = this.enrolled - this.approvedEnrolled;
    this.coursesEnrolledPositiveValue = Number(((this.approvedEnrolled / this.enrolled) * 100).toFixed(2));
    console.log('testering', this.coursesEnrolledPositiveValue);
  }

}
