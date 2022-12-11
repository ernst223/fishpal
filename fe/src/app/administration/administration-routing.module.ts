import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RoleManagementComponent } from '../role-management/role-management.component';
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
    { path: 'reporting', component: ReportingComponent, data: { breadcrumb: 'Reporting' } },
    { path: 'role-management', component: RoleManagementComponent, data: { breadcrumb: 'RoleManagement' }},
    { path: 'app-personal-information', component: PersonalInformationComponent, data: { breadcrumb: 'PersonalInformation' }},
    { path: 'app-medical-information', component: MedicalInformationComponent, data: { breadcrumb: 'MedicalInformation' }},
    { path: 'app-club-information', component: ClubInformationComponent, data: { breadcrumb: 'ClubInformation' }},
    { path: 'app-provincial-information', component: ProvincialInformationComponent, data: { breadcrumb: 'ProvinceInformation' }},
    { path: 'app-geo-province-information', component: GeoProvinceInformationComponent, data: { breadcrumb: 'GeoProvinceInformation' }},
    { path: 'app-training', component: TrainingComponent, data: { breadcrumb: 'TrainingInformation' }},
    { path: 'app-boat-information',component: BoatInformationComponent, data: { breadcrumb: 'Boatinformation' }},
    { path: 'app-angling-history', component: AnglingHistoryComponent, data: { breadcrumb: 'AnglingHistory' }},
    { path: 'app-angling-administration-history', component: AnglingAdministrationHistoryComponent, data: { breadcrumb: 'AnglingAdministrationhistory' }},
    { path: 'app-other-angling-achievements', component: OtherAnglingAchievementsComponent, data: { breadcrumb: 'OtherAnglingAchievements' }}
  ],
  data: { breadcrumb: 'Administration' }
}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdministrationRoutingModule { }
