import { Injectable } from '@angular/core';
import { Contact } from '../models/contact';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ContactService {

  private url = "Contact";
  constructor(private http: HttpClient) { }

  public getContacts(): Observable<Contact[]> {
    return this.http.get<Contact[]>(`${environment.apiUrl}/${this.url}`);
  }

  public updateContact(contact: Contact): Observable<Contact[]> {
    return this.http.put<Contact[]>(
      `${environment.apiUrl}/${this.url}`,
      contact
    );
  }

  public createContact(contact: Contact): Observable<Contact[]> {
    return this.http.post<Contact[]>(
      `${environment.apiUrl}/${this.url}`,
      contact
    );
  }

  public deleteContact(contact: Contact): Observable<Contact[]> {
    return this.http.delete<Contact[]>(
      `${environment.apiUrl}/${this.url}/${contact.id}`
    );
  }

  public getContactsDropdownList(): Observable<String[]> {
    return this.http.get<String[]>(`${environment.apiUrl}/${this.url}/contactsDropdown`);
  }
}
