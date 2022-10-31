import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-medical-information',
  templateUrl: './medical-information.component.html',
  styleUrls: ['./medical-information.component.scss']
})
export class MedicalInformationComponent implements OnInit {

  constructor() { }
  emrelationship:any;
  isLoading:any;
  MAName:any;
  MAPlan:any;
  MANumber:any;
  MAContactNumber:any;
  PhysicianName:any;
  PhysicianContact:any;
  PharmacyName:any;
  PharmacyContact:any;
  emname:any;
  conditionName:any;
  conditionMed:any;
  conditionDosage:any;
  ConditionFreq:any;
  

  ngOnInit() {
  }

}
