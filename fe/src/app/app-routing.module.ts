import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ShellComponent } from './shell/shell.component';



const routes: Routes = [
  {
    path: '',
     canActivate: [],
    component: ShellComponent,
    children: [
      { path: '', redirectTo: '/account/login', pathMatch: 'full' },
      { path: 'dashboard', loadChildren: './administration/administration.module#AdministrationModule'},
      { path: 'user-information', loadChildren: './administration/administration.module#AdministrationModule'},
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
