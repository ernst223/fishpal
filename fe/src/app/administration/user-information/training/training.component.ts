import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { TrainingDTO } from 'src/shared/shared.models';
import { SharedService } from 'src/shared/shared.serice';

@Component({
  selector: 'app-training',
  templateUrl: './training.component.html',
  styleUrls: ['./training.component.scss']
})
export class TrainingComponent implements OnInit {

  constructor(private service: SharedService, private snackBar: MatSnackBar) { }

  currentProfile = localStorage.getItem("profileId");
  isLoading: any;

  training: TrainingDTO = {
    id: null,
    managerYearCompleted: null,
    managerPointsReceived: null,
    coachLvl1YearCompleted: null,
    coachLvl1PointsReceived: null,
    coachLvl2YearCompleted: null,
    coachLvl2PointsReceived: null,
    captainYearCompleted: null,
    captainPointsReceived: null,
    adminYearCompleted: null,
    adminPointsReceived: null
  };

  ngOnInit() {
    this.setupDataStream();
  }

  setupDataStream() {
    this.service
      .getTrainingInformation(Number(this.currentProfile))
      .subscribe((a) => {
        this.training = a;
      });
  }

  update() {
    this.service.updateTrainingInformation(
      this.training,
      Number(this.currentProfile)
    ).subscribe((a) => {
      this.setupDataStream();
      this.openSnackBar("Training Information Updated", "close");
    });
  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }

}
