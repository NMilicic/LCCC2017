import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service'

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.css']
})
export class SideBarComponent implements OnInit {
  UserName: string;

 constructor(private userservice: UserService) {
    userservice.username.subscribe(model => {
      this.UserName = model.Name;
    })
  }

  ngOnInit() {
    this.UserName = localStorage.getItem('username');
  }

}
