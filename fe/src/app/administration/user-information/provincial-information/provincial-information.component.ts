import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { ProvincialInformationComteeMembersDTO, ProvincialInformationDTO, ProvincialInformationPriorPeriodsDTO } from 'src/shared/shared.models';
import { SharedService } from 'src/shared/shared.serice';

@Component({
  selector: 'app-provincial-information',
  templateUrl: './provincial-information.component.html',
  styleUrls: ['./provincial-information.component.scss']
})
export class ProvincialInformationComponent implements OnInit {

  currentProfile = localStorage.getItem("profileId");

  comitteeMembers: ProvincialInformationComteeMembersDTO[] = [];
  priorPeriods: ProvincialInformationPriorPeriodsDTO[] = [];

  provincialInformation: ProvincialInformationDTO = {
    id: null,
    provinceName: null,
    provincePeriod: null,
    provinceConstitutionRecieved: null,
    provinceConstitutionDate: null,
    provinceCodeOfCoductRecieved: null,
    provinceCodeOfCoductDate: null,
    provinceDressCodeRecieved: null,
    provinceDressCodeDate: null,
    provinceDisciplinaryCodeRecieved: null,
    provinceDisciplinaryCodeDate: null,
    comitteeMembers: [],
    priorPeriods: [],
  };

  selectedComitteeMembersPosition: string;
  selectedComitteeMembersPeriod: string;

  selectedPriorPeriodsName: string;
  selectedpriorPeriodsPeriod: string;

  constructor(private snackBar: MatSnackBar, private service: SharedService) { }

  ngOnInit() {
    this.setupDataStream();
  }

  setupDataStream() {
    this.service
      .getProvincialInformation(Number(this.currentProfile))
      .subscribe((a) => {
        this.provincialInformation = a;
        this.comitteeMembers = this.provincialInformation.comitteeMembers;
        this.priorPeriods = this.provincialInformation.priorPeriods;
        let tempSetDate = new Date();
        if (this.provincialInformation.provinceCodeOfCoductDate.toString() === "0001-01-01T00:00:00") {
          this.provincialInformation.provinceCodeOfCoductDate = tempSetDate;
        }
        if (this.provincialInformation.provinceConstitutionDate.toString() === "0001-01-01T00:00:00") {
          this.provincialInformation.provinceConstitutionDate = tempSetDate;
        }
        if (this.provincialInformation.provinceDisciplinaryCodeDate.toString() === "0001-01-01T00:00:00") {
          this.provincialInformation.provinceDisciplinaryCodeDate = tempSetDate;
        }
        if (this.provincialInformation.provinceDressCodeDate.toString() === "0001-01-01T00:00:00") {
          this.provincialInformation.provinceDressCodeDate = tempSetDate;
        }
      });
  }

  update() {
    this.provincialInformation.comitteeMembers = this.comitteeMembers;
    this.provincialInformation.priorPeriods = this.priorPeriods;

    this.service.updateProvincialInformation(
      this.provincialInformation,
      Number(this.currentProfile)
    ).subscribe((a) => {
      this.setupDataStream();
      this.openSnackBar("Provincial Information Updated", "close");
    });
  }

  removeFromCommittee(selectedComittee: ProvincialInformationComteeMembersDTO) {
    const index = this.comitteeMembers.indexOf(selectedComittee, 0);
    if (index > -1) {
      this.comitteeMembers.splice(index, 1);
    }
  }

  removeFromPriorPeriods(selectedPriorPeriod: ProvincialInformationPriorPeriodsDTO) {
    const index = this.priorPeriods.indexOf(selectedPriorPeriod, 0);
    if (index > -1) {
      this.priorPeriods.splice(index, 1);
    }
  }

  addToComittee() {
    this.comitteeMembers.push({
      id: 0,
      position: this.selectedComitteeMembersPosition,
      period: this.selectedComitteeMembersPeriod
    });
  }

  addToPriorPeriods() {
    this.priorPeriods.push({
      id: 0,
      name: this.selectedPriorPeriodsName,
      period: this.selectedpriorPeriodsPeriod
    });
  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }
}
