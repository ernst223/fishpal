import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AdministrationComponent } from './administration.component';
import { CalendarOfEventsComponent } from './calendar-of-events/calendar-of-events.component';
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
import { UserInformationComponent } from './user-information/user-information.component';

const routes: Routes = [{
  path: '',
  component: AdministrationComponent,
  children: [
    { path: '', redirectTo: 'dashboard', pathMatch: 'full' },
    { path: 'dashboard', component: DashboardComponent, data: { breadcrumb: 'Dashboard' } },
    { path: 'user-information', component: UserInformationComponent, data: { breadcrumb: 'UserInformation' } },
    { path: 'calendar-of-events', component: CalendarOfEventsComponent, data: { breadcrumb: 'CalendarOfEvents' } },
    { path: 'clothes-order', component: ClothesOrderComponent, data: { breadcrumb: 'ClothesOrder' } },
    { path: 'communication', component: CommunicationComponent, data: { breadcrumb: 'Communication' } },
    { path: 'courses', component: CoursesComponent, data: { breadcrumb: 'Courses' } },
    { path: 'document-management', component: DocumentManagementComponent, data: { breadcrumb: 'DocumentManagement' } },
    { path: 'membership-cards', component: MembershipCardsComponent, data: { breadcrumb: 'MembershipCards' } },
    { path: 'mobile-app', component: MobileAppComponent, data: { breadcrumb: 'MobileApp' } },
    { path: 'payments', component: PaymentsComponent, data: { breadcrumb: 'Payments' } },
    { path: 'protea-colors', component: ProteaColorsComponent, data: { breadcrumb: 'ProteaColors' } },
    { path: 'reporting', component: ReportingComponent, data: { breadcrumb: 'Reporting' } }
  ],
  data: { breadcrumb: 'Administration' }
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdministrationRoutingModule { }
