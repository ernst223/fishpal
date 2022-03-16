export interface RegistrationDTO {
  userName: string;
  password: string;
  name: string;
  surname: string;
  phoneNumber: string;
  clubs: number[];
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
