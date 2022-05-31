import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProvincialInformationComponent } from './provincial-information.component';

describe('ProvincialInformationComponent', () => {
  let component: ProvincialInformationComponent;
  let fixture: ComponentFixture<ProvincialInformationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProvincialInformationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProvincialInformationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
