import { Component } from '@angular/core';
import { Property } from './models/property';
import { PropertyService } from './services/property.service';
import { Contact } from './models/contact';
import { ContactService } from './services/contact.service';
import { PropertyOwnership } from './models/propertyOwnership';
import { PropertyOwnershipService } from './services/propertyOwnership.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Property.UI';
  properties: Property[] = [];
  propertyToEdit?: Property;
  contacts: Contact[] = [];
  contactToEdit?: Contact;
  propertyOwnerships: PropertyOwnership[] = [];
  propertyOwnershipToEdit?: PropertyOwnership;

  constructor(private propertyService: PropertyService, private contactService: ContactService, private propertyOwnershipService: PropertyOwnershipService) { }

  ngOnInit(): void {
    this.propertyService
      .getProperties()
      .subscribe((result: Property[]) => this.properties = result);
    this.contactService
      .getContacts()
      .subscribe((result: Contact[]) => this.contacts = result);
    this.propertyOwnershipService
      .getPropertyOwnerships()
      .subscribe((result: PropertyOwnership[]) => this.propertyOwnerships = result);
  }

  updatePropertyList(properties: Property[]) {
    this.properties = properties;
  }

  initNewProperty() {
    this.propertyToEdit = new Property();
  }

  editProperty(property: Property) {
    this.propertyToEdit = property;
  }

  updateContactList(contacts: Contact[]) {
    this.contacts = contacts;
  }

  initNewContact() {
    this.contactToEdit = new Contact();
  }

  editContact(contact: Contact) {
    this.contactToEdit = contact;
  }

  updatePropertyOwnershipList(propertyOwnerships: PropertyOwnership[]) {
    this.propertyOwnerships = propertyOwnerships;
  }

  initNewPropertyOwnership() {
    this.propertyOwnershipToEdit = new PropertyOwnership();
  }

  editPropertyOwnership(propertyOwnership: PropertyOwnership) {
    this.propertyOwnershipToEdit = propertyOwnership;
  }
}
