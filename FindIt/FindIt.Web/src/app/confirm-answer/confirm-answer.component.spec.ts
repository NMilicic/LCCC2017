import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ConfirmAnswerComponent } from './confirm-answer.component';

describe('ConfirmAnswerComponent', () => {
  let component: ConfirmAnswerComponent;
  let fixture: ComponentFixture<ConfirmAnswerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ConfirmAnswerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ConfirmAnswerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should be created', () => {
    expect(component).toBeTruthy();
  });
});
