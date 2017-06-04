import { Component, OnInit, Input } from '@angular/core';
import { Question } from '../models/question';

@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.css']
})
export class QuestionComponent implements OnInit {
  @Input() private questions: Question[];
  @Input() private currentQuestion: Question = new Question();

  constructor() { }

  ngOnInit() { 
    if(!this.currentQuestion)
      this.currentQuestion = new Question();
  }



}
