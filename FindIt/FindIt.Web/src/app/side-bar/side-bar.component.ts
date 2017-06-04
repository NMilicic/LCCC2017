import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service'
import { QuestionService } from '../question.service';
import { Question } from '../models/question';

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.css'],
  providers: [QuestionService]
})
export class SideBarComponent implements OnInit {
  UserName: string;
  questions: Question[];
  currentQuestion: Question;
  errorMessage: string;

  constructor(private userservice: UserService, private questionService: QuestionService) {
    userservice.username.subscribe(model => {
      this.UserName = model.Name;
      this.questionService.getQuestions()
        .subscribe(
        heroes => {
          this.questions = heroes
          this.currentQuestion = this.questions[0];
        },
        error => this.errorMessage = <any>error);

    })
  }

  ngOnInit() {
    this.UserName = localStorage.getItem('username');
  }

}
