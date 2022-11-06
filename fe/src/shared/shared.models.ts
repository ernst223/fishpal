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
  federation: string;
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
  selectedProvinceIds:number[];
}

export interface ClubDTO {
  id: number;
  name: string;
  province: string;
  facet: string;
}

export interface MessageDTO {
  id: number;
  message: string,
  creationDate: Date,
  status: number,
  creatorUserProfileId:number,
  rolesToSendTo: string[],
  statusChangeDate?: Date,
  approverRequired?: number,
  selectedProvince?: number[],
  selectedClubs?: number[],
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
export interface GeoProvinceInformationDTO {
  id: number;
  geoProvince: string;
  provincialSasaccManagement: string;
  position: string;
}
export interface MobileUserInfoDTO {
  ProfileId : number;
  UserId : string;
  Name : string;
  Surname: string;
  FacetName: string; 
  TypeName: string; 
  FacetId : number;
  ProfileCreationDate :Date
  ClubName : string;
  ClubId : number;
  Province : string;
  ProvinceId : number;
  FacetLogoBase64: string;
}

export interface ClubInformationComitteeMembersDTO {
  id: number;
  position: string;
  period: string;
}

export interface BoatInformationDTO {
  id: number;
  boatOwner: string;
  boatNumber: string;
  hullType: string;
  hullColour: string;
  motorMake: string;
  horsePower: string;
  towVehicleRegistrationNumber: string;
  trailerRegistrationNumber: string;
  cofNumber: string;
  cofExpiryDate: Date;
}

export interface TrainingDTO {
  id: number;
  managerYearCompleted: string;
  managerPointsReceived: string;
  coachLvl1YearCompleted: string;
  coachLvl1PointsReceived: string;
  coachLvl2YearCompleted: string;
  coachLvl2PointsReceived: string;
  captainYearCompleted: string;
  captainPointsReceived: string;
  adminYearCompleted: string;
  adminPointsReceived: string;
}

export interface ClubInformationPriorPeriodsDTO {
  id: number;
  clubName: string;
  clubPeriod: string;
}

export interface ClubInformationDTO {
  id: number;
  clubName: string;
  clubPeriod: string;
  clubConstitutionRecieved: string;
  clubConstitutionDateAccepted: Date;
  clubCodeOfConductRecieved: string;
  clubCodeOfConductDateAccepted: Date;
  clubDisciplinaryCodeRecieved: string;
  clubDisciplinaryCodeDateAccepted: Date;
  comitteeMembers: ClubInformationComitteeMembersDTO[];
  priorPeriods: ClubInformationPriorPeriodsDTO[];
}

export interface ProvincialInformationComteeMembersDTO{
  id: number;
  position: string;
  period: string;
}

export interface ProvincialInformationPriorPeriodsDTO {
  id: number;
  name: string;
  period: string;
}

export interface ProvincialInformationDTO {
  id: number;
  provinceName: string;
  provincePeriod: string;
  provinceConstitutionRecieved: string;
  provinceConstitutionDate: Date;
  provinceCodeOfCoductRecieved: string;
  provinceCodeOfCoductDate: Date;
  provinceDressCodeRecieved: string;
  provinceDressCodeDate: Date;
  provinceDisciplinaryCodeRecieved: string;
  provinceDisciplinaryCodeDate: Date;
  priorPeriods: ProvincialInformationPriorPeriodsDTO[];
  comitteeMembers: ProvincialInformationComteeMembersDTO[];
}

export interface OtherAnglingAchievementsDTO {
  id: number;
  achievement: string;
  year: string;
  team: string;
  teamMembers: string;
  tournament: string;
  venue: string;
}

export interface AnglishAdministrationHistoryDTO {
  id: number;
  capacity: string;
  year: string;
}
