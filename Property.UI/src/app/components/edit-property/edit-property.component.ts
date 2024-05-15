import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Property } from '../../models/property';
import { PropertyService } from '../../services/property.service';

@Component({
  selector: 'app-edit-property',
  templateUrl: './edit-property.component.html',
  styleUrls: ['./edit-property.component.css']
})
export class EditPropertyComponent implements OnInit {
  @Input() property?: Property;
  @Output() propertiesUpdated = new EventEmitter<Property[]>();

  constructor(private propertyService: PropertyService) {

  }

  ngOnInit(): void {
        
  }

  updateProperty(property: Property) {
    console.log(property);
    this.propertyService
      .updateProperty(property)
      .subscribe((properties: Property[]) => this.propertiesUpdated.emit(properties));
  }

  deleteProperty(property: Property) {
    console.log(property);
    this.propertyService
      .deleteProperty(property)
      .subscribe((properties: Property[]) => this.propertiesUpdated.emit(properties));
  }

  createProperty(property: Property) {
    console.log(property);
    this.propertyService
      .createProperty(property)
      .subscribe((properties: Property[]) => this.propertiesUpdated.emit(properties));
  }

}
