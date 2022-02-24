import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from 'src/shared/shared.module';
import { DatasetadministrationRoutingModule } from './datasetadministration-routing.module';
import { ManageDatasetComponent } from './manage-dataset/manage-dataset.component';
import { DatasetadministrationComponent } from './datasetadministration.component';
import { SharedService } from 'src/shared/shared.serice';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSnackBarModule } from '@angular/material/snack-bar';



@NgModule({
  imports: [
    CommonModule,
    DatasetadministrationRoutingModule,
    SharedModule,
    FormsModule,
    MatSnackBarModule,
    ReactiveFormsModule
  ],
  declarations: [
    ManageDatasetComponent,
    DatasetadministrationComponent
  ],
  providers: [
    SharedService,
    MatSnackBarModule
  ]

})
export class DatasetadministrationModule {}
