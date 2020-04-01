import {Component, OnInit} from '@angular/core';
import {ToastrService} from '../../core/services/toastr.service';
import {PeopleService} from '../../core/services/people-service';
import {ActivatedRoute, Router} from '@angular/router';
import {IPerson} from 'src/app/shared/interfaces/person.model';

@Component({
  selector: 'person-details',
  templateUrl: './person-details.component.html',
  styles: [`
  .details-container{
     padding: 20px;
    }
  .pets-list{
    padding: 30px
  }
  `]
})
export class PersonDetailsComponent implements OnInit {
  data: IPerson;
  viewMode = true;

  constructor(private peopleService: PeopleService, private toastr: ToastrService,
              private route: ActivatedRoute, private router: Router) {
  }

  ngOnInit() {
    const id: number = +this.route.snapshot.params.id;
    this.peopleService.getById(id).subscribe(data => {
      this.data = data;
    });
  }

  backToList() {
    this.router.navigate(['/people']);
  }
}
