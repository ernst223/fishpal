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
      let tempSetDate = new Date();
      if (this.personalInformation.dob.toString() === "0001-01-01T00:00:00") {
        this.personalInformation.dob = tempSetDate;
      }
      if (this.personalInformation.passportExpirationDate.toString() === "0001-01-01T00:00:00") {
        this.personalInformation.passportExpirationDate = tempSetDate;
      }
    });
  }

  update() {
    if (this.isValidSAID(this.personalInformation.idNumber)) {
      this.service.updatePersonalInformation(this.personalInformation, Number(this.currentProfile)).subscribe(a => {
        this.setupDataStream();
        this.openSnackBar('Personal Information Updated', 'close');
        if (this.idFile) {
          this.service.uploadIdDocument(this.idFile).subscribe(a => {
          });
        }
        if (this.passportFile) {
          this.service.uploadPassportDocument(this.passportFile).subscribe(a => {
          });
        }
        if (this.skippersLicenseFile) {
          this.service.uploadSkippersDocument(this.skippersLicenseFile).subscribe(a => {
          });
        }
      });
    } else {
      this.openSnackBar('Please supply a valid ID', 'close');
    }
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

  isValidSAID(id) {
    let i: any;
    let c: any;
    let even: any = '';
    let sum: any = 0;
    let check: any = id.slice(-1);

    if (id.length != 13 || id.match(/\D/)) {
        return false;
    }
    id = id.substr(0, id.length - 1);
    for (i = 0; c = id.charAt(i); i += 2) {
        sum += +c;
        even += id.charAt(i + 1);
    }
    even = '' + even * 2;
    for (i = 0; c = even.charAt(i); i++) {
        sum += +c;
    }
    let temp: any = ('' + sum).charAt(1);
    sum = 10 - temp;
    //return ('' + sum).slice(-1) == check;
    return true;
  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }
}
