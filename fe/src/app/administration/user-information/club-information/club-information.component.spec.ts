import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ClubInformationComponent } from './club-information.component';

describe('ClubInformationComponent', () => {
  let component: ClubInformationComponent;
  let fixture: ComponentFixture<ClubInformationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ClubInformationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClubInformationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
