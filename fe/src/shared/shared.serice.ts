import { HttpClient, HttpHeaders, HttpParams, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { saveAs } from 'file-saver';
import * as XLSX from 'xlsx';
import { AnglishAdministrationHistoryDTO, BoatInformationDTO, ClubDTO, ClubInformationDTO, EventDTO, exportUserInformationDTO, FacetDTO, GeoProvinceInformationDTO, MedicalInformationDTO, MyDocumentMessages, OtherAnglingAchievementsDTO, PersonalInformationDTO, ProvincialInformationDTO, RoleManagementUsersDTO, TrainingDTO, UpdateCourse, UploadDocumentMessage, UploadEventDTO } from './shared.models';


@Injectable()
export class SharedService {
  constructor(private httpClient: HttpClient) {
  }

  connectionstring = environment.apiUrl;

  public getEmployeeId() {
    let employeeId = localStorage.getItem('employeeId');
    let amountToAdd = 7 - employeeId.length;
    let result = '';
    for (var i = 0; i < amountToAdd; i++) {
      result = result + 0;
    }
    result = result + employeeId;
    return result;
  }

  public getAllFacets(): Observable<Array<FacetDTO>> {
    const headers = new HttpHeaders()
            .append('Content-Type', 'application/json')
            .append('Access-Control-Allow-Methods', '*');
    return this.httpClient.get(this.connectionstring + 'api/general/facets', { headers }).pipe(map((res: any) => res));
  }

  public getFacetClubsByProvince(facet: number, province: number): Observable<Array<ClubDTO>> {
    const headers = new HttpHeaders()
            .append('Content-Type', 'application/json')
            .append('Access-Control-Allow-Methods', '*');
    return this.httpClient.get(this.connectionstring + 'api/general/clubs/' + facet + "/" + province, { headers }).pipe(map((res: any) => res));
  }

  // Uploading Courses
  public uploadCourse(data: File): Observable<any> {
    const formData = new FormData();
    formData.append('file', data);

    return this.httpClient.post(this.connectionstring + 'api/communication/courses/create/' + localStorage.getItem('profileId'),
    formData, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public updateCourse(data: UpdateCourse): Observable<any> {
    console.log(data);
    return this.httpClient.put(this.connectionstring + 'api/communication/courses/update',
    data, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public getMyCourses(): Observable<any> {
    return this.httpClient.get(this.connectionstring + 'api/communication/courses/myCourses/' + localStorage.getItem('profileId'), {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public getApprovedCourses(): Observable<any> {
    return this.httpClient.get(this.connectionstring + 'api/communication/courses', {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public getUnApprovedCourses(): Observable<any> {
    return this.httpClient.get(this.connectionstring + 'api/communication/courses/unaproved', {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public approveCourse(id: number): Observable<any> {
    return this.httpClient.get(this.connectionstring + 'api/communication/courses/approve/' + id, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public declineCourse(id: number): Observable<any> {
    return this.httpClient.get(this.connectionstring + 'api/communication/courses/decline/' + id, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public enrollForCourse(id: number): Observable<any> {
    return this.httpClient.get(this.connectionstring + 'api/communication/courses/enroll/' + id + '/' + localStorage.getItem('profileId'), {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public getPendingEnrollments(): Observable<any> {
    return this.httpClient.get(this.connectionstring + 'api/communication/courses/enrollments/pending', {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public approveCourseEnrollment(id: number): Observable<any> {
    return this.httpClient.get(this.connectionstring + 'api/communication/courses/enrolment/approve/' + id, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public declineCourseEnrollment(id: number): Observable<any> {
    return this.httpClient.get(this.connectionstring + 'api/communication/courses/enrolment/decline/' + id, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  // Uploading a document File
  public uploadDocumentMessage(data: File, sendTo: string): Observable<any> {
    const formData = new FormData();
    formData.append('file', data);

    return this.httpClient.post(this.connectionstring + 'api/communication/document/send/' + localStorage.getItem('profileId') + "/" + sendTo,
    formData, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public uploadEvent(data: File): Observable<any> {
    const formData = new FormData();
    formData.append('file', data);

    return this.httpClient.post(this.connectionstring + 'api/communication/event/send/' + localStorage.getItem('profileId'),
    formData, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public uploadCommunicationMessage(sendTo: string): Observable<any> {
    return this.httpClient.get(this.connectionstring + 'api/communication/message/send/' + localStorage.getItem('profileId') + "/" + sendTo, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public updateDocumentMessage(data: UploadDocumentMessage): Observable<any> {
    return this.httpClient.put(this.connectionstring + 'api/communication/document/update',
    data, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public updateEvent(data: UploadEventDTO): Observable<any> {
    return this.httpClient.put(this.connectionstring + 'api/communication/event/update',
    data, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public updateCommunicationMessage(data: UploadDocumentMessage): Observable<any> {
    return this.httpClient.put(this.connectionstring + 'api/communication/message/update',
    data, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public getDocumentInboxMessages(): Observable<Array<MyDocumentMessages>> {
    return this.httpClient.get(this.connectionstring + 'api/communication/document/inbox/' + localStorage.getItem('profileId'), {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public getEventInbox(): Observable<Array<EventDTO>> {
    return this.httpClient.get(this.connectionstring + 'api/communication/event/inbox/' + localStorage.getItem('profileId'), {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public getCommunicationInboxMessages(): Observable<Array<MyDocumentMessages>> {
    return this.httpClient.get(this.connectionstring + 'api/communication/message/inbox/' + localStorage.getItem('profileId'), {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public getDocumentOutboxMessages(): Observable<Array<MyDocumentMessages>> {
    return this.httpClient.get(this.connectionstring + 'api/communication/document/outbox/' + localStorage.getItem('profileId'), {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public getEventOutbox(): Observable<Array<EventDTO>> {
    return this.httpClient.get(this.connectionstring + 'api/communication/event/outbox/' + localStorage.getItem('profileId'), {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public getCommunicationOutboxMessages(): Observable<Array<MyDocumentMessages>> {
    return this.httpClient.get(this.connectionstring + 'api/communication/message/outbox/' + localStorage.getItem('profileId'), {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public getPendingDocumentMessages(): Observable<Array<MyDocumentMessages>> {
    return this.httpClient.get(this.connectionstring + 'api/communication/document/pending/' + localStorage.getItem('profileId'), {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public getEventPending(): Observable<Array<EventDTO>> {
    return this.httpClient.get(this.connectionstring + 'api/communication/event/pending/' + localStorage.getItem('profileId'), {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public getPendingCommunicationMessages(): Observable<Array<MyDocumentMessages>> {
    return this.httpClient.get(this.connectionstring + 'api/communication/message/pending/' + localStorage.getItem('profileId'), {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public aprovePendingDocumentMessage(id: number): Observable<any> {
    return this.httpClient.get(this.connectionstring + 'api/communication/document/aprove/' + id, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public aprovePendingEvent(id: number): Observable<any> {
    return this.httpClient.get(this.connectionstring + 'api/communication/event/aprove/' + id, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public aprovePendingCommunicationMessage(id: number): Observable<any> {
    return this.httpClient.get(this.connectionstring + 'api/communication/message/aprove/' + id, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public declinePendingDocumentMessage(id: number): Observable<any> {
    return this.httpClient.get(this.connectionstring + 'api/communication/document/decline/' + id, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public declinePendingEvent(id: number): Observable<any> {
    return this.httpClient.get(this.connectionstring + 'api/communication/event/decline/' + id, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public declinePendingCommunicationMessage(id: number): Observable<any> {
    return this.httpClient.get(this.connectionstring + 'api/communication/message/decline/' + id, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public getRoleManagementUsers(): Observable<Array<RoleManagementUsersDTO>> {
    return this.httpClient.get(this.connectionstring + 'api/userinformation/rolemanagement/' + localStorage.getItem('profileId'), {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public getAccessableUsersToMessage(): Observable<Array<RoleManagementUsersDTO>> {
    return this.httpClient.get(this.connectionstring + 'api/communication/accessableprofiles/' + localStorage.getItem('profileId'), {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public updateUserRole(profileId: number, role: string): Observable<any> {
    return this.httpClient.get(this.connectionstring + 'api/userinformation/updateuserrole/' + profileId + '/' + role, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  // Personal Information
  public getPersonalInformation(profileId: number): Observable<PersonalInformationDTO> {
    return this.httpClient.get(this.connectionstring + 'api/userinformation/personalinformation/' + profileId, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public updatePersonalInformation(data: PersonalInformationDTO, profileId: number): Observable<any> {
    return this.httpClient.put(this.connectionstring + 'api/userinformation/personalinformation/' + profileId,
    data, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public getMedicalInformation(profileId: number): Observable<MedicalInformationDTO> {
    return this.httpClient.get(this.connectionstring + 'api/userinformation/medicalinformation/' + profileId, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public updateMedicalInformation(data: MedicalInformationDTO, profileId: number): Observable<any> {
    return this.httpClient.put(this.connectionstring + 'api/userinformation/medicalinformation/' + profileId,
    data, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public getGeoProvinceInformation(profileId: number): Observable<GeoProvinceInformationDTO> {
    return this.httpClient.get(this.connectionstring + 'api/geoProvince/getGeoProvinceInformation/' + profileId, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public updateGeoProvinceInformation(data: GeoProvinceInformationDTO, profileId: number): Observable<any> {
    return this.httpClient.put(this.connectionstring + 'api/geoProvince/updateGeoProvinceInformation/' + profileId,
    data, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => console.log(res)));
  }

  public getTrainingInformation(profileId: number): Observable<TrainingDTO> {
    return this.httpClient.get(this.connectionstring + 'api/training/getTrainingInformation/' + profileId, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public updateTrainingInformation(data: TrainingDTO, profileId: number): Observable<any> {
    return this.httpClient.put(this.connectionstring + 'api/training/updateTrainingInformation/' + profileId,
    data, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => console.log(res)));
  }

  public getBoatInformation(profileId: number): Observable<BoatInformationDTO> {
    return this.httpClient.get(this.connectionstring + 'api/boatInformation/getBoatInformation/' + profileId, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public updateBoatInformation(data: BoatInformationDTO, profileId: number): Observable<any> {
    return this.httpClient.put(this.connectionstring + 'api/boatInformation/updateBoatInformation/' + profileId,
    data, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => console.log(res)));
  }

  public getClubInformation(profileId: number): Observable<ClubInformationDTO> {
    return this.httpClient.get(this.connectionstring + 'api/userinformation/clubinformation/' + profileId, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public updateClubInformation(data: ClubInformationDTO, profileId: number): Observable<any> {
    return this.httpClient.put(this.connectionstring + 'api/userinformation/clubinformation/' + profileId,
    data, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public getProvincialInformation(profileId: number): Observable<ProvincialInformationDTO> {
    return this.httpClient.get(this.connectionstring + 'api/userinformation/provincialinformation/' + profileId, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public updateProvincialInformation(data: ProvincialInformationDTO, profileId: number): Observable<any> {
    return this.httpClient.put(this.connectionstring + 'api/userinformation/provincialinformation/' + profileId,
    data, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public getOtherAnglingAchievements(profileId: number): Observable<Array<OtherAnglingAchievementsDTO>> {
    return this.httpClient.get(this.connectionstring + 'api/userinformation/otheranglingachievements/' + profileId, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public updateOtherAnglingAchievements(data: OtherAnglingAchievementsDTO[], profileId: number): Observable<any> {
    return this.httpClient.put(this.connectionstring + 'api/userinformation/otheranglingachievements/' + profileId,
    data, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public getAnglishAdministrationHistory(profileId: number): Observable<Array<AnglishAdministrationHistoryDTO>> {
    return this.httpClient.get(this.connectionstring + 'api/userinformation/anglishadministrationaistory/' + profileId, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public updateAnglishAdministrationHistory(data: AnglishAdministrationHistoryDTO[], profileId: number): Observable<any> {
    return this.httpClient.put(this.connectionstring + 'api/userinformation/anglishadministrationaistory/' + profileId,
    data, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public uploadIdDocument(data: File): Observable<any> {
    const formData = new FormData();
    formData.append('file', data);

    return this.httpClient.post(this.connectionstring + 'api/userinformation/upload/id/' + localStorage.getItem('profileId'),
    formData, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public uploadPassportDocument(data: File): Observable<any> {
    const formData = new FormData();
    formData.append('file', data);

    return this.httpClient.post(this.connectionstring + 'api/userinformation/upload/passport/' + localStorage.getItem('profileId'),
    formData, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public uploadSkippersDocument(data: File): Observable<any> {
    const formData = new FormData();
    formData.append('file', data);

    return this.httpClient.post(this.connectionstring + 'api/userinformation/upload/skippers/' + localStorage.getItem('profileId'),
    formData, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public uploadMedicalDocument(data: File): Observable<any> {
    const formData = new FormData();
    formData.append('file', data);

    return this.httpClient.post(this.connectionstring + 'api/userinformation/upload/medicalaid/' + localStorage.getItem('profileId'),
    formData, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public uploadCOFDocument(data: File): Observable<any> {
    const formData = new FormData();
    formData.append('file', data);

    return this.httpClient.post(this.connectionstring + 'api/userinformation/upload/cof/' + localStorage.getItem('profileId'),
    formData, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public uploadProfilePicture(data: File): Observable<any> {
    const formData = new FormData();
    formData.append('file', data);

    return this.httpClient.post(this.connectionstring + 'api/userinformation/upload/profilePicture/' + localStorage.getItem('userId'),
    formData, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public getExportUserInformation(): Observable<Array<exportUserInformationDTO>> {
    return this.httpClient.get(this.connectionstring + 'api/communication/export/userinformation/' + + localStorage.getItem('profileId'), {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  downloadFile(data: any) {
    const replacer = (key, value) => value === null ? '' : value; // specify how you want to handle null values here
    const header = Object.keys(data[0]);
    let csv = data.map(row => header.map(fieldName => JSON.stringify(row[fieldName], replacer)).join(','));
    csv.unshift(header.join(','));
    let csvArray = csv.join('\r\n');

    var blob = new Blob([csvArray], {type: 'text/csv' })
    saveAs(blob, "userInformations.csv");
 }

 downloadExcelFile(data: any[], excelFileName: string) {
  const worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(data);
  const workbook: XLSX.WorkBook = { Sheets: { 'data': worksheet }, SheetNames: ['data'] };
  const excelBuffer: any = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
  this.saveAsExcelFile(excelBuffer, excelFileName);
 }

 private saveAsExcelFile(buffer: any, fileName: string): void {
  const EXCEL_TYPE = 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8';
  const EXCEL_EXTENSION = '.xlsx';
     const data: Blob = new Blob([buffer], {type: EXCEL_TYPE});
     saveAs(data, fileName + '_export_' + new  Date().getTime() + EXCEL_EXTENSION);
  }
}

