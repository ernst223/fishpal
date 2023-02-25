import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule, MatInputModule, MatSnackBarModule } from '@angular/material';
import { MatTabsModule } from '@angular/material/tabs';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { QRCodeModule } from 'angular2-qrcode';
import { SharedModule } from 'src/shared/shared.module';
import { SharedService } from 'src/shared/shared.serice';
import { RoleManagementComponent } from '../role-management/role-management.component';
import { AdministrationRoutingModule } from './administration-routing.module';
import { AdministrationComponent } from './administration.component';
import { AdministrationService } from './administration.service';
import { CalendarOfEventsComponent } from './calendar-of-events/calendar-of-events.component';
import { ClothesItemsComponent } from './clothes-items/clothes-items.component';
import { ClothesOrderComponent } from './clothes-order/clothes-order.component';
import { CommunicationComponent } from './communication/communication.component';
import { CoursesComponent } from './courses/courses.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { DocumentManagementComponent } from './document-management/document-management.component';
import { MembershipCardsComponent } from './membership-cards/membership-cards.component';
import { MobileAppComponent } from './mobile-app/mobile-app.component';
import { PaymentsComponent } from './payments/payments.component';
import { ProteaColorsComponent } from './protea-colors/protea-colors.component';
import { ReportingComponent } from './reporting/reporting.component';
import { AnglingAdministrationHistoryComponent } from './user-information/angling-administration-history/angling-administration-history.component';
import { AnglingHistoryComponent } from './user-information/angling-history/angling-history.component';
import { BoatInformationComponent } from './user-information/boat-information/boat-information.component';
import { ClubInformationComponent } from './user-information/club-information/club-information.component';
import { GeoProvinceInformationComponent } from './user-information/geo-province-information/geo-province-information.component';
import { MedicalInformationComponent } from './user-information/medical-information/medical-information.component';
import { OtherAnglingAchievementsComponent } from './user-information/other-angling-achievements/other-angling-achievements.component';
import { PersonalInformationComponent } from './user-information/personal-information/personal-information.component';
import { ProvincialInformationComponent } from './user-information/provincial-information/provincial-information.component';
import { TrainingComponent } from './user-information/training/training.component';
import { UserInformationComponent } from './user-information/user-information.component';
import {MatExpansionModule} from '@angular/material/expansion';
import {MatProgressBarModule} from '@angular/material/progress-bar';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
@NgModule({
  imports: [
    MatProgressSpinnerModule,
    MatProgressBarModule,
    MatExpansionModule,
    MatTabsModule,
    ReactiveFormsModule,
    QRCodeModule,
    CommonModule,
    AdministrationRoutingModule,
    SharedModule,
    FormsModule,
    MatSnackBarModule,
    MatFormFieldModule,
    MatInputModule,
    AngularFontAwesomeModule
  ],
  declarations: [
    DashboardComponent, 
    AdministrationComponent,
    UserInformationComponent,
    CalendarOfEventsComponent,
    DocumentManagementComponent,
    CommunicationComponent,
    CoursesComponent,
    ProteaColorsComponent,
    ReportingComponent,
    ClothesOrderComponent,
    PaymentsComponent,
    MembershipCardsComponent,
    MobileAppComponent,
    ClothesItemsComponent,
    MedicalInformationComponent,
    ClubInformationComponent,
    ProvincialInformationComponent,
    GeoProvinceInformationComponent,
    TrainingComponent,
    BoatInformationComponent,
    AnglingHistoryComponent,
    AnglingAdministrationHistoryComponent,
    OtherAnglingAchievementsComponent,
    PersonalInformationComponent,
    RoleManagementComponent
  ],
  providers: [
    AdministrationService,
    SharedService,
    MatSnackBarModule
  ]
})
export class AdministrationModule { }
