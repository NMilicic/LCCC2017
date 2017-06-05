import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { User } from './models/user';

@Injectable()
export class UserService {

  private userName = new Subject<User>();

  username = this.userName.asObservable();

  userNameReceived(username: User) {
    this.userName.next(username);
  }


}
