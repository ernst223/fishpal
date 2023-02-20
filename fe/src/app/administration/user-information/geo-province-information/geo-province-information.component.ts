import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { GeoProvinceInformationDTO } from 'src/shared/shared.models';
import { SharedService } from 'src/shared/shared.serice';

@Component({
  selector: 'app-geo-province-information',
  templateUrl: './geo-province-information.component.html',
  styleUrls: ['./geo-province-information.component.scss']
})
export class GeoProvinceInformationComponent implements OnInit {

  constructor(private service: SharedService,private snackBar: MatSnackBar) { }
  currentProfile = localStorage.getItem("profileId");
  isLoading:any;

  geoProvinceInformation: GeoProvinceInformationDTO = {
    id: null,
    geoProvince: null,
    provincialSasaccManagement: null,
    position: null,
    districtMunicipality: null,
  };
  ngOnInit() {
    this.setupDataStream();
  }

  setupDataStream() {
    this.service
      .getGeoProvinceInformation(Number(this.currentProfile))
      .subscribe((a) => {
        this.geoProvinceInformation = a;
        console.log(a);
      });
  }

  update() {
    this.service.updateGeoProvinceInformation(
      this.geoProvinceInformation,
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
