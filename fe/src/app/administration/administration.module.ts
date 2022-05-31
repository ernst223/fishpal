import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdministrationComponent } from './administration.component';
import { AdministrationRoutingModule } from './administration-routing.module';
import { SharedModule } from 'src/shared/shared.module';
import { SharedService } from 'src/shared/shared.serice';
import { MatFormFieldModule, MatInputModule, MatSnackBarModule } from '@angular/material';
import { FormsModule } from '@angular/forms';
import { DashboardComponent } from './dashboard/dashboard.component';
import { UserInformationComponent } from './user-information/user-information.component';
import { MedicalInformationComponent } from './user-information/medical-information/medical-information.component';
import { ClubInformationComponent } from './user-information/club-information/club-information.component';
import { ProvincialInformationComponent } from './user-information/provincial-information/provincial-information.component';
import { GeoProvinceInformationComponent } from './user-information/geo-province-information/geo-province-information.component';
import { TrainingComponent } from './user-information/training/training.component';
import { BoatInformationComponent } from './user-information/boat-information/boat-information.component';
import { AnglingHistoryComponent } from './user-information/angling-history/angling-history.component';
import { AnglingAdministrationHistoryComponent } from './user-information/angling-administration-history/angling-administration-history.component';
import { OtherAnglingAchievementsComponent } from './user-information/other-angling-achievements/other-angling-achievements.component';
import { PersonalInformationComponent } from './user-information/personal-information/personal-information.component';


@NgModule({
  imports: [
    CommonModule,
    AdministrationRoutingModule,
    SharedModule,
    FormsModule,
    MatSnackBarModule,
    MatFormFieldModule,
    MatInputModule
  ],
  declarations: [
    DashboardComponent, 
    AdministrationComponent,
    UserInformationComponent,
    MedicalInformationComponent,
    ClubInformationComponent,
    ProvincialInformationComponent,
    GeoProvinceInformationComponent,
    TrainingComponent,
    BoatInformationComponent,
    AnglingHistoryComponent,
    AnglingAdministrationHistoryComponent,
    OtherAnglingAchievementsComponent,
    PersonalInformationComponent
  ],
  providers: [
    SharedService,
    MatSnackBarModule
  ]
})
export class AdministrationModule { }
