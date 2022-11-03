import { Component, OnInit } from "@angular/core";
import { MatSnackBar } from "@angular/material";
import {
  MedicalInformationAllergiesDTO,
  MedicalInformationDTO,
  MedicalInformationEmergencyContactsDTO,
  MedicalInformationMedicalConditionsDTO,
  MedicalInformationPharmaciesDTO,
  MedicalInformationPhysiciansDTO,
} from "src/shared/shared.models";
import { SharedService } from "src/shared/shared.serice";

@Component({
  selector: "app-medical-information",
  templateUrl: "./medical-information.component.html",
  styleUrls: ["./medical-information.component.scss"],
})
export class MedicalInformationComponent implements OnInit {
  currentProfile = localStorage.getItem("profileId");

  physicians: MedicalInformationPhysiciansDTO[] = [];
  pharmacies: MedicalInformationPharmaciesDTO[] = [];
  emergencyContacts: MedicalInformationEmergencyContactsDTO[] = [];
  medicalConditions: MedicalInformationMedicalConditionsDTO[] = [];
  allergies: MedicalInformationAllergiesDTO[] = [];

  medicalInformation: MedicalInformationDTO = {
    id: null,
    medicalAidName: null,
    medicalAidContactNumber: null,
    medicalAidPlan: null,
    medicalAidNumber: null,
    medicalInformationPhysicians: [],
    medicalInformationPharmacies: [],
    medicalInformationEmergencyContacts: [],
    medicalInformationMedicalConditions: [],
    medicalInformationAllergies: [],
  };

  physicianName: string;
  physicianContact: string;

  pharmacyName: string;
  pharmacyContact: string;

  emname: string;
  emrelationship: string;
  emcontactNumberCell: string;
  emcontactNumberHome: string;

  conditionName: string;
  conditionMed: string;
  conditionDosage: string;
  conditionFreq: string;

  allergyName: string;
  allergyReaction: string;
  allergyMedication: string;

  constructor(private snackBar: MatSnackBar, private service: SharedService) { }

  ngOnInit() {
    this.setupDataStream();
  }

  setupDataStream() {
    this.service
      .getMedicalInformation(Number(this.currentProfile))
      .subscribe((a) => {
        this.medicalInformation = a;
        this.physicians = this.medicalInformation.medicalInformationPhysicians;
        this.pharmacies = this.medicalInformation.medicalInformationPharmacies;
        this.emergencyContacts = this.medicalInformation.medicalInformationEmergencyContacts;
        this.medicalConditions = this.medicalInformation.medicalInformationMedicalConditions;
        this.allergies = this.medicalInformation.medicalInformationAllergies;
      });
  }

  update() {
    this.medicalInformation.medicalInformationPhysicians = this.physicians;
    this.medicalInformation.medicalInformationPharmacies = this.pharmacies;
    this.medicalInformation.medicalInformationEmergencyContacts = this.emergencyContacts;
    this.medicalInformation.medicalInformationMedicalConditions = this.medicalConditions;
    this.medicalInformation.medicalInformationAllergies = this.allergies;

    console.log(this.medicalInformation);
    this.service.updateMedicalInformation(
      this.medicalInformation,
      Number(this.currentProfile)
    ).subscribe((a) => {
      this.setupDataStream();
      this.openSnackBar("Medical Information Updated", "close");
    });
  }

  removeFromPhysicians(selectedPhysician: MedicalInformationPhysiciansDTO) {
    const index = this.physicians.indexOf(selectedPhysician, 0);
    if (index > -1) {
      this.physicians.splice(index, 1);
    }
  }

  removeFromPharmacies(selectedPharmacy: MedicalInformationPharmaciesDTO) {
    const index = this.pharmacies.indexOf(selectedPharmacy, 0);
    if (index > -1) {
      this.pharmacies.splice(index, 1);
    }
  }

  removeFromEmergencyContacts(selectedEm: MedicalInformationEmergencyContactsDTO) {
    const index = this.emergencyContacts.indexOf(selectedEm, 0);
    if (index > -1) {
      this.emergencyContacts.splice(index, 1);
    }
  }

  removeFromMedicalConditions(selectedCondition: MedicalInformationMedicalConditionsDTO) {
    const index = this.medicalConditions.indexOf(selectedCondition, 0);
    if (index > -1) {
      this.medicalConditions.splice(index, 1);
    }
  }

  removeFromAllergies(selectedAllergy: MedicalInformationAllergiesDTO) {
    const index = this.allergies.indexOf(selectedAllergy, 0);
    if (index > -1) {
      this.allergies.splice(index, 1);
    }
  }

  addToPhysicians() {
    this.physicians.push({
      id: 0,
      physicianName: this.physicianName,
      physicianContactNumber: this.physicianContact,
    });
  }

  addToPharmacies() {
    this.pharmacies.push({
      id: 0,
      pharmacyName: this.pharmacyName,
      pharmacyContactNumber: this.pharmacyContact,
    });
  }

  addToEmergencyContacts() {
    this.emergencyContacts.push({
      id: 0,
      name: this.emname,
      relationship: this.emrelationship,
      contactNumberCell: this.emcontactNumberCell,
      contactNumberHome: this.emcontactNumberHome,
    });
  }

  addToMedicalConditions() {
    this.medicalConditions.push({
      id: 0,
      conditionName: this.conditionName,
      medicationName: this.conditionMed,
      medicationDosage: this.conditionDosage,
      medicationFrequency: this.conditionFreq,
    });
  }

  addToAllergies() {
    this.allergies.push({
      id: 0,
      allergyName: this.allergyName,
      allergyMedication: this.allergyMedication,
      allergyReaction: this.allergyReaction,
    });
  }

  openSnackBar(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 2000,
    });
  }
}
