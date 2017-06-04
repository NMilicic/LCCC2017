import { Component, OnInit } from '@angular/core';
import { QuestionService } from '../question.service';
import { Question } from '../models/question';

@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.css'],
  providers: [QuestionService]
})
export class QuestionComponent implements OnInit {
  questions: Question[];
  errorMessage: string
  constructor(private questionService: QuestionService) { }

  ngOnInit() {
    this.questionService.getQuestions()
      .subscribe(
      heroes => {
        this.questions = heroes
      },
      error => this.errorMessage = <any>error);
  }


}
