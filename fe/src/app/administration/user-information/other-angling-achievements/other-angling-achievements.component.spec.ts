import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OtherAnglingAchievementsComponent } from './other-angling-achievements.component';

describe('OtherAnglingAchievementsComponent', () => {
  let component: OtherAnglingAchievementsComponent;
  let fixture: ComponentFixture<OtherAnglingAchievementsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OtherAnglingAchievementsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OtherAnglingAchievementsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
