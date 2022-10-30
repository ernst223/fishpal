import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { PersonalInformationDTO } from 'src/shared/shared.models';
import { SharedService } from 'src/shared/shared.serice';

@Component({
  selector: 'app-personal-information',
  templateUrl: './personal-information.component.html',
  styleUrls: ['./personal-information.component.scss']
})
export class PersonalInformationComponent implements OnInit {

  currentProfile = localStorage.getItem('profileId');
  emailAddress = localStorage.getItem('loggedInUserEmail');
  personalInformation: PersonalInformationDTO = {
    name: null,
    nickName: null,
    surname: null,
    idNumber: null,
    passportNumber: null,
    dob: null,
    nationality: null,
    ethnicGroup: null,
    homeAddress: null,
    postalAddress: null,
    gender: null,
    passportExpirationDate: null,
    phone: null,
    cell: null,
    skipperLicenseNumber: null
  };
  constructor(private snackBar: MatSnackBar, private service: SharedService) { }

  ngOnInit() {
    this.setupDataStream();
  }

  setupDataStream() {
    this.service.getPersonalInformation(Number(this.currentProfile)).subscribe(a => {
      this.personalInformation = a;
    });
  }

  update() {
    this.service.updatePersonalInformation(this.personalInformation, Number(this.currentProfile)).subscribe(a => {
      this.setupDataStream();
      this.openSnackBar('Personal Information Updated', 'close');
    });
  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }
}
