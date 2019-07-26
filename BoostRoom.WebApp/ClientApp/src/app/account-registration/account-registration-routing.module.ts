import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AccountRegistrationComponent } from './account-registration.component';
import { RegistrationConfirmationComponent } from './registration-confirmation/registration-confirmation.component';

const routes: Routes = [
  {
    path: 'accounts/register',
    component: AccountRegistrationComponent
  },
  {
    path: 'accounts/registration-confirmation',
    component: RegistrationConfirmationComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccountRegistrationRoutingModule { }
