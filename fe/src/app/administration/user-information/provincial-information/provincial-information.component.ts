import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-provincial-information',
  templateUrl: './provincial-information.component.html',
  styleUrls: ['./provincial-information.component.scss']
})
export class ProvincialInformationComponent implements OnInit {

  constructor() { }
  PharmacyName:any;
  PhysicianContact:any;
  ngOnInit() {
  }

}
