import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Contact } from '../../models/contact';
import { ContactService } from '../../services/contact.service';

@Component({
  selector: 'app-edit-contact',
  templateUrl: './edit-contact.component.html',
  styleUrls: ['./edit-contact.component.css']
})
export class EditContactComponent implements OnInit {
  @Input() contact?: Contact;
  @Output() contactsUpdated = new EventEmitter<Contact[]>();

  constructor(private contactService: ContactService) {

  }

  ngOnInit(): void {

  }

  updateContact(contact: Contact) {
    console.log(contact);
    this.contactService
      .updateContact(contact)
      .subscribe((contacts: Contact[]) => this.contactsUpdated.emit(contacts));
  }

  deleteContact(contact: Contact) {
    console.log(contact);
    this.contactService
      .deleteContact(contact)
      .subscribe((contacts: Contact[]) => this.contactsUpdated.emit(contacts));
  }

  createContact(contact: Contact) {
    console.log(contact);
    this.contactService
      .createContact(contact)
      .subscribe((contacts: Contact[]) => this.contactsUpdated.emit(contacts));
  }

}
