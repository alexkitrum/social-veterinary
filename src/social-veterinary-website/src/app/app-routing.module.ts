import {NgModule} from '@angular/core';
import {Routes, RouterModule} from '@angular/router';

import {PeopleListComponent, PersonDetailsComponent,
  CreatePersonComponent, PeopleListResolver} from './people';


import {Error404Component} from './errors/404.components';


const routes: Routes = [
  {path: 'people', component: PeopleListComponent, resolve: { people: PeopleListResolver}},
  {path: 'people/new', component: CreatePersonComponent, canDeactivate: ['canDeactivateCreateEvent']},
  {path: 'people/:id', component: PersonDetailsComponent},
  {path: '404', component: Error404Component}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule {}
