import * as internal from "assert";

export interface RegistrationDTO {
  userName: string;
  password: string;
  name: string;
  surname: string;
  phoneNumber: string;
  clubs: number[];
  federations: number[];
}
export interface ResetPasswordDTO {
  token: string;
  newPassword: string;
  userName: string;
}

export interface LoginProfilesDTO {
  id: number;
  name: string;
  role: string;
  club: string;
  federation: string;
}

export interface FacetDTO {
  id: number;
  name: string;
  provinces: ProvinceDTO[];
}

export interface UploadDocumentMessage {
  documentId: number;
  title: string;
  note: string;
}

export interface MyDocumentMessages {
  id: number;
  title: string;
  note: string;
  sendFrom: string;
}

export interface FederationDTO {
  Id: number;
  Name: string;
}

export interface ProvinceDTO {
  id: number;
  name: string;
}

export interface ClubDTO {
  id: number;
  name: string;
  province: string;
  facet: string;
}

export interface MessageDTO {
  Id: number;
  Message: string,
  CreationDate: Date,
  Status: number,
  InboxOutbox: number,
  CreatoruserId: string,
  StatusChangeDate?: Date,
  ApproverRequired?: string
}

export interface PersonalInformationDTO {
  name: string;
  nickName: string;
  surname: string;
  idNumber: string;
  dob: Date;
  nationality: string;
  ethnicGroup: string;
  gender: string;
  passportNumber: string;
  passportExpirationDate: Date;
  homeAddress: string;
  postalAddress: string;
  phone: string;
  cell: string;
  skipperLicenseNumber: string;
}

export interface MedicalInformationPhysiciansDTO {
  id: number;
  physicianName: string;
  physicianContactNumber: string;
}

export interface MedicalInformationPharmaciesDTO {
  id: number;
  pharmacyName: string;
  pharmacyContactNumber: string;
}

export interface MedicalInformationEmergencyContactsDTO {
  id: number;
  name: string;
  relationship: string;
  contactNumberCell: string;
  contactNumberHome: string;
}

export interface MedicalInformationMedicalConditionsDTO {
  id: number;
  conditionName: string;
  medicationName: string;
  medicationDosage: string;
  medicationFrequency: string;
}

export interface MedicalInformationAllergiesDTO {
  id: number;
  allergyName: string;
  allergyReaction: string;
  allergyMedication: string;
}

export interface MedicalInformationDTO {
  id: number;
  medicalAidName: string;
  medicalAidPlan: string;
  medicalAidNumber: string;
  medicalAidContactNumber: string;
  medicalInformationPhysicians: MedicalInformationPhysiciansDTO[];
  medicalInformationPharmacies: MedicalInformationPharmaciesDTO[];
  medicalInformationEmergencyContacts: MedicalInformationEmergencyContactsDTO[];
  medicalInformationMedicalConditions: MedicalInformationMedicalConditionsDTO[];
  medicalInformationAllergies: MedicalInformationAllergiesDTO[];
}
