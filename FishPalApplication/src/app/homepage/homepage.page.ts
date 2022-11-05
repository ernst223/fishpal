import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.page.html',
  styleUrls: ['./homepage.page.scss'],
})
export class HomepagePage implements OnInit {
  mainPage:boolean = true;
  profileCardPage:boolean = false;
  qrScannerPage:boolean = false;

  constructor() { }

  ngOnInit() {
  }

  item = [{
    'name': 'Andre',
    'surname': 'Strauss',
    'facet': 'Artlure',
    'ValidUntil': '7 July 2023'
  }]

  qrInfo = JSON.stringify(this.item);

  backButtonClick() {
    this.mainPage = true;
    this.profileCardPage = false;
    this.qrScannerPage = false; 
  }

  profileCardClick() {
    this.profileCardPage = true;
    this.qrScannerPage = false;
    this.mainPage = false;
  }

  qrScannerClick() {
    this.qrScannerPage = true;
    this.profileCardPage = false;
    this.mainPage = false;
  }

}
