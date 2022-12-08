import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { environment } from 'src/environments/environment';
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

  idFile: File;
  passportFile: File;
  skippersLicenseFile: File;

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
      if (this.idFile) {
        this.service.uploadIdDocument(this.idFile).subscribe(a => {
          console.log('Id Document uploaded');
        });
      }
      if (this.passportFile) {
        this.service.uploadPassportDocument(this.passportFile).subscribe(a => {
          console.log('Passport Document uploaded');
        });
      }
      if (this.skippersLicenseFile) {
        this.service.uploadSkippersDocument(this.skippersLicenseFile).subscribe(a => {
          console.log('Skipper License Document uploaded');
        });
      }
    });
  }

  OpenSkippersLicense(event: any) {
    this.skippersLicenseFile = event.target.files[0];
    this.openSnackBar('File Ready For Processing', 'Close');
  }

  OpenPassport(event: any) {
    this.passportFile = event.target.files[0];
    this.openSnackBar('File Ready For Processing', 'Close');
  }

  OpenId(event: any) {
    this.idFile = event.target.files[0];
    this.openSnackBar('File Ready For Processing', 'Close');
  }

  downloadId() {
    window.open(environment.apiUrl + "idDocuments/" + localStorage.getItem('profileId') + ".pdf", '_blank');
  }

  downloadSkippersLicense() {
    window.open(environment.apiUrl + "skippersLicenses/" + localStorage.getItem('profileId') + ".pdf", '_blank');
  }

  downloadPassport() {
    window.open(environment.apiUrl + "passports/" + localStorage.getItem('profileId') + ".pdf", '_blank');
  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }
}
