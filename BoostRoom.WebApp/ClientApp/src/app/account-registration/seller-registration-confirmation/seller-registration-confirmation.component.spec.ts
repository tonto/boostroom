import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SellerRegistrationConfirmationComponent } from './seller-registration-confirmation.component';

describe('SellerRegistrationConfirmationComponent', () => {
  let component: SellerRegistrationConfirmationComponent;
  let fixture: ComponentFixture<SellerRegistrationConfirmationComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SellerRegistrationConfirmationComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SellerRegistrationConfirmationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
