import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { VerifiedFailureComponent } from '../app-verification/verified-failure/verified-failure.component';
import { VerifiedSuccessComponent } from '../app-verification/verified-success/verified-success.component';
import { WebsiteComponent } from '../website/website.component';
import { ConfirmEmailComponent } from './confirm-email/confirm-email.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';

const routes: Routes = [
  {
    path: 'account',
    children: [
      { path: 'login', component: LoginComponent, pathMatch: 'full' },
      { path: 'register', component: RegisterComponent, pathMatch: 'full' },
      { path: 'confirm-email/:id', component: ConfirmEmailComponent, pathMatch: 'full'},
      { path: 'reset-password', component: ResetPasswordComponent, pathMatch: 'full'},
      { path: 'website', component: WebsiteComponent, pathMatch: 'full' },
      { path: 'verified-success', component: VerifiedSuccessComponent, pathMatch: 'full' },
      { path: 'verified-failure', component: VerifiedFailureComponent, pathMatch: 'full' },
    ],
  },
  {
    path: 'website',
    children: [
      { path: 'home', component: WebsiteComponent, pathMatch: 'full'},
    ],
  }, 
  {
    path: 'verified-success',
    children: [
      { path: 'home', component: VerifiedSuccessComponent, pathMatch: 'full'},
    ],
  },
  {
    path: 'verified-failure',
    children: [
      { path: 'home', component: VerifiedFailureComponent, pathMatch: 'full'},
    ],
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccountRoutingModule { }
