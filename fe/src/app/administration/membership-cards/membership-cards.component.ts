import { Component, OnInit } from '@angular/core';
import { jsPDF } from "jspdf";
import html2canvas from 'html2canvas';
import { AccountService } from 'src/app/account/account.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-membership-cards',
  templateUrl: './membership-cards.component.html',
  styleUrls: ['./membership-cards.component.scss']
})
export class MembershipCardsComponent implements OnInit {
  loggedInUserName: string;
  loggedInFederationId: number;
  profileCard: any;
  name: string;
  surname: string;
  QRcode: string;

  constructor(public datepipe: DatePipe, private accountService: AccountService) {
  }

  longText = `The Shiba Inu is the smallest of the six original and distinct spitz breeds of dog
  from Japan. A small, agile dog that copes very well with mountainous terrain, the Shiba Inu was
  originally bred for hunting.`;

  ngOnInit() {
    this.loggedInUserName = localStorage.getItem('loggedInUserEmail');
    this.loggedInFederationId = Number(localStorage.getItem('federationId'));
    this.getFullDetailsForPerson(this.loggedInUserName, this.loggedInFederationId);
  }

  getFullDetailsForPerson(userName: string, federationID?: number) {
    this.accountService.getAllUserInfo(userName, federationID).subscribe((result: any) => {
      this.profileCard = result[0];

      this.name = result[0].name;
      this.surname = result[0].surname;

      this.createBarcode();
    });
  }

  createBarcode() {
    var barcodeString = this.loggedInUserName + "^"
      + this.loggedInFederationId + "^";

    this.QRcode = barcodeString;
  }

  getExpiryDate(date: Date) {
    var profileExpiryDate = this.datepipe.transform(date, 'dd/MM/yyyy');
    return profileExpiryDate;
  }

  public convertToPDF() {
    var personCard = document.getElementById('profilePDFCard');
    html2canvas(personCard).then(canvas => {
      // Few necessary setting options

      const contentDataURL = canvas.toDataURL('image/png')
      let pdf = new jsPDF('p', 'mm', 'a4'); // A4 size page of PDF
      var width = pdf.internal.pageSize.getWidth();
      var height = canvas.height * width / canvas.width;
      pdf.addImage(contentDataURL, 'PNG', 0, 0, width, height)
      pdf.save('output.pdf'); // Generated PDF
    });
  }
}
