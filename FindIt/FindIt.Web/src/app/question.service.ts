import { Injectable } from '@angular/core';
import { Game } from './models/models';

import { Http, Response, RequestOptions, Headers, URLSearchParams  } from '@angular/http';
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
    let model = {
      GameId: game.GameId,
      Latitudes: [],
      Longitudes: [],
      Hints1Used: [],
      Hints2Used: [],
      Username:game.Username
    }
    game.Questions.forEach(question => {
      model.Latitudes.push(question.Latitude)
      model.Longitudes.push(question.Longitude)
      model.Hints1Used.push(question.UsedFirstHint ? true : false)
      model.Hints2Used.push(question.UsedSecondHint? true : false)
    })
  

console.log(model);
debugger;
    this.http.post('https://llamasfindit.azurewebsites.net/api/game/submitgame', { model }, headers)
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
