import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AccountRegistrationRoutingModule } from './account-registration-routing.module';
import { RegisterSellerComponent } from './register-seller/register-seller.component';
import { AccountRegistrationComponent } from './account-registration.component';
import { NgxCaptchaModule } from 'ngx-captcha';

@NgModule({
  declarations: [RegisterSellerComponent, AccountRegistrationComponent],
  imports: [
    CommonModule,
    AccountRegistrationRoutingModule,
    NgxCaptchaModule
  ]
})
export class AccountRegistrationModule { }
