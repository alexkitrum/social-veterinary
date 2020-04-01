import {Component, OnInit} from '@angular/core';
import {ToastrService} from '../core/services/toastr.service';
import {PeopleService} from '../core/services/people-service';
import {ActivatedRoute} from '@angular/router';
import {IPerson} from '../shared/interfaces/person.model';

const ROUTE_PEOPLE_DATA_PARAMETER = 'people';

@Component({
  selector: 'people-list',
  templateUrl: './people-list.component.html',
})
export class PeopleListComponent implements OnInit {
  people: IPerson[];
  constructor(private peopleService: PeopleService, private toastr: ToastrService, private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.people = this.route.snapshot.data[ROUTE_PEOPLE_DATA_PARAMETER];
  }
}
