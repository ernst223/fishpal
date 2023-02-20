import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { ClubDTO, FacetDTO, ProvinceDTO } from 'src/shared/shared.models';
import { SharedService } from 'src/shared/shared.serice';

@Component({
  selector: 'app-reporting',
  templateUrl: './reporting.component.html',
  styleUrls: ['./reporting.component.scss']
})
export class ReportingComponent implements OnInit {

  facets: FacetDTO[];
  provinces: ProvinceDTO[];
  clubs: ClubDTO[];
  selectedProvince: number = 0;
  selectedFacet: number = 0;
  selectedClub: number = 0;
  canExecute = false;

  constructor(private snackBar: MatSnackBar, private service: SharedService) { }

  setupDataStream() {
    this.service.getAllFacets().subscribe(a => {
      this.facets = a;
    });
  }

  ngOnInit() {
    this.setupDataStream();
  }

  facetSelected() {
    this.provinces = this.facets.find(a => Number(a.id) === Number(this.selectedFacet)).provinces;
    this.canExecute = true;
  }

  getFacetClubsByProvince() {
    this.service.getFacetClubsByProvince(this.selectedFacet, this.selectedProvince).subscribe(a => {
      this.clubs = a;
    });
  }

  download() {
    this.openSnackBar('Downloading', 'close');
    this.service.getExportUserInformation(this.selectedFacet, this.selectedProvince , this.selectedClub).subscribe(a => {
      this.service.downloadExcelFile(a, 'userInformation');
      this.openSnackBar('Completed', 'close');
    });
  }

  downloadCSV() {
    this.openSnackBar('Downloading', 'close');
    this.service.getExportUserInformation(this.selectedFacet, this.selectedProvince , this.selectedClub).subscribe(a => {
      this.service.downloadFile(a);
      this.openSnackBar('Completed', 'close');
    });
  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }
}
