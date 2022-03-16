import { Component, Inject } from "@angular/core";
import { MatDialogRef, MatSnackBar, MAT_DIALOG_DATA } from "@angular/material";
import { ClubDTO, FacetDTO, ProvinceDTO } from "src/shared/shared.models";
import { SharedService } from "src/shared/shared.serice";

export interface DialogData {
    facets: FacetDTO[]
}

@Component({
    selector: 'add-club-dialog',
    templateUrl: 'add-club-dialog.html',
})
export class AddClubDialog {
    showClubs = false;
    selectedClubs: number[] = [];
    provinces: ProvinceDTO[];
    clubs: ClubDTO[];
    selectedProvince: number;
    selectedFacet: number;
    selectedClub: number;

    constructor(
        public dialogRef: MatDialogRef<AddClubDialog>,
        @Inject(MAT_DIALOG_DATA) public data: DialogData,
        public snackBar: MatSnackBar, private sharedService: SharedService
    ) { }

    onNoClick(): void {
        this.dialogRef.close();
    }

    facetSelected() {
        this.provinces = this.data.facets.find(a => Number(a.id) === Number(this.selectedFacet)).provinces;
    }

    getFacetClubsByProvince() {
        this.sharedService.getFacetClubsByProvince(this.selectedFacet, this.selectedProvince).subscribe(a => {
            this.clubs = a;
        });
    }

    addAnotherClub() {
        this.selectedClubs.push(this.selectedClub);
        this.selectedClub = undefined;
        this.selectedFacet = undefined;
        this.selectedProvince = undefined;
        this.openSnackBar('Choose a Club', '');
    }

    toggleShowAddClubs() {
        if (this.showClubs) {
            this.showClubs = false;
        } else {
            this.showClubs = true;
        }
    }

    getShowClubs() {
        return this.showClubs;
    }

    done() {
        this.selectedClubs.push(this.selectedClub);
        this.dialogRef.close(this.selectedClubs);
    }

    openSnackBar(message: string, action: string) {
        this.snackBar.open(message, action, {
            duration: 2000,
        });
    }
}