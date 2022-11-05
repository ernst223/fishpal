import { Component, OnInit } from '@angular/core';
import {  MatSnackBar } from '@angular/material';
import { SharedService } from 'src/shared/shared.serice';

@Component({
  selector: 'app-user-information',
  templateUrl: './user-information.component.html',
  styleUrls: ['./user-information.component.scss']
})
export class UserInformationComponent implements OnInit {

  constructor(private snackBar: MatSnackBar, private service: SharedService) { }

  selectedPage: string;
  userInfoPages: UserInfoPage[] = [
    {value: 'app-personal-information', viewValue: 'Personal Information'},
    {value: 'app-medical-information', viewValue: 'Medical Information'},
    {value: 'app-club-information', viewValue: 'Club Information'},
    {value: 'app-provincial-information', viewValue: 'Provincial Information'},
    {value: 'app-geo-province-information', viewValue: 'GEO Province Information'},
    {value: 'app-training', viewValue: 'Training'},
    {value: 'app-boat-information', viewValue: 'Boat Information'},
    {value: 'app-angling-history', viewValue: 'Angling History'},
    {value: 'app-angling-administration-history', viewValue: 'Angling Administration History'},
    {value: 'app-other-angling-achievements', viewValue: 'Other Angling Achievements'}
  ];

  ngOnInit() {
  }

   openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }
}

interface UserInfoPage {
  value: string;
  viewValue: string;
}
