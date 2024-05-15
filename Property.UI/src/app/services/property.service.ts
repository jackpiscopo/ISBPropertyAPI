import { Injectable } from '@angular/core';
import { Property } from '../models/property';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PropertyService {

  private url = "Property";
  constructor(private http: HttpClient) { }

  public getProperties(): Observable<Property[]> {
    return this.http.get<Property[]>(`${environment.apiUrl}/${this.url}`);
  }

  public updateProperty(property: Property): Observable<Property[]> {
    return this.http.put<Property[]>(
      `${environment.apiUrl}/${this.url}`,
      property
    );
  }

  public createProperty(property: Property): Observable<Property[]> {
    return this.http.post<Property[]>(
      `${environment.apiUrl}/${this.url}`,
      property
    );
  }

  public deleteProperty(property: Property): Observable<Property[]> {
    return this.http.delete<Property[]>(
      `${environment.apiUrl}/${this.url}/${property.propertyId}`
    );
  }

  public getPropertiesDropdownList(): Observable<String[]> {
    return this.http.get<String[]>(`${environment.apiUrl}/${this.url}/propertiesDropdown`);
  }
}
