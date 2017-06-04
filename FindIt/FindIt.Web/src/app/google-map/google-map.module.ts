import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AgmCoreModule } from '@agm/core';
import { FormsModule } from '@angular/forms';
import { GoogleMapComponent } from './google-map.component';
import { StartGameComponent } from '../start-game/start-game.component';
import { UserService } from '../user.service'

@NgModule({
    declarations: [
        GoogleMapComponent,
        StartGameComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
        AgmCoreModule.forRoot({
            apiKey: 'AIzaSyCeX9sulgeOKOq6cNfXhGCdQhWZzqjNQXs'
        })
    ],

    exports: [GoogleMapComponent],
    bootstrap: [StartGameComponent],
    providers: [UserService]
})
export class GoogleMapModule { }