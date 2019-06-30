import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AccountRegistrationComponent } from './account-registration.component';

const routes: Routes = [
  {
    path: 'accounts/register',
    component: AccountRegistrationComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccountRegistrationRoutingModule { }
