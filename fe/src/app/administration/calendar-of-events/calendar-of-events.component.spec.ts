import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CalendarOfEventsComponent } from './calendar-of-events.component';

describe('CalendarOfEventsComponent', () => {
  let component: CalendarOfEventsComponent;
  let fixture: ComponentFixture<CalendarOfEventsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ CalendarOfEventsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CalendarOfEventsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
