import {Component, OnInit, EventEmitter, Output} from '@angular/core';
import {PetsService} from '../../../core/services/pets-service';
import {Router, ActivatedRoute} from '@angular/router';
import {FormGroup, FormControl, Validators} from '@angular/forms';
import {IPet} from 'src/app/shared/interfaces/pet.model';

@Component({
  selector: 'create-pet',
  templateUrl: './create-pet.component.html',
  styles: [`
  em{float: right; color: #E05C65; padding-left:10px;}
  .controls .btn-secondary {margin-left: 10px}
  `]
})

export class CreatePetComponent implements OnInit {

  @Output()
  added: EventEmitter<number> = new EventEmitter<number>();

  personId: number;
  types: string[] = ['Cat', 'Bird', 'Fish', 'Hamster', 'Dog', 'Rabbit', 'Turtle'];
  petForm: FormGroup;
  get isDirty(): boolean {
    return false;
  }

  constructor(private petsService: PetsService, private router: Router, private route: ActivatedRoute) {
  }

  ngOnInit() {
    this.personId = +this.route.snapshot.params.id;
    const name = new FormControl();
    const type = new FormControl();

    this.petForm = new FormGroup({
      name, type
    });
  }

  save(pet) {
    this.petsService.add(this.personId, pet).subscribe(v => {
      if (v) {
        this.petForm.reset();
        this.added.emit(v.id);
      }
    });
  }

  cancel() {
    this.router.navigate(['/']);
  }
}
