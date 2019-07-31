import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-account-registration',
  templateUrl: './account-registration.component.html',
  styleUrls: ['./account-registration.component.scss']
})
export class AccountRegistrationComponent implements OnInit {

  clientRegistration = false;

  constructor() { }

  ngOnInit() {
  }

  toggleRegistration() {
    this.clientRegistration = !this.clientRegistration;
  }

}
