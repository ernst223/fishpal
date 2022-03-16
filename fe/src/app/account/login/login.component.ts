import { Component, OnInit } from '@angular/core';
import { MatSnackBar } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  isLoading = false;

  constructor(public snackBar: MatSnackBar, private formBuilder: FormBuilder,
              private accountService: AccountService, private router: Router) {
                this.loginForm = this.formBuilder.group({
                  username: ['', Validators.required],
                  password: ['', Validators.required]
                });
              }

  ngOnInit() {
  }

  forgotPassword() {
    const formModel = this.loginForm.value;
    if(formModel.username == undefined || formModel.username == '') {
      this.openSnackBar('Please fill in a Username or Email.', 'Close');
    } else {
      this.accountService.sendResetPasswordEmail(formModel.username).subscribe((result) => {
        this.openSnackBar('Reset Link is send if the account exist', 'Close');
      }, error => {
        this.openSnackBar('Reset Link is send if the account exist', 'Close');
      });
    }
  }

  login() {
    if (this.loginForm.invalid) {
      return;
    }
     this.isLoading = true;
     const formModel = this.loginForm.value;
    this.accountService.login(formModel.username, formModel.password).subscribe((result) => {
      if (result === true) {
        this.isLoading = false;
        this.router.navigate(['dashboard']);
      } else {
        this.isLoading = false;
        this.snackBar.open('Could not log in! Make sure your details are correct', '', {
          duration: 2000,
        }).afterDismissed().subscribe(() => { });

        console.log(result);
      }
    },
    error => {
      this.isLoading = false;
      this.snackBar.open('Could not log in! Make sure your details are correct', '', {
        duration: 2000,
      }).afterDismissed().subscribe(() => { });

      console.log(error);
    });
  }

  register() {
    this.router.navigate(['/account/register']);
  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }
}
