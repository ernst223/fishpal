import { HttpClient, HttpHeaders, HttpParams, HttpRequest } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ClubDTO, FacetDTO, MyDocumentMessages, UploadDocumentMessage } from './shared.models';


@Injectable()
export class SharedService {
  constructor(private httpClient: HttpClient) {
  }

  connectionstring = environment.apiUrl;

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

  // Uploading a document File
  public uploadDocumentMessage(data: File, sendTo: number): Observable<any> {
    const formData = new FormData();
    formData.append('file', data);

    return this.httpClient.post(this.connectionstring + 'api/communication/document/send/' + localStorage.getItem('profileId') + "/" + sendTo,
    formData, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public updateDocumentMessage(data: UploadDocumentMessage): Observable<any> {
    return this.httpClient.put(this.connectionstring + 'api/communication/document/update',
    data, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public getDocumentInboxMessages(): Observable<Array<MyDocumentMessages>> {
    return this.httpClient.get(this.connectionstring + 'api/communication/document/inbox/' + localStorage.getItem('profileId'), {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public getDocumentOutboxMessages(): Observable<Array<MyDocumentMessages>> {
    return this.httpClient.get(this.connectionstring + 'api/communication/document/outbox/' + localStorage.getItem('profileId'), {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public getPendingDocumentMessages(): Observable<Array<MyDocumentMessages>> {
    return this.httpClient.get(this.connectionstring + 'api/communication/document/pending/' + localStorage.getItem('profileId'), {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public aprovePendingDocumentMessage(id: number): Observable<any> {
    return this.httpClient.get(this.connectionstring + 'api/communication/document/aprove/' + id, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }

  public declinePendingDocumentMessage(id: number): Observable<any> {
    return this.httpClient.get(this.connectionstring + 'api/communication/document/decline/' + id, {
      headers: new HttpHeaders().set('Authorization', `Bearer ${localStorage.getItem('access_token')}`)
    }).pipe(map((res: any) => res));
  }
}
