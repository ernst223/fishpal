import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AnglingAdministrationHistoryComponent } from './angling-administration-history.component';

describe('AnglingAdministrationHistoryComponent', () => {
  let component: AnglingAdministrationHistoryComponent;
  let fixture: ComponentFixture<AnglingAdministrationHistoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AnglingAdministrationHistoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AnglingAdministrationHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
