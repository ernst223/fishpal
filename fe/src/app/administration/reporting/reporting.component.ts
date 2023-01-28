import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { SharedService } from 'src/shared/shared.serice';

@Component({
  selector: 'app-reporting',
  templateUrl: './reporting.component.html',
  styleUrls: ['./reporting.component.scss']
})
export class ReportingComponent implements OnInit {

  constructor(private snackBar: MatSnackBar, private service: SharedService) { }

  ngOnInit() {
  }

  download() {
    this.openSnackBar('Downloading', 'close');
    this.service.getExportUserInformation().subscribe(a => {
      this.service.downloadExcelFile(a, 'userInformation');
      this.openSnackBar('Completed', 'close');
    });
  }

  downloadCSV() {
    this.openSnackBar('Downloading', 'close');
    this.service.getExportUserInformation().subscribe(a => {
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
