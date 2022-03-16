import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountRoutingModule } from './account-routing.module';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AccountService } from './account.service';
import { MatSnackBarModule } from '@angular/material';
import { SharedModule } from 'src/shared/shared.module';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { SharedService } from 'src/shared/shared.serice';
import { ConfirmEmailComponent } from './confirm-email/confirm-email.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { AddClubDialog } from './register/add-club-dialog/add-club-dialog';

@NgModule({
  imports: [
    CommonModule,
    SharedModule,
    ReactiveFormsModule,
    AccountRoutingModule,
    RouterModule,
    MatSnackBarModule,
    FormsModule
  ],
  declarations: [
    LoginComponent,
    RegisterComponent,
    ConfirmEmailComponent,
    ResetPasswordComponent,
    AddClubDialog
  ],
  providers: [
    AccountService,
    SharedService
  ],
  entryComponents: [
    AddClubDialog
  ]
})
export class AccountModule { }
