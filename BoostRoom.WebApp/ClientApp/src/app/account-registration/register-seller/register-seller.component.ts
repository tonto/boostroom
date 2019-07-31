import { Component, OnInit } from '@angular/core';
import { FormGroup, AbstractControl, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-register-seller',
  templateUrl: './register-seller.component.html',
  styleUrls: ['./register-seller.component.scss']
})
export class RegisterSellerComponent implements OnInit {

  sellerForm = new FormGroup({
    username: new FormControl('', [
      Validators.required,
      Validators.minLength(5)
    ]),
    email: new FormControl('', [
      Validators.required,
      Validators.email
    ]),
    password: new FormControl('', [
      Validators.required,
      Validators.minLength(5)
    ]),
    confirmPassword: new FormControl('', [
      Validators.required,
      Validators.minLength(5)
    ]),
    country: new FormControl('', Validators.required),
    acceptTerms: new FormControl('', Validators.requiredTrue),
  }, {
    validators: [this.validatePassword]
  });

  registrationInProgress = false;

  constructor() { }

  ngOnInit() {
  }

  submitSellerForm() {

  }

  validatePassword(c: AbstractControl) {
    if (c.get('password').value !== c.get('confirmPassword').value) {
      return { passwordsDoNotMatch: true };
    }

    return null;
  }
}
