import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { AnglishAdministrationHistoryDTO } from 'src/shared/shared.models';
import { SharedService } from 'src/shared/shared.serice';

@Component({
  selector: 'app-angling-administration-history',
  templateUrl: './angling-administration-history.component.html',
  styleUrls: ['./angling-administration-history.component.scss']
})
export class AnglingAdministrationHistoryComponent implements OnInit {

  currentProfile = localStorage.getItem("profileId");

  anglishAdministrationHistory: AnglishAdministrationHistoryDTO[] = [];
  selectedCapacity: string;
  selectedYear: string;

  constructor(private snackBar: MatSnackBar, private service: SharedService) { }

  ngOnInit() {
    this.setupDataStream();
  }

  setupDataStream() {
    this.service
      .getAnglishAdministrationHistory(Number(this.currentProfile))
      .subscribe((a) => {
        this.anglishAdministrationHistory = a;
      });
  }

  removeFromList(selectedObject: AnglishAdministrationHistoryDTO) {
    const index = this.anglishAdministrationHistory.indexOf(selectedObject, 0);
    if (index > -1) {
      this.anglishAdministrationHistory.splice(index, 1);
    }
  }

  addToList() {
    this.anglishAdministrationHistory.push({
      id: 0,
      capacity: this.selectedCapacity,
      year: this.selectedYear,
    });
  }

  update() {
    this.service.updateAnglishAdministrationHistory(
      this.anglishAdministrationHistory,
      Number(this.currentProfile)
    ).subscribe((a) => {
      this.setupDataStream();
      this.openSnackBar("Angling Administration History Updated", "close");
    });
  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }
}
