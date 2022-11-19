import { Component, OnInit } from '@angular/core';
import {  MatSnackBar } from '@angular/material';
import { SharedService } from 'src/shared/shared.serice';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  _number:number = 7;
  
  constructor() {};

  ngOnInit() {}

}
