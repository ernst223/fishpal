import { Component, OnInit } from '@angular/core';
import {  MatSnackBar } from '@angular/material';
import { SharedService } from 'src/shared/shared.serice';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  constructor(private snackBar: MatSnackBar, private service: SharedService) { }

  ngOnInit() {
  }

   openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }
}
