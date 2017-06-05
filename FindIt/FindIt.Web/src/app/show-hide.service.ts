import { Injectable } from '@angular/core';
import { Subject } from 'rxjs/Subject';

@Injectable()
export class ShowHideService {

  constructor() { }

  private showConfirmDialog = new Subject<boolean>();
  private confirmAnswer = new Subject<number>();
  private showCorrectAnswer = new Subject<boolean>();
  private evaluate = new Subject<boolean>();

  showConfirmDialog$ = this.showConfirmDialog.asObservable();
  showCorrectAnswer$ = this.showCorrectAnswer.asObservable();
  evaluate$ = this.evaluate.asObservable();
  confirmAnswer$ = this.confirmAnswer.asObservable();

  showHideConfirmaDialog(state: boolean) {
    this.showConfirmDialog.next(state);
  }

  showCorrectAnswerDialog(state: boolean) {
    this.showCorrectAnswer.next(state);
  }

  confirmAnswerDialog(questionNumber: number) {
    this.confirmAnswer.next(questionNumber);
  }

   evaluateFn(state: boolean) {
    this.evaluate.next(state);
  }
}
