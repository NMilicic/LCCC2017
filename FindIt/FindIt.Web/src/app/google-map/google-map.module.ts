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

@NgModule({
    declarations: [
        GoogleMapComponent,
        StartGameComponent,
        ConfirmAnswerComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        SideBarModule,
        AgmCoreModule.forRoot({
            apiKey: 'AIzaSyCeX9sulgeOKOq6cNfXhGCdQhWZzqjNQXs'
        })
    ],

    exports: [GoogleMapComponent],
    bootstrap: [StartGameComponent, ConfirmAnswerComponent],
    providers: [UserService, ShowHideService, QuestionService]
})
export class GoogleMapModule { }