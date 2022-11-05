import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ProteaColorsComponent } from './protea-colors.component';

describe('ProteaColorsComponent', () => {
  let component: ProteaColorsComponent;
  let fixture: ComponentFixture<ProteaColorsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ProteaColorsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ProteaColorsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
