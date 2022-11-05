import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { BoatInformationDTO } from 'src/shared/shared.models';
import { SharedService } from 'src/shared/shared.serice';

@Component({
  selector: 'app-boat-information',
  templateUrl: './boat-information.component.html',
  styleUrls: ['./boat-information.component.scss']
})
export class BoatInformationComponent implements OnInit {

  constructor(private service: SharedService, private snackBar: MatSnackBar) { }

  currentProfile = localStorage.getItem("profileId");
  isLoading: any;

  boatInformation: BoatInformationDTO = {
    id: null,
    boatOwner: null,
    boatNumber: null,
    hullType: null,
    hullColour: null,
    motorMake: null,
    horsePower: null,
    towVehicleRegistrationNumber: null,
    trailerRegistrationNumber: null,
    cofNumber: null,
    cofExpiryDate: null
  };

  ngOnInit() {
    this.setupDataStream();
  }

  setupDataStream() {
    this.service
      .getBoatInformation(Number(this.currentProfile))
      .subscribe((a) => {
        this.boatInformation = a;
        console.log("returned data", this.boatInformation);
      });
  }

  update() {
    console.log("this is the test", this.boatInformation);
    this.service.updateBoatInformation(
      this.boatInformation,
      Number(this.currentProfile)
    ).subscribe((a) => {
      this.setupDataStream();
      this.openSnackBar("Geo Province Information Updated", "close");
    });
  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }

}
