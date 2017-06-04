import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { GoogleMapModule} from './google-map/google-map.module';
import { SideBarComponent } from './side-bar/side-bar.component';
import { QuestionComponent } from './question/question.component';


@NgModule({
  declarations: [
    AppComponent,
    SideBarComponent,
    QuestionComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    GoogleMapModule
  ],
  providers: [],
  bootstrap: [AppComponent, SideBarComponent]
})
export class AppModule { }
