import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { SideBarComponent} from './side-bar.component'
import { QuestionComponent} from '../question/question.component'
import { UserService } from '../user.service'
import { QuestionService } from '../question.service'

@NgModule({
    declarations: [
        SideBarComponent,
        QuestionComponent
    ],
    imports: [
        BrowserModule,
        FormsModule,
    ],

    exports: [SideBarComponent],
    bootstrap: [QuestionComponent],
    providers: [UserService, QuestionService]
})
export class SideBarModule { }