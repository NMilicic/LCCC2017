import { Component, OnInit, Input } from '@angular/core';
import { EndGame, NewAchievements } from '../models/models'

@Component({
  selector: 'app-score',
  templateUrl: './score.component.html',
  styleUrls: ['./score.component.css']
})
export class ScoreComponent implements OnInit {
@Input() totalScore: EndGame = new EndGame();
  constructor() { }

  ngOnInit() {
    this.totalScore.NewAchievements = new NewAchievements();
  }

  refresh(): void {
    window.location.reload();
  }

}
