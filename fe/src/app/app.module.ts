import { DatePipe } from '@angular/common';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { Router } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { AccountModule } from './account/account.module';
import { AppRoutingModule } from './app-routing.module';
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
    NavbarpageComponent
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
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor(router: Router) {
  }
}
