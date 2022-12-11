import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { VerifiedFailureComponent } from './verified-failure.component';

describe('VerifiedFailureComponent', () => {
  let component: VerifiedFailureComponent;
  let fixture: ComponentFixture<VerifiedFailureComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ VerifiedFailureComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VerifiedFailureComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
