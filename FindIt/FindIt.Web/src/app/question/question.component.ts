import { Component, OnInit, Input } from '@angular/core';
import { Question } from '../models/question';

@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.css']
})
export class QuestionComponent implements OnInit {
  @Input() questions: Question[];
  @Input() currentQuestion: Question = new Question();
  @Input() showQuestionDetails: boolean;

  constructor() { }

  ngOnInit() {
    if (!this.currentQuestion)
      this.currentQuestion = new Question();
  }

  UsedFirstHint() {
    var tmp = this.questions.find(x => x == this.currentQuestion)
    tmp.UsedFirstHint = true;
    this.currentQuestion.UsedFirstHint = true;
  }

    UsedSecondHint() {
    var tmp = this.questions.find(x => x == this.currentQuestion)
    tmp.UsedSecondHint = true;
    this.currentQuestion.UsedSecondHint = true;
  }
}
