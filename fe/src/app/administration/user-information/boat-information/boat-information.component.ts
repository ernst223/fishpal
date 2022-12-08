import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { environment } from 'src/environments/environment';
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
  cofFile: File;

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
      this.openSnackBar("Boat Information Updated", "close");
      if (this.cofFile) {
        this.service.uploadCOFDocument(this.cofFile).subscribe(a => {
          console.log('COF Document uploaded');
        });
      }
    });
  }

  OpenCOF(event: any) {
    this.cofFile = event.target.files[0];
    this.openSnackBar('File Ready For Processing', 'Close');
  }

  downloadCOFFile() {
    window.open(environment.apiUrl + "cof/" + localStorage.getItem('profileId') + ".pdf", '_blank');
  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }

}
