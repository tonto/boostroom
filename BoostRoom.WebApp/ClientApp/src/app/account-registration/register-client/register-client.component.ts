import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, AbstractControl } from '@angular/forms';

@Component({
  selector: 'app-register-client',
  templateUrl: './register-client.component.html',
  styleUrls: ['./register-client.component.css']
})
export class RegisterClientComponent implements OnInit {

  clientForm = new FormGroup({
    username: new FormControl('', Validators.required),
    email: new FormControl('', [
      Validators.required,
      Validators.email
    ]),
    password: new FormControl('', Validators.required),
    confirmPassword: new FormControl('', Validators.required),
    firstName: new FormControl('', Validators.required),
    lastName: new FormControl('', Validators.required),
    address: new FormControl('', Validators.required),
    city: new FormControl('', Validators.required),
    zip: new FormControl('', Validators.required),
    country: new FormControl('', Validators.required),
    notifyAboutDeals: new FormControl(''),
    acceptTerms: new FormControl('', Validators.requiredTrue),
  }, {
    validators: [this.validatePassword]
  });

  constructor() { }

  ngOnInit() {
  }

  submitClientForm() {
    console.log('form submited');
  }

  validatePassword(c: AbstractControl) {
    if (c.get('password').value !== c.get('confirmPassword').value) {
      return { passwordsDoNotMatch: true };
    }

    return null;
  }
}
