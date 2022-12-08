import { Component, OnInit } from '@angular/core';
import { MatDialogRef, MatSnackBar } from '@angular/material';
import { environment } from 'src/environments/environment';
import { SharedService } from 'src/shared/shared.serice';

@Component({
    selector: 'app-settings',
    templateUrl: './settings.component.html',
    styleUrls: ['./settings.component.scss']
})
export class SettingsComponent implements OnInit {

    profilefile: File;
    constructor(private service: SharedService, private snackBar: MatSnackBar, 
        public dialogRef: MatDialogRef<SettingsComponent>
    ) { }

    onNoClick(): void {
        this.dialogRef.close();
    }

    ngOnInit() { }

    myProfileImage() {
        return environment.apiUrl + "profilePicture/" + localStorage.getItem('profileId') + ".jpg"
    }

    OpenProfile(event: any) {
        this.profilefile = event.target.files[0];
        this.service.uploadProfilePicture(this.profilefile).subscribe(a => {
            this.openSnackBar('Profile Picture updated', 'Close');
            this.dialogRef.close();
        });
    }

    openSnackBar(message: string, action: string) {
        this.snackBar.open(message, action, {
          duration: 2000,
        });
      }
}
