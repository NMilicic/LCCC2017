import { Component, OnInit, Input } from '@angular/core';
import { ShowHideService } from '../show-hide.service'
import { Question } from '../models/question'
@Component({
  selector: 'app-confirm-answer',
  templateUrl: './confirm-answer.component.html',
  styleUrls: ['./confirm-answer.component.css']
})
export class ConfirmAnswerComponent implements OnInit {
  @Input() isShown: boolean;
  @Input() isAnswered: boolean;
  @Input() currentQuestion: Question;
  @Input() showFinishGame: boolean;
  @Input() userHasSetMarket: boolean;

  constructor(private showHideService: ShowHideService) {

    showHideService.showConfirmDialog$.subscribe(
      data => {
        this.isShown = data;
      });
  }

  ngOnInit() {
  }

  dismissDialog() {
    this.showHideService.showHideConfirmaDialog(false);
  }

  confirmAnswer() {
    this.showHideService.confirmAnswerDialog(this.currentQuestion.Order);
  }

  showCorrectAnswer() {
    this.showHideService.showCorrectAnswerDialog(true);
  }

  evaluate(){
    this.showHideService.evaluateFn(true);
  }
}
