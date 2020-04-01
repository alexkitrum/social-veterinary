import {Component, OnInit, Input} from '@angular/core';
import {ToastrService} from '../../core/services/toastr.service';
import {PetsService} from '../../core/services/pets-service';
import {ActivatedRoute} from '@angular/router';
import {IPet} from '../../shared/interfaces/pet.model';

@Component({
  selector: 'pets-list',
  templateUrl: './pets-list.component.html',
})
export class PetsListComponent implements OnInit {
  pets: IPet[];
  personId: number;
  constructor(private petsService: PetsService, private toastr: ToastrService, private route: ActivatedRoute) {
    this.personId = +this.route.snapshot.params.id;
  }

  onAdded(id) {
    console.log(id);
    this.reloadList();
  }

  ngOnInit() {
    this.reloadList();
  }

  reloadList() {
    this.petsService.getPetsForPerson(this.personId).subscribe(data => {
      this.pets = data;
    });
  }
}
