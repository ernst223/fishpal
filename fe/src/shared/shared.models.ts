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

export interface FacetDTO {
  id: number;
  description: string;
  provinces: ProvinceDTO[];
}

export interface UploadDocumentMessage {
  data: FormData;
  userName: string;
  sendTo: number;
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
  description: string;
}

export interface ClubDTO {
  id: number;
  description: string;
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
