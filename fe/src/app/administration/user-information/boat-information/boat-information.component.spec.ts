import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BoatInformationComponent } from './boat-information.component';

describe('BoatInformationComponent', () => {
  let component: BoatInformationComponent;
  let fixture: ComponentFixture<BoatInformationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BoatInformationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BoatInformationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
