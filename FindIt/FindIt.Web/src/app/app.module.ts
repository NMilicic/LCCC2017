import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { GoogleMapModule} from './google-map/google-map.module';
import { SideBarModule} from './side-bar/side-bar.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    GoogleMapModule,
    SideBarModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
