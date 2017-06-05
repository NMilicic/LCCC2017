import { Component, OnInit, Input } from '@angular/core';
import { UserService } from '../user.service'
import { QuestionService } from '../question.service';
import { Question } from '../models/question';
import { trigger,state,style,transition,animate,keyframes } from '@angular/animations';

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.css'],
  providers: [QuestionService]
})
export class SideBarComponent implements OnInit {
  UserName: string;
  @Input() questions: Question[];
  @Input() currentQuestion: Question;
  @Input() showQuestionDetails: boolean;
  errorMessage: string;

  constructor(private userservice: UserService, private questionService: QuestionService) {
    userservice.username.subscribe(model => {
      this.UserName = model.Name;


    })
  }

  ngOnInit() {
    this.UserName = localStorage.getItem('username');
  }

}
