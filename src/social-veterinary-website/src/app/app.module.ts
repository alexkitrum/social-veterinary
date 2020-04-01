import {BrowserModule} from '@angular/platform-browser';

// Modules
import {NgModule} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {HttpClientModule} from '@angular/common/http';
import {NgbModule, NgbDropdownModule} from '@ng-bootstrap/ng-bootstrap';

import {AppRoutingModule} from './app-routing.module';

// Components
import {AppComponent} from './app.component';
import {NavBarComponent} from './nav/navbar.component';

import {
  PeopleListComponent,
  PersonDetailsComponent,
  CreatePersonComponent,
  PeopleListResolver
} from './people';

import {
  PetsListComponent,
  CreatePetComponent
} from './people/pets';

import {Error404Component} from './errors/404.components';

// Services
import {ToastrService} from './core/services/toastr.service';
import {PeopleService } from './core/services/people-service';
import {PetsService } from './core/services/pets-service';

@NgModule({
  declarations: [
    AppComponent,
    NavBarComponent,
    // People
    PeopleListComponent,
    PersonDetailsComponent,
    CreatePersonComponent,
    // Pets
    PetsListComponent,
    CreatePetComponent,
    // Errors
    Error404Component

    // Pipes
  ],
  imports: [

    BrowserModule,
    AppRoutingModule,
    NgbModule,
    NgbDropdownModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [
    // Services
    ToastrService,
    PeopleService,
    PeopleListResolver,
    PetsService,
    // Custom functions
    {
      provide: 'canDeactivateCreateEvent', useValue: checkDirtyState
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule {}

export function checkDirtyState(component: CreatePersonComponent) {

  if (component.isDirty) {
    return window.confirm('You have pending changes, do you really want to cancel?');
  }
  return true;
}
