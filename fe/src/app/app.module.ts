import { DatePipe } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { Router } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { AccountModule } from './account/account.module';
import { RoleChangeDialogComponent } from './administration/role-change-dialog/role-change-dialog.component';
import { SettingsComponent } from './administration/settings/settings.component';
import { AppRoutingModule } from './app-routing.module';
import { VerifiedFailureComponent } from './app-verification/verified-failure/verified-failure.component';
import { VerifiedSuccessComponent } from './app-verification/verified-success/verified-success.component';
import { AppComponent } from './app.component';
import { HeaderInterceptor } from './interceptors/header.interceptor';
import { NavbarpageComponent } from './navbarpage/navbarpage.component';
import { MenuService } from './shell/menu.service';
import { ShellBreadcrumbsComponent } from './shell/shell-breadcrumbs/shell-breadcrumbs.component';
import { ShellNavListComponent } from './shell/shell-nav-list/shell-nav-list.component';
import { ShellComponent } from './shell/shell.component';
import { WebsiteComponent } from './website/website.component';


@NgModule({
  declarations: [
    AppComponent,
    ShellBreadcrumbsComponent,
    ShellNavListComponent,
    ShellComponent,
    WebsiteComponent,
    NavbarpageComponent,
    SettingsComponent,
    RoleChangeDialogComponent,
    VerifiedSuccessComponent,
    VerifiedFailureComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    SharedModule,
    BrowserAnimationsModule,
    AccountModule
  ],
  providers: [
    DatePipe,
    MenuService,
    { provide: HTTP_INTERCEPTORS, useClass: HeaderInterceptor, multi: true}
  ],
  bootstrap: [AppComponent],
  entryComponents: [SettingsComponent, RoleChangeDialogComponent]
})
export class AppModule {
  constructor(router: Router) {
  }
}
