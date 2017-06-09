import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AgmCoreModule } from '@agm/core';
import { FormsModule } from '@angular/forms';
import { GoogleMapComponent } from './google-map.component';
import { StartGameComponent } from '../start-game/start-game.component';
import { ConfirmAnswerComponent } from '../confirm-answer/confirm-answer.component';
import { UserService } from '../user.service'
import { ShowHideService } from '../show-hide.service'
import { SideBarModule } from '../side-bar/side-bar.module'
import { QuestionService } from '../question.service';
import { ScoreComponent } from '../score/score.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
    declarations: [
        GoogleMapComponent,
        StartGameComponent,
        ConfirmAnswerComponent,
        ScoreComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        SideBarModule,
        BrowserAnimationsModule,
        AgmCoreModule.forRoot({
            apiKey: '{Google Maps API Key}'
        })
    ],

    exports: [GoogleMapComponent],
    bootstrap: [StartGameComponent, ConfirmAnswerComponent, ScoreComponent],
    providers: [UserService, ShowHideService, QuestionService]
})
export class GoogleMapModule { }