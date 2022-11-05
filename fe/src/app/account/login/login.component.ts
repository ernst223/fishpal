import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MatSnackBar, MAT_DIALOG_DATA } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { Router } from '@angular/router';
import { LoginProfilesDTO } from 'src/shared/shared.models';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  loginProfiles: LoginProfilesDTO[];
  loginProfile: LoginProfilesDTO;
  isLoading = false;
  title = "Login";
  isForgetPassword = false;

  constructor(public snackBar: MatSnackBar, private formBuilder: FormBuilder,
              private accountService: AccountService, private router: Router, public dialog: MatDialog) {
                this.loginForm = this.formBuilder.group({
                  username: ['', Validators.required],
                  password: ['', Validators.required]
                });
              }

  ngOnInit() {
  }

  openDialog(): void {
    const dialogRef = this.dialog.open(ChooseProfileDialog, {
      width: '500px',
      data: this.loginProfiles,
    });

    dialogRef.afterClosed().subscribe(result => {
      localStorage.setItem('role',result.role);
      localStorage.setItem('club', result.club);
      localStorage.setItem('federation', result.federation);
      localStorage.setItem('profileName', result.name);
      localStorage.setItem('profileId', result.id);
      localStorage.setItem('federationId', result.federationId);
      this.router.navigate(['dashboard']);
    });
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

  forgotPasswordPage() {
    this.title = "Forgot your Password?"
    this.isForgetPassword = true;
  }

  goBackToLogin() {
    this.title = "Login"
    this.isForgetPassword = false;
  }

  login() {
    if (this.loginForm.invalid) {
      return;
    }
     this.isLoading = true;
     const formModel = this.loginForm.value;
    this.accountService.login(formModel.username, formModel.password).subscribe((result) => {
      if (result !== undefined) {
        this.isLoading = false;
        this.snackBar.open('Please a choose a profile to login.', '', {
          duration: 2000,
        }).afterDismissed().subscribe(() => { });
        if(result.length > 1) {
          this.loginProfiles = result;
          this.openDialog();
        } else {
          this.router.navigate(['dashboard']);
        }
      } else {
        this.isLoading = false;
        this.snackBar.open('Could not log in! Make sure your details are correct', '', {
          duration: 2000,
        }).afterDismissed().subscribe(() => { });
      }
    },
    error => {
      this.isLoading = false;
      this.snackBar.open('Could not log in! Make sure your details are correct', '', {
        duration: 2000,
      }).afterDismissed().subscribe(() => { });
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

@Component({
  selector: 'choose-profile-dialog',
  templateUrl: 'choose-profile-dialog.html',
})
export class ChooseProfileDialog {
  profiles: LoginProfilesDTO[];
  constructor(
    public dialogRef: MatDialogRef<ChooseProfileDialog>,
    @Inject(MAT_DIALOG_DATA) public data: LoginProfilesDTO[],
  ) {
    this.profiles = data;
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
