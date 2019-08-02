import { Component, OnInit } from '@angular/core';
import { FormGroup, AbstractControl, FormControl, Validators } from '@angular/forms';
import { RegistrationService } from '../services/registration.service';
import { delay, finalize } from 'rxjs/operators';
import { Router } from '@angular/router';

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

  recaptchaValid = false;

  constructor(
    private router: Router,
    private registrationService: RegistrationService
  ) { }

  ngOnInit() {
  }

  submitSellerForm() {
    this.registrationInProgress = true;
    const form = this.sellerForm.value;

    this.registrationService.registerSeller({
      username: form.username,
      email: form.email,
      password: form.password,
      country: form.country
    }).pipe(
      delay(2000),
      finalize(() => {
        this.registrationInProgress = false;
      })
    ).subscribe(_ => {
      this.router.navigate(['accounts/seller-registration-confirmation']);
    });

    return false;
  }

  validatePassword(c: AbstractControl) {
    if (c.get('password').value !== c.get('confirmPassword').value) {
      return { passwordsDoNotMatch: true };
    }

    return null;
  }

  resolved(captchaResponse: string) {
    this.recaptchaValid = true;
  }
}
