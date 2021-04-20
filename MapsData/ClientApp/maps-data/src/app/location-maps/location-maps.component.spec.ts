import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { LocationMapsComponent } from './location-maps.component';

describe('LocationMapsComponent', () => {
  let component: LocationMapsComponent;
  let fixture: ComponentFixture<LocationMapsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ LocationMapsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(LocationMapsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
