import { Component, OnInit } from '@angular/core';
import { AgmMap, AgmMarker } from '@agm/core';
import { User } from '../models/user'
import { Marker, MapMouseEvent } from '../models/marker'
import { Question } from '../models/question'
import { Game } from '../models/models'
import { ShowHideService } from '../show-hide.service'
import { QuestionService } from '../question.service';


@Component({
  selector: 'app-google-map',
  templateUrl: './google-map.component.html',
  styleUrls: ['./google-map.component.css']
})
export class GoogleMapComponent implements OnInit {
  lat: number = 51.678418;
  lng: number = 7.809007;
  markers: Marker[] = [];
  zoom: number = 2;
  gameStarted: boolean;
  showConfirmationDialog: boolean = false;
  game: Game = new Game();
  currentQuestion: Question;
  errorMessage: string;
  activeQuestionIndex: number;
  temp: number;
  showQuestionDetails: boolean = false;
  isAnswered: boolean;
  showFinishGame: boolean;

  constructor(private showHideService: ShowHideService, private questionService: QuestionService) {
    showHideService.showConfirmDialog$.subscribe(
      data => {
        this.showConfirmationDialog = data;
      });

    showHideService.confirmAnswer$.subscribe(
      data => {
        this.temp = data;
        this.isAnswered = false;
        this.showQuestionDetails = false;
        this.markers = [];
        if (data < this.game.Questions.length) {
          this.currentQuestion = this.game.Questions[data]
          this.showHideDialog(false);

        }
        else {
          this.showFinishGame = true;
          this.isAnswered = true;
          this.showQuestionDetails = true;
        }
      });

    showHideService.showCorrectAnswer$.subscribe(
      data => {
        this.showCorrectAnswer()
      });

    showHideService.evaluate$.subscribe(
      data => {
        this.showHideDialog(false);
        this.EvaluateGame();
      });
  }


  ngOnInit() {
    this.activeQuestionIndex = 1;
  }
  mapClicked($event: MapMouseEvent) {
    this.markers = [];
    this.markers.push({
      lat: $event.coords.lat,
      lng: $event.coords.lng,
      draggable: true,
      title: ''
    });
    this.showHideDialog(true);
  }

  markerDragEnd(m, $event) {
    this.showHideDialog(true);
  }

  onNotify(message: boolean): void {
    this.gameStarted = true;
    this.questionService.getQuestions()
      .subscribe(
      data => {
        this.game = data
        this.currentQuestion = this.game.Questions[0];
      },
      error => this.errorMessage = <any>error);
  }

  showHideDialog(state: boolean) {
    this.showHideService.showHideConfirmaDialog(state);
  }

  showCorrectAnswer() {
    this.markers[0].draggable = false;
    this.currentQuestion.AnswerLatitude = this.markers[0].lat;
    this.currentQuestion.AnswerLongitude = this.markers[0].lng;

    let correctAnswerMarker = new Marker();
    correctAnswerMarker.lat = this.currentQuestion.Latitude;
    correctAnswerMarker.lng = this.currentQuestion.Longitude;
    correctAnswerMarker.draggable = false;

    this.markers.push(correctAnswerMarker)
    this.showQuestionDetails = true
    this.isAnswered = true;

  }

  clickedMarker(label, i) {
    this.markers[i].openInfoWindow = true;
  }

EvaluateGame(){
this.questionService.evaluateGame(this.game)
}

}

