import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-verified-success',
  templateUrl: './verified-success.component.html',
  styleUrls: ['./verified-success.component.scss']
})
export class VerifiedSuccessComponent implements OnInit {
  userId: any;
  //validUntilDate: any;

  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.queryParams.subscribe(param => {
      this.userId = param.usrId;
      //this.validUntilDate = param.validUntilDate;
    });
  }

  getPersonProfilePicture() {
    return environment.apiUrl + "profilePicture/" + this.userId + ".jpg";
  }
}
