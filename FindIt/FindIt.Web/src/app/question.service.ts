import { Injectable } from '@angular/core';
import { Question } from './models/question';

import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

import {Marker} from './models/marker'

@Injectable()
export class QuestionService {
  private questionsUrl = 'http://llamasfindit.azurewebsites.net/api/game/newgame';
  constructor(private http: Http) { }

  getQuestions(): Observable<Question[]> {
    return this.http.get(this.questionsUrl)
      .map(this.extractData)
      .catch(this.handleError);
  }

  private extractData(res: Response) {
    let body = res.json();


    body.Questions.forEach((element, index) => {
      element.Order = index + 1 ;
    });
    return body.Questions || {};
  }
  private handleError(error: Response | any) {
    let errMsg: string;
    if (error instanceof Response) {
      const body = error.json() || '';
      const err = body.error || JSON.stringify(body);
      errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
    } else {
      errMsg = error.message ? error.message : error.toString();
    }
    console.error(errMsg);
    return Observable.throw(errMsg);
  }

  saveAnswer(marker: Marker){
    
  }

}
