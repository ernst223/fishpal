import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MatSnackBar, MAT_DIALOG_DATA } from '@angular/material';
import { environment } from 'src/environments/environment';
import { SharedService } from 'src/shared/shared.serice';

interface Role {
    value: string;
    viewValue: string;
  }

@Component({
    selector: 'app-role-change-dialog',
    templateUrl: './role-change-dialog.component.html',
    styleUrls: ['./role-change-dialog.component.scss']
})
export class RoleChangeDialogComponent implements OnInit {

    profilefile: File;
    selectedRole: string;

    roles: Role[] = [];

    constructor(private service: SharedService, private snackBar: MatSnackBar, 
        public dialogRef: MatDialogRef<RoleChangeDialogComponent>,
        @Inject(MAT_DIALOG_DATA) public data: number
    ) { }

    onNoClick(): void {
        this.dialogRef.close();
    }

    ngOnInit() { 
        let myRole = localStorage.getItem('role');
        if(myRole.charAt(0) == 'A') {
            this.addARoles();
            this.addBRoles();
            this.addCRoles();
            this.addDRoles();
            this.addERoles();
        }
        if(myRole.charAt(0) == 'B') {
            this.addBRoles();
            this.addCRoles();
            this.addDRoles();
            this.addERoles();
        }
        if(myRole.charAt(0) == 'C') {
            this.addCRoles();
            this.addDRoles();
            this.addERoles();
        }
        if(myRole.charAt(0) == 'D') {
            this.addDRoles();
            this.addERoles();
        }
    }

    addARoles() {
        this.roles.push({value: 'A1', viewValue: 'A1 President'});
        this.roles.push({value: 'A2', viewValue: 'A2 Vice-President'});
        this.roles.push({value: 'A3', viewValue: 'A3 Secretary'});
        this.roles.push({value: 'A4', viewValue: 'A4 Treasurer'});
        this.roles.push({value: 'A5', viewValue: 'A5 Registration officer'});
        this.roles.push({value: 'A6', viewValue: 'A6 Records officer'});
        this.roles.push({value: 'A7', viewValue: 'A7 Conservation officer'});
        this.roles.push({value: 'A8', viewValue: 'A8 Colours officer'});
        this.roles.push({value: 'A9', viewValue: 'A9 Tournament official'});
        this.roles.push({value: 'A10', viewValue: 'A10 Transformation officer'});
        this.roles.push({value: 'A11', viewValue: 'A11 Coach'});
        this.roles.push({value: 'A12', viewValue: 'A12 Anglers representative'});
        this.roles.push({value: 'A13', viewValue: 'A13 Officials commitee'});
    }
    addBRoles() {
        this.roles.push({value: 'B1', viewValue: 'B1 President'});
        this.roles.push({value: 'B2', viewValue: 'B2 Vice-President'});
        this.roles.push({value: 'B3', viewValue: 'B3 Secretary'});
        this.roles.push({value: 'B4', viewValue: 'B4 Treasurer'});
        this.roles.push({value: 'B5', viewValue: 'B5 Registration officer'});
        this.roles.push({value: 'B6', viewValue: 'B6 Records officer'});
        this.roles.push({value: 'B7', viewValue: 'B7 Conservation officer'});
        this.roles.push({value: 'B8', viewValue: 'B8 Colours officer'});
        this.roles.push({value: 'B9', viewValue: 'B9 Tournament official'});
        this.roles.push({value: 'B10', viewValue: 'B10 Transformation officer'});
        this.roles.push({value: 'B11', viewValue: 'B11 Coach'});
        this.roles.push({value: 'B12', viewValue: 'B12 Anglers representative'});
        this.roles.push({value: 'B13', viewValue: 'B13 Officials commitee'});
    }
    addCRoles() {
        this.roles.push({value: 'C1', viewValue: 'C1 President'});
        this.roles.push({value: 'C2', viewValue: 'C2 Vice-President'});
        this.roles.push({value: 'C3', viewValue: 'C3 Secretary'});
        this.roles.push({value: 'C4', viewValue: 'C4 Treasurer'});
        this.roles.push({value: 'C5', viewValue: 'C5 Registration officer'});
        this.roles.push({value: 'C6', viewValue: 'C6 Records officer'});
        this.roles.push({value: 'C7', viewValue: 'C7 Conservation officer'});
        this.roles.push({value: 'C8', viewValue: 'C8 Colours officer'});
        this.roles.push({value: 'C9', viewValue: 'C9 Tournament official'});
        this.roles.push({value: 'C10', viewValue: 'C10 Transformation officer'});
        this.roles.push({value: 'C11', viewValue: 'C11 Coach'});
        this.roles.push({value: 'C12', viewValue: 'C12 Anglers representative'});
        this.roles.push({value: 'C13', viewValue: 'C13 Officials commitee'});
    }
    addDRoles() {
        this.roles.push({value: 'D1', viewValue: 'D1 President'});
        this.roles.push({value: 'D2', viewValue: 'D2 Vice-President'});
        this.roles.push({value: 'D3', viewValue: 'D3 Secretary'});
        this.roles.push({value: 'D4', viewValue: 'D4 Treasurer'});
        this.roles.push({value: 'D5', viewValue: 'D5 Registration officer'});
        this.roles.push({value: 'D6', viewValue: 'D6 Records officer'});
        this.roles.push({value: 'D7', viewValue: 'D7 Conservation officer'});
        this.roles.push({value: 'D8', viewValue: 'D8 Colours officer'});
        this.roles.push({value: 'D9', viewValue: 'D9 Tournament official'});
        this.roles.push({value: 'D10', viewValue: 'D10 Transformation officer'});
        this.roles.push({value: 'D11', viewValue: 'D11 Coach'});
        this.roles.push({value: 'D12', viewValue: 'D12 Anglers representative'});
        this.roles.push({value: 'D13', viewValue: 'D13 Officials commitee'});
    }

    addERoles() {
        this.roles.push({value: 'E1', viewValue: 'Club member'});
        this.roles.push({value: 'E2', viewValue: 'Social member'});
    }

    updateRole() {
        if(this.selectedRole == '' || this.selectedRole == undefined) {
            this.openSnackBar('Please select a Role', 'Close');
        } else {
            this.service.updateUserRole(this.data, this.selectedRole).subscribe(a => {
                this.openSnackBar('Role Updated', 'close');
                this.dialogRef.close();
            });
        }
    }

    openSnackBar(message: string, action: string) {
        this.snackBar.open(message, action, {
          duration: 2000,
        });
      }
}
