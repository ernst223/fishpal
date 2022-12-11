import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-verified-failure',
  templateUrl: './verified-failure.component.html',
  styleUrls: ['./verified-failure.component.scss']
})
export class VerifiedFailureComponent implements OnInit {
  userId: any;
  //expDate: any;

  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.queryParams.subscribe(param => {
      this.userId =  param.usrId;
      //this.expDate =  param.expdate;
    });
  }

  getPersonProfilePicture() {
    return environment.apiUrl + "profilePicture/" + this.userId + ".jpg";
  }
}
