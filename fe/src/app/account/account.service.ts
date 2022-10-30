import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { LoginProfilesDTO, RegistrationDTO, ResetPasswordDTO } from 'src/shared/shared.models';

@Injectable()
export class AccountService {

    loginProfiles: LoginProfilesDTO[];
    constructor(private httpClient: HttpClient) { }

    connectionstring = environment.apiUrl;

    public login(username: string, password: string): Observable<LoginProfilesDTO[]> {
        const body = '{ "UserName": "' + username + '","Password": "' + password + '"}';
        const headers = new HttpHeaders()
            .append('Content-Type', 'application/json')
            .append('Access-Control-Allow-Methods', '*');
            return this.httpClient.post(this.connectionstring + 'api/auth/token', body, { headers }).pipe(
            map((res: any) => {
                if (res.err == 'err') {
                    return undefined;
                }
                if (typeof res.token !== 'undefined') {
                    // Stores access token & refresh token.
                    this.loginProfiles = res.profiles;
                    
                    if (this.loginProfiles.length > 1) {
                        localStorage.setItem('multipleProfiles', '1');
                    } else {
                        localStorage.setItem('multipleProfiles', '0');
                        localStorage.setItem('role', res.profiles[0].role);
                        localStorage.setItem('profileId', res.profile[0].id);
                        localStorage.setItem('club', res.profiles[0].club);
                        localStorage.setItem('federation', res.profiles[0].federation);
                        localStorage.setItem('profileName', res.profiles[0].name);
                    }
                    localStorage.setItem('access_token', res.token);
                    localStorage.setItem('loggedInUserEmail', res.userName);
                    return this.loginProfiles;
                } else {
                    return undefined;
                }
            }));
    }

    public register(body: RegistrationDTO): Observable<any> {
        const headers = new HttpHeaders()
            .append('Content-Type', 'application/json')
            .append('Access-Control-Allow-Methods', '*');
            return this.httpClient.post(this.connectionstring + 'api/auth/user', body, { headers }).pipe(
            map((res: any) => {
                if (res.err == 'err') {
                    return false;
                }
                return true;
            }));
    }

    public confirmEmail(id: string): Observable<any> {
        const headers = new HttpHeaders()
            .append('Content-Type', 'application/json')
            .append('Access-Control-Allow-Methods', '*');
            return this.httpClient.get(this.connectionstring + 'api/auth/confirm/' + id, { headers }).pipe(
            map((res: any) => {
                if (res.err == 'err') {
                    return false;
                }
                return true;
            }));
    }

    public sendResetPasswordEmail(username: string): Observable<any> {
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
    }

    public resetPassword(body: ResetPasswordDTO): Observable<any> {
        const headers = new HttpHeaders()
            .append('Content-Type', 'application/json')
            .append('Access-Control-Allow-Methods', '*');
            return this.httpClient.post(this.connectionstring + 'api/auth/reset', body, { headers }).pipe(
            map((res: any) => {
                if (res.err == 'err') {
                    return false;
                }
                return true;
            }));
    }

    public logout() {
        localStorage.clear();
        return this.httpClient.get('auth/logout');
    }
}
