import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdministrationComponent } from './administration.component';
import { AdministrationRoutingModule } from './administration-routing.module';
import { SharedModule } from 'src/shared/shared.module';
import { SharedService } from 'src/shared/shared.serice';
import { MatSnackBarModule } from '@angular/material';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { DashboardComponent } from './dashboard/dashboard.component';
import { UserInformationComponent } from './user-information/user-information.component';
import { CalendarOfEventsComponent } from './calendar-of-events/calendar-of-events.component';
import { DocumentManagementComponent } from './document-management/document-management.component';
import { CommunicationComponent } from './communication/communication.component';
import { CoursesComponent } from './courses/courses.component';
import { ProteaColorsComponent } from './protea-colors/protea-colors.component';
import { ReportingComponent } from './reporting/reporting.component';
import { ClothesOrderComponent } from './clothes-order/clothes-order.component';
import { PaymentsComponent } from './payments/payments.component';
import { MembershipCardsComponent } from './membership-cards/membership-cards.component';
import { MobileAppComponent } from './mobile-app/mobile-app.component';
import { QRCodeModule } from 'angular2-qrcode';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
@NgModule({
  imports: [
    ReactiveFormsModule,
    QRCodeModule,
    CommonModule,
    AdministrationRoutingModule,
    SharedModule,
    FormsModule,
    MatSnackBarModule,
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
    MobileAppComponent
  ],
  providers: [
    SharedService,
    MatSnackBarModule
  ]
})
export class AdministrationModule { }
