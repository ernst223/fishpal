import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ClothesOrderComponent } from './clothes-order.component';

describe('ClothesOrderComponent', () => {
  let component: ClothesOrderComponent;
  let fixture: ComponentFixture<ClothesOrderComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ClothesOrderComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ClothesOrderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
