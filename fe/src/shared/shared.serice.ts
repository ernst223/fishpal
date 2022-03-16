import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { ClubDTO, FacetDTO } from './shared.models';


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
}
