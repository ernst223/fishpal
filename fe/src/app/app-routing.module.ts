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
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: []
})
export class AppRoutingModule { }
