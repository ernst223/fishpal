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
