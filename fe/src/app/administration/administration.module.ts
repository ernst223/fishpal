import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdministrationComponent } from './administration.component';
import { AdministrationRoutingModule } from './administration-routing.module';
import { SharedModule } from 'src/shared/shared.module';
import { SharedService } from 'src/shared/shared.serice';
import { MatSnackBarModule } from '@angular/material';
import { FormsModule } from '@angular/forms';
import { DashboardComponent } from './dashboard/dashboard.component';
import { UserInformationComponent } from './user-information/user-information.component';


@NgModule({
  imports: [
    CommonModule,
    AdministrationRoutingModule,
    SharedModule,
    FormsModule,
    MatSnackBarModule
  ],
  declarations: [
    DashboardComponent, 
    AdministrationComponent,
    UserInformationComponent
  ],
  providers: [
    SharedService,
    MatSnackBarModule
  ]
})
export class AdministrationModule { }
