import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AnglingHistoryComponent } from './angling-history.component';

describe('AnglingHistoryComponent', () => {
  let component: AnglingHistoryComponent;
  let fixture: ComponentFixture<AnglingHistoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AnglingHistoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AnglingHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
