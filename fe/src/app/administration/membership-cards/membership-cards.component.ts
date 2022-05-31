import { Component, OnInit } from '@angular/core';
import { jsPDF } from "jspdf";
import html2canvas from 'html2canvas';

@Component({
  selector: 'app-membership-cards',
  templateUrl: './membership-cards.component.html',
  styleUrls: ['./membership-cards.component.scss']
})
export class MembershipCardsComponent implements OnInit {
  toggleButton: boolean = false;
  constructor() { }

  ngOnInit() {
  }

  item = [{
    'name': 'Andre',
    'surname': 'Strauss',
    'facet': 'Artlure',
    'ValidUntil': '7 July 2023'
  }]

  qrInfo = JSON.stringify(this.item);


  generateProfile() {
    this.toggleButton = !this.toggleButton;
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
