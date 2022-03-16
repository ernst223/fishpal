import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.scss']
})
export class ConfirmEmailComponent implements OnInit {

  title = 'Please wait for confirmation...';
  constructor(public snackBar: MatSnackBar, private route: ActivatedRoute,
    private service: AccountService) {
  }

  ngOnInit() {
      this.service.confirmEmail(this.route.snapshot.paramMap.get('id')).subscribe(a => {
        this.openSnackBar('Email Address Confirmed', 'Close');
        this.title = 'Congratiolations! Email Address Confirmed.';
      }, error => {
        this.openSnackBar('Could Not Confirm the Email Address', 'Close');
        this.title = 'Could not confirm this Users Email Address';
      });
  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }
}
