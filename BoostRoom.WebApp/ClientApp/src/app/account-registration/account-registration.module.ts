import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { AccountRegistrationRoutingModule } from './account-registration-routing.module';
import { RegisterSellerComponent } from './register-seller/register-seller.component';
import { AccountRegistrationComponent } from './account-registration.component';
import { NgxCaptchaModule } from 'ngx-captcha';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { RegisterClientComponent } from './register-client/register-client.component';
import { RegistrationConfirmationComponent } from './registration-confirmation/registration-confirmation.component';

@NgModule({
  declarations: [RegisterSellerComponent, AccountRegistrationComponent, RegisterClientComponent, RegistrationConfirmationComponent],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    AccountRegistrationRoutingModule,
    NgxCaptchaModule
  ]
})
export class AccountRegistrationModule { }
