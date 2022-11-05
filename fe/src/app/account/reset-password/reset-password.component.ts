import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ResetPasswordDTO } from 'src/shared/shared.models';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent implements OnInit {
  resetForm: FormGroup;
  isLoading = false;
  body: ResetPasswordDTO;

  constructor(public snackBar: MatSnackBar, private formBuilder: FormBuilder,
              private accountService: AccountService, private router: Router,
              private activeRoute: ActivatedRoute) {
                this.resetForm = this.formBuilder.group({
                  password: ['', Validators.required],
                  cpassword: ['', Validators.required]
                });
              }

  ngOnInit() {
  }

  reset() {
    this.isLoading = true;
    const formModel = this.resetForm.value;
    
    if (formModel.password === undefined || formModel.password === ''){
        this.isLoading = false;
        return;
    }
    if(formModel.password != formModel.cpassword){
        this.openSnackBar('Passwords does not Match', 'Close');
        this.isLoading = false;
        return;
    }
    
    this.body = {
        token: this.activeRoute.snapshot.queryParamMap.get('token'),
        newPassword: formModel.password,
        userName: this.activeRoute.snapshot.queryParamMap.get('user')
    };

    this.accountService.resetPassword(this.body).subscribe((result) => {
      if (result === true) {
        this.isLoading = false;
        this.router.navigate(['']);
      } else {
        this.isLoading = false;
        this.snackBar.open('Could not Reset Password! Make sure your Reset Email is not older than 24hours', '', {
          duration: 2000,
        }).afterDismissed().subscribe(() => { });
      }
    },
    error => {
      this.isLoading = false;
      this.snackBar.open('Could not Reset Password! Make sure your Reset Email is not older than 24hours', '', {
        duration: 2000,
      }).afterDismissed().subscribe(() => { });
    });
  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }
}
