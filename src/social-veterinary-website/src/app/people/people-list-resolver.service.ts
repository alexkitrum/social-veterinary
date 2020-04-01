import {Injectable} from '@angular/core';
import {Resolve} from '@angular/router';

import {PeopleService} from '../core/services/people-service';
import {IPerson} from '../shared/interfaces/person.model';

@Injectable()
export class PeopleListResolver implements Resolve<IPerson[]> {
  constructor(private peopleService: PeopleService) {
  }

  resolve() {
    return this.peopleService.getAll();
  }
}
