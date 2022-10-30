import { Component, OnInit } from '@angular/core';
import { MatDialog, MatSnackBar } from '@angular/material';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { Router } from '@angular/router';
import { SharedService } from 'src/shared/shared.serice';
import { ClubDTO, FacetDTO, ProvinceDTO, RegistrationDTO } from 'src/shared/shared.models';
import { AddClubDialog } from './add-club-dialog/add-club-dialog';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  loginForm: FormGroup;
  isLoading = false;
  facets: FacetDTO[];
  provinces: ProvinceDTO[];
  clubs: ClubDTO[];
  federation: any[] = [];
  selectedProvince: number;
  selectedFacet: number;
  selectedClub: number;
  registrationDTO: RegistrationDTO;
  Name: string;
  Surname: string;
  Email: string;
  Phone: string;
  Password: string;
  CPassword: string;

  constructor(public snackBar: MatSnackBar, private formBuilder: FormBuilder, public dialog: MatDialog,
    private accountService: AccountService, private sharedService: SharedService, private router: Router) {
  }

  ngOnInit() {
    this.setupDataStream();
  }

  setupDataStream() {
    this.sharedService.getAllFacets().subscribe(a => {
      this.facets = a;
      console.log(this.facets);
    });
  }

  facetSelected() {
    this.provinces = this.facets.find(a => Number(a.id) === Number(this.selectedFacet)).provinces;
  }

  getFacetClubsByProvince() {
    this.sharedService.getFacetClubsByProvince(this.selectedFacet, this.selectedProvince).subscribe(a => {
      this.clubs = a;
    });
  }

  register() {
    if (this.Password != this.CPassword) {
      this.openSnackBar('Passwords does not match', 'Close');
      return;
    }

    const dialogRef = this.dialog.open(AddClubDialog, {
      //width: '250px',
      data: { facets: this.facets },
    });

    dialogRef.afterClosed().subscribe(result => {
      let userClubs = result;
      if (userClubs === undefined || userClubs === null) {
        userClubs = [this.selectedClub];
      } else {
        userClubs.push(this.selectedClub);
      }

      //add check here to see what federation this is and pass it to backend
      this.federation.push(1);

      this.registrationDTO = {
        userName: this.Email,
        password: this.Password,
        phoneNumber: this.Phone,
        name: this.Name,
        surname: this.Surname,
        clubs: userClubs,
        federations: this.federation
      }
      this.accountService.register(this.registrationDTO).subscribe(a => {
        this.openSnackBar('Account Created!', 'Close');
        this.router.navigate(['']);
      });
    });

  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }
}
