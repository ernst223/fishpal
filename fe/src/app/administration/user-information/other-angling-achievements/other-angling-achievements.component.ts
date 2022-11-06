import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { OtherAnglingAchievementsDTO } from 'src/shared/shared.models';
import { SharedService } from 'src/shared/shared.serice';

@Component({
  selector: 'app-other-angling-achievements',
  templateUrl: './other-angling-achievements.component.html',
  styleUrls: ['./other-angling-achievements.component.scss']
})
export class OtherAnglingAchievementsComponent implements OnInit {

  currentProfile = localStorage.getItem("profileId");

  otherAnglingAchievements: OtherAnglingAchievementsDTO[] = [];
  selectedAchievement: string;
  selectedYear: string;
  selectedTeam: string;
  selectedTeamMembers: string;
  selectedTournament: string;
  selectedVenue: string;

  constructor(private snackBar: MatSnackBar, private service: SharedService) { }

  ngOnInit() {
    this.setupDataStream();
  }

  setupDataStream() {
    this.service
      .getOtherAnglingAchievements(Number(this.currentProfile))
      .subscribe((a) => {
        this.otherAnglingAchievements = a;
      });
  }

  removeFromList(selectedObject: OtherAnglingAchievementsDTO) {
    const index = this.otherAnglingAchievements.indexOf(selectedObject, 0);
    if (index > -1) {
      this.otherAnglingAchievements.splice(index, 1);
    }
  }

  addToList() {
    this.otherAnglingAchievements.push({
      id: 0,
      achievement: this.selectedAchievement,
      year: this.selectedYear,
      team: this.selectedTeam,
      teamMembers: this.selectedTeamMembers,
      tournament: this.selectedTournament,
      venue: this.selectedVenue,
    });
  }

  update() {
    this.service.updateOtherAnglingAchievements(
      this.otherAnglingAchievements,
      Number(this.currentProfile)
    ).subscribe((a) => {
      this.setupDataStream();
      this.openSnackBar("Other Angling History Updated", "close");
    });
  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }
}
