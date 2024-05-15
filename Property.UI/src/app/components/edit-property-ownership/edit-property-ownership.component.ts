import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { PropertyOwnership } from '../../models/propertyOwnership';
import { PropertyOwnershipService } from '../../services/propertyOwnership.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { PropertyService } from '../../services/property.service';
import { ContactService } from '../../services/contact.service';

@Component({
  selector: 'app-edit-property-ownership',
  templateUrl: './edit-property-ownership.component.html',
  styleUrls: ['./edit-property-ownership.component.css']
})
export class EditPropertyOwnershipComponent implements OnInit {
  @Input() propertyOwnership?: PropertyOwnership;
  @Output() propertyOwnershipsUpdated = new EventEmitter<PropertyOwnership[]>();

  form: FormGroup;
  propertyDropdownOptions: any[] = [];
  selectedPropertyId: string = "";
  contactDropdownOptions: any[] = [];
  selectedContactId: string = "";

  constructor(private fb: FormBuilder, private propertyOwnershipService: PropertyOwnershipService, private propertyService: PropertyService, private contactService: ContactService) {
      this.form = this.fb.group({
        selectedPropertyId: [null] // Initialize the dropdown with null
      });
    this.form = this.fb.group({
      selectedContactId: [null] // Initialize the dropdown with null
    });
  }

  ngOnInit(): void {
    this.propertyService.getPropertiesDropdownList().subscribe(
      options => {
        this.propertyDropdownOptions = options; // Populate the dropdown options
      },
      error => console.error('Error fetching dropdown options:', error)
    );
    this.contactService.getContactsDropdownList().subscribe(
      options => {
        this.contactDropdownOptions = options; // Populate the dropdown options
      },
      error => console.error('Error fetching dropdown options:', error)
    );
  }

  // This function updates the actual property ID based on selection
  updatePropertyId(): void {
    if (this.propertyOwnership != undefined) {
      if (this.selectedPropertyId === 'Other') {
        // When 'Other' is selected, 'propertyOwnership.propertyId' can be edited freely by the user
        this.propertyOwnership.propertyId = '';  // Reset or allow input
      } else {
        // Any other selection directly updates the property ID
        this.propertyOwnership.propertyId = this.selectedPropertyId;
      }
    }
  }

  // This function updates the actual property ID based on selection
  updateContactId(): void {
    if (this.propertyOwnership != undefined) {
      if (this.selectedContactId === 'Other') {
        // When 'Other' is selected, 'propertyOwnership.propertyId' can be edited freely by the user
        this.propertyOwnership.contactId = '';  // Reset or allow input
      } else {
        // Any other selection directly updates the property ID
        this.propertyOwnership.contactId = this.selectedContactId;
      }
    }
  }

  updatePropertyOwnership(propertyOwnership: PropertyOwnership) {
    console.log(propertyOwnership);
    this.propertyOwnershipService
      .updatePropertyOwnership(propertyOwnership)
      .subscribe((propertyOwnerships: PropertyOwnership[]) => this.propertyOwnershipsUpdated.emit(propertyOwnerships));
  }

  deletePropertyOwnership(propertyOwnership: PropertyOwnership) {
    console.log(propertyOwnership);
    this.propertyOwnershipService
      .deletePropertyOwnership(propertyOwnership)
      .subscribe((propertyOwnerships: PropertyOwnership[]) => this.propertyOwnershipsUpdated.emit(propertyOwnerships));
  }

  createPropertyOwnership(propertyOwnership: PropertyOwnership) {
    console.log(propertyOwnership);
    this.propertyOwnershipService
      .createPropertyOwnership(propertyOwnership)
      .subscribe((propertyOwnership: PropertyOwnership[]) => this.propertyOwnershipsUpdated.emit(propertyOwnership));
  }

}
