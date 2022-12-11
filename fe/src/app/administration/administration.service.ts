import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ClubDTO, FacetDTO, FederationDTO, MessageDTO, ProvinceDTO } from 'src/shared/shared.models';
import { InsertClothesOrderModel } from './models/add-clothes-order-form-model';

@Injectable()
export class AdministrationService {

    constructor(private httpClient: HttpClient) { }

    connectionstring = environment.apiUrl;

    public insertOrderItem(body: InsertClothesOrderModel): Observable<any> {
        const headers = new HttpHeaders()
            .append('Content-Type', 'application/json')
            .append('Access-Control-Allow-Methods', '*');
            return this.httpClient.post(this.connectionstring + 'api/clothes/insertOrderItem', body, { headers }).pipe(
            map((res: any) => {
                if (res.err == 'err') {
                    return false;
                }
                return true;
            }));
    }
    
    public sendMessages(body: MessageDTO, federationId:number, profileId:number, sendEmail:boolean): Observable<any> {
      const headers = new HttpHeaders()
          .append('Content-Type', 'application/json')
          .append('Access-Control-Allow-Methods', '*');
          return this.httpClient.post(this.connectionstring + 'api/communication/getAll/sendMessages/' + federationId + "/" + profileId + "/" + sendEmail, body, { headers }).pipe(
          map((res: any) => {
          }));
  }

    public getAllFederations(userRole:string): Observable<Array<FacetDTO>> {
        const headers = new HttpHeaders()
                .append('Content-Type', 'application/json')
                .append('Access-Control-Allow-Methods', '*');
        return this.httpClient.get(this.connectionstring + 'api/communication/getAll/federations/' + userRole, { headers }).pipe(map((res: any) => res));
      }    

      public getAllRolesCurrentRoleCanSendTo(userRole:string): Observable<Array<string>> {
        const headers = new HttpHeaders()
                .append('Content-Type', 'application/json')
                .append('Access-Control-Allow-Methods', '*');
        return this.httpClient.get(this.connectionstring + 'api/communication/getAll/RolesCurrentRoleCanSendTo/' + userRole, { headers }).pipe(map((res: any) => res));
      }  

      public getAllProvinces(loggedInUserEmail:string, selectedFederations:FederationDTO[]): Observable<Array<ProvinceDTO>> {
        const headers = new HttpHeaders()
                .append('Content-Type', 'application/json')
                .append('Access-Control-Allow-Methods', '*');
        return this.httpClient.post(this.connectionstring + 'api/communication/getAll/provinces/' + loggedInUserEmail, selectedFederations, { headers }).pipe(map((res: any) => res));
      } 

      public getAllMessages(messageTypeToReturn: number, profileId:number): Observable<Array<MessageDTO>> {
        const headers = new HttpHeaders()
                .append('Content-Type', 'application/json')
                .append('Access-Control-Allow-Methods', '*');
        return this.httpClient.get(this.connectionstring + 'api/communication/getAllMessage/' + messageTypeToReturn + "/" + profileId, { headers }).pipe(map((res: any) => res));
      } 
      
      public deleteMessage(messageId:number): Observable<Array<MessageDTO>> {
        const headers = new HttpHeaders()
                .append('Content-Type', 'application/json')
                .append('Access-Control-Allow-Methods', '*');
        return this.httpClient.delete(this.connectionstring + 'api/communication/deleteMessage/' + messageId, { headers }).pipe(map((res: any) => res));
      }

      public approveDeclineMessage(approveDecline:number, messageId:number): Observable<Array<MessageDTO>> {
        const headers = new HttpHeaders()
                .append('Content-Type', 'application/json')
                .append('Access-Control-Allow-Methods', '*');
        return this.httpClient.post(this.connectionstring + 'api/communication/approveDeclineMessage/' + approveDecline + "/" + messageId, { headers }).pipe(map((res: any) => res));
      }

      public getAllProvincesForSelectedFederation(userRole:string, federationId:number): Observable<Array<ProvinceDTO>> {
        const headers = new HttpHeaders()
                .append('Content-Type', 'application/json')
                .append('Access-Control-Allow-Methods', '*');
        return this.httpClient.get(this.connectionstring + 'api/communication/getAll/getAllProvincesForSelectedFederation/' + userRole + '/' + federationId, { headers }).pipe(map((res: any) => res));
      }  
      
      public getAllClubsForSelectedProvinces(province:ProvinceDTO): Observable<Array<ClubDTO>> {
        province.id = 0;
        const headers = new HttpHeaders()
                .append('Content-Type', 'application/json')
                .append('Access-Control-Allow-Methods', '*');
        return this.httpClient.post(this.connectionstring + 'api/communication/getAll/getAllClubsForSelectedProvinces',province, { headers }).pipe(map((res: any) => res));
      }  
}
