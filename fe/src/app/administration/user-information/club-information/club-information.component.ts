import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { ClubInformationComitteeMembersDTO, ClubInformationDTO, ClubInformationPriorPeriodsDTO } from 'src/shared/shared.models';
import { SharedService } from 'src/shared/shared.serice';

@Component({
  selector: 'app-club-information',
  templateUrl: './club-information.component.html',
  styleUrls: ['./club-information.component.scss']
})
export class ClubInformationComponent implements OnInit {

  currentProfile = localStorage.getItem("profileId");

  comitteeMembers: ClubInformationComitteeMembersDTO[] = [];
  priorPeriods: ClubInformationPriorPeriodsDTO[] = [];

  clubInformation: ClubInformationDTO = {
    id: null,
    clubName: null,
    clubPeriod: null,
    clubConstitutionRecieved: null,
    clubConstitutionDateAccepted: null,
    clubCodeOfConductRecieved: null,
    clubCodeOfConductDateAccepted: null,
    clubDisciplinaryCodeRecieved: null,
    clubDisciplinaryCodeDateAccepted: null,
    comitteeMembers: [],
    priorPeriods: [],
  };

  selectedPosition: string;
  selectedPeriod: string;

  selectedClubName: string;
  selectedClubPeriod: string;

  constructor(private snackBar: MatSnackBar, private service: SharedService) { }

  ngOnInit() {
    this.setupDataStream();
  }

  setupDataStream() {
    this.service
      .getClubInformation(Number(this.currentProfile))
      .subscribe((a) => {
        this.clubInformation = a;
        this.comitteeMembers = this.clubInformation.comitteeMembers;
        this.priorPeriods = this.clubInformation.priorPeriods;
        let tempSetDate = new Date();
        if (this.clubInformation.clubCodeOfConductDateAccepted.toString() === "0001-01-01T00:00:00") {
          this.clubInformation.clubCodeOfConductDateAccepted = tempSetDate;
        }
        if (this.clubInformation.clubConstitutionDateAccepted.toString() === "0001-01-01T00:00:00") {
          this.clubInformation.clubConstitutionDateAccepted = tempSetDate;
        }
        if (this.clubInformation.clubDisciplinaryCodeDateAccepted.toString() === "0001-01-01T00:00:00") {
          this.clubInformation.clubDisciplinaryCodeDateAccepted = tempSetDate;
        }
      });
  }

  update() {
    this.clubInformation.comitteeMembers = this.comitteeMembers;
    this.clubInformation.priorPeriods = this.priorPeriods;

    this.service.updateClubInformation(
      this.clubInformation,
      Number(this.currentProfile)
    ).subscribe((a) => {
      this.setupDataStream();
      this.openSnackBar("Club Information Updated", "close");
    });
  }

  removeFromCommittee(selectedComittee: ClubInformationComitteeMembersDTO) {
    const index = this.comitteeMembers.indexOf(selectedComittee, 0);
    if (index > -1) {
      this.comitteeMembers.splice(index, 1);
    }
  }

  removeFromPriorPeriods(selectedPriorPeriod: ClubInformationPriorPeriodsDTO) {
    const index = this.priorPeriods.indexOf(selectedPriorPeriod, 0);
    if (index > -1) {
      this.priorPeriods.splice(index, 1);
    }
  }

  addToComittee() {
    this.comitteeMembers.push({
      id: 0,
      position: this.selectedPosition,
      period: this.selectedPeriod
    });
  }

  addToPriorPeriods() {
    this.priorPeriods.push({
      id: 0,
      clubName: this.selectedClubName,
      clubPeriod: this.selectedClubPeriod
    });
  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }
}
