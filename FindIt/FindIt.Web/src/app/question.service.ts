import { Injectable } from '@angular/core';
import { Game } from './models/models';

import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';

import { Marker } from './models/marker'

@Injectable()
export class QuestionService {
  private questionsUrl = 'https://llamasfindit.azurewebsites.net/api/game/newgame';
  constructor(private http: Http) { }

  getQuestions(): Observable<Game> {
    return this.http.get(this.questionsUrl)
      .map(this.extractData)
      .catch(this.handleError);
  }

  private extractData(res: Response) {
    let body = res.json();


    body.Questions.forEach((element, index) => {
      element.Order = index + 1;
    });
    return body || {};
  }


  evaluateGame(game: Game) {
    let headers = new Headers({ 'Content-Type': 'application/json' });
    let tmp = {
      GameId: game.GameId,
      Latitudes: "",
      Longitudes: "",
      Hints1Used: "",
      Hints2Used: ""
    }
    game.Questions.forEach(question => {
      tmp.Latitudes += question.AnswerLatitude + ","
      tmp.Longitudes += question.AnswerLongitude + ","
      tmp.Hints1Used += (question.UsedFirstHint ? "True" : "False") + ","
      tmp.Hints2Used += (question.UsedSecondHint ? "True" : "False") + ","
    })
    tmp.Latitudes = tmp.Latitudes.substring(0, tmp.Latitudes.length - 1);
    tmp.Longitudes = tmp.Longitudes.substring(0, tmp.Longitudes.length - 1);
    tmp.Hints1Used = tmp.Hints1Used.substring(0, tmp.Hints1Used.length - 1);
    tmp.Hints2Used = tmp.Hints2Used.substring(0, tmp.Hints2Used.length - 1);
    debugger;
    this.http.post('https://llamasfindit.azurewebsites.net/api/game/submitgame', { tmp }, headers)
      .map(res => res.json())
      .subscribe(
      data => console.log(data),
      err => this.handleError
      );
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

  saveAnswer(marker: Marker) {

  }

}
