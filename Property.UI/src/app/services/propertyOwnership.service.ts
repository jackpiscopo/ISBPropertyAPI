import { Injectable } from '@angular/core';
import { PropertyOwnership } from '../models/propertyOwnership';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PropertyOwnershipService {

  private url = "Ownership";
  constructor(private http: HttpClient) { }

  public getPropertyOwnerships(): Observable<PropertyOwnership[]> {
    return this.http.get<PropertyOwnership[]>(`${environment.apiUrl}/${this.url}`);
  }

  public updatePropertyOwnership(propertyOwnership: PropertyOwnership): Observable<PropertyOwnership[]> {
    return this.http.put<PropertyOwnership[]>(
      `${environment.apiUrl}/${this.url}`,
      propertyOwnership
    );
  }

  public createPropertyOwnership(propertyOwnership: PropertyOwnership): Observable<PropertyOwnership[]> {
    return this.http.post<PropertyOwnership[]>(
      `${environment.apiUrl}/${this.url}`,
      propertyOwnership
    );
  }

  public deletePropertyOwnership(propertyOwnership: PropertyOwnership): Observable<PropertyOwnership[]> {
    return this.http.delete<PropertyOwnership[]>(
      `${environment.apiUrl}/${this.url}/${propertyOwnership.id}`
    );
  }
}
