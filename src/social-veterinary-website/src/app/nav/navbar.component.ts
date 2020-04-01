import {Component} from '@angular/core';


@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
  styles: [`
  .dropdown-nav-link{
    cursor: pointer
  }
  a.active{
    font-weight: bold;
  }
  `]
})
export class NavBarComponent {

}
