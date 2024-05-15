import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { EditPropertyComponent } from './components/edit-property/edit-property.component';
import { FormsModule } from '@angular/forms';
import { EditContactComponent } from './components/edit-contact/edit-contact.component';
import { EditPropertyOwnershipComponent } from './components/edit-property-ownership/edit-property-ownership.component';

@NgModule({
  declarations: [
    AppComponent,
    EditPropertyComponent,
    EditContactComponent,
    EditPropertyOwnershipComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
