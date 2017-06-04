import { Component } from '@angular/core';
import { UserService} from './user.service'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  isUserDataSet : boolean = localStorage.getItem('username') ? true: false;
  title = 'app works!';

  constructor(private userservice: UserService) {
    userservice.username.subscribe(model => {
      this.isUserDataSet = true;
    })
  }
}
