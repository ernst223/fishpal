import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GeoProvinceInformationComponent } from './geo-province-information.component';

describe('GeoProvinceInformationComponent', () => {
  let component: GeoProvinceInformationComponent;
  let fixture: ComponentFixture<GeoProvinceInformationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GeoProvinceInformationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GeoProvinceInformationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
