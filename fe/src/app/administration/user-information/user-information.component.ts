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

  ngOnInit() {
  }

   openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }
}
