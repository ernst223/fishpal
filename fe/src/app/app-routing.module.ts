import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ShellComponent } from './shell/shell.component';

const routes: Routes = [
  {
    path: '',
     canActivate: [],
    component: ShellComponent,
    children: [
      { path: '', redirectTo: '/website/home', pathMatch: 'full' },
      { path: 'verified-success', redirectTo: '/verified-success/home', pathMatch: 'full' },
      { path: 'verified-failure', redirectTo: '/verified-failure/home', pathMatch: 'full' },
      { path: 'dashboard', loadChildren: './administration/administration.module#AdministrationModule'},
      { path: 'user-information', loadChildren: './administration/administration.module#AdministrationModule'},
      { path: 'calendar-of-events', loadChildren: './administration/administration.module#AdministrationModule'},
      { path: 'clothes-order', loadChildren: './administration/administration.module#AdministrationModule'},
      { path: 'communication', loadChildren: './administration/administration.module#AdministrationModule'},
      { path: 'courses', loadChildren: './administration/administration.module#AdministrationModule'},
      { path: 'document-management', loadChildren: './administration/administration.module#AdministrationModule'},
      { path: 'membership-cards', loadChildren: './administration/administration.module#AdministrationModule'},
      { path: 'mobile-app', loadChildren: './administration/administration.module#AdministrationModule'},
      { path: 'payments', loadChildren: './administration/administration.module#AdministrationModule'},
      { path: 'protea-colors', loadChildren: './administration/administration.module#AdministrationModule'},
      { path: 'reporting', loadChildren: './administration/administration.module#AdministrationModule'},
      { path: 'datasetadministration', loadChildren: './datasetadministration/datasetadministration.module#DatasetadministrationModule'},
      { path: 'role-management', loadChildren: './administration/administration.module#AdministrationModule'},
      { path: 'app-personal-information', loadChildren: './administration/administration.module#AdministrationModule'},
      { path: 'app-medical-information', loadChildren: './administration/administration.module#AdministrationModule'},
      { path: 'app-club-information', loadChildren: './administration/administration.module#AdministrationModule'},
      { path: 'app-provincial-information', loadChildren: './administration/administration.module#AdministrationModule'},
      { path: 'app-geo-province-information', loadChildren: './administration/administration.module#AdministrationModule'},
      { path: 'app-training', loadChildren: './administration/administration.module#AdministrationModule'},
      { path: 'app-boat-information', loadChildren: './administration/administration.module#AdministrationModule'},
      { path: 'app-angling-history', loadChildren: './administration/administration.module#AdministrationModule'},
      { path: 'app-angling-administration-history', loadChildren: './administration/administration.module#AdministrationModule'},
      { path: 'app-other-angling-achievements', loadChildren: './administration/administration.module#AdministrationModule'}
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: []
})
export class AppRoutingModule { }
