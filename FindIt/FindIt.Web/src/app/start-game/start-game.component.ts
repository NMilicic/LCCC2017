import { Component, EventEmitter, Output, Input } from '@angular/core';
import { User } from '../models/user';
import { UserService } from '../user.service'
import { Subscription } from 'rxjs/Subscription';

@Component({
  selector: 'app-start-game',
  templateUrl: './start-game.component.html',
  styleUrls: ['./start-game.component.css']
})
export class StartGameComponent {
  @Output() notify: EventEmitter<boolean> = new EventEmitter<boolean>();

  constructor(private userservice: UserService) {
    userservice.username.subscribe(model => {
      this.model = model;
    })
  }

  model = new User();
  submitted =  false;
  onSubmit() {
    this.userservice.userNameReceived(this.model);
      localStorage.setItem('username', this.model.Name);
      this.submitted = true;
      this.notify.emit(true);

  }
  ngOnInit() {
    this.model.Name = localStorage.getItem('username');
  }

}