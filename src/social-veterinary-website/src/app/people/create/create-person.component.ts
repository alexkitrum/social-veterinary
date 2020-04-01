import {Component, OnInit} from '@angular/core';
import {PeopleService} from '../../core/services/people-service';
import {Router} from '@angular/router';
import {FormGroup, FormControl, Validators} from '@angular/forms';

@Component({
  selector: 'create-person',
  templateUrl: './create-person.component.html',
  styles: [`
  em{float: right; color: #E05C65; padding-left:10px;}
  .controls .btn-secondary {margin-left: 10px}
  `]
})
export class CreatePersonComponent implements OnInit {
  personForm: FormGroup;
  mouseoverSubmit: boolean;
  get isDirty(): boolean {
    return this.personForm.dirty;
  }

  constructor(private peopleService: PeopleService, private router: Router) {
  }

  ngOnInit(): void {
    const name = new FormControl('', Validators.required);
    const lastName = new FormControl('', Validators.required);
    const isEmployee = new FormControl(false);

    this.personForm = new FormGroup({
      name, lastName, isEmployee
    });
  }

  save(person) {
    if (this.personForm.valid) {
      this.peopleService.add(person).subscribe(v => {
        if (v) {
          this.personForm.markAsPristine();
          this.router.navigate(['/people']);
        }
      });
    }
  }

  cancel() {
    this.router.navigate(['/people']);
  }
}
