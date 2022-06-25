import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import {  ResetPasswordDTO } from 'src/shared/shared.models';
import { InsertClothesOrderModel } from './models/add-clothes-order-form-model';

@Injectable()
export class AdministrationService {

    constructor(private httpClient: HttpClient) { }

    connectionstring = environment.apiUrl;

    /*public sendResetPasswordEmail(username: string): Observable<any> {
        const headers = new HttpHeaders()
            .append('Content-Type', 'application/json')
            .append('Access-Control-Allow-Methods', '*');
            return this.httpClient.get(this.connectionstring + 'api/auth/resetEmail/' + username, { headers }).pipe(
            map((res: any) => {
                if (res.err == 'err') {
                    return false;
                }
                return true;
            }));
    }*/

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

    
}
