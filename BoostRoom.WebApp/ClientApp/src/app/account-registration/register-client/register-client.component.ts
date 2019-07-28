import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, AbstractControl } from '@angular/forms';
import { RegistrationService } from '../services/registration.service';
import { Router } from '@angular/router';
import { finalize, delay } from 'rxjs/operators';

@Component({
  selector: 'app-register-client',
  templateUrl: './register-client.component.html',
  styleUrls: ['./register-client.component.scss']
})
export class RegisterClientComponent implements OnInit {

  clientForm = new FormGroup({
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

  registrationInProgress = false;

  constructor(
    private router: Router,
    private registrationService: RegistrationService) { }

  ngOnInit() {
  }

  submitClientForm() {
    this.registrationInProgress = true;
    const form = this.clientForm.value;

    this.registrationService.registerClient({
      username: form.username,
      email: form.email,
      password: form.password,
      firstName: form.firstName,
      lastName: form.lastName,
      addressLine: form.address,
      city: form.city,
      zip: form.zip,
      country: form.country,
      subscribeToOffers: form.notifyAboutDeals ? true : false
    }).pipe(
      delay(2000),
      finalize(() => {
        this.registrationInProgress = false;
      })
    ).subscribe(_ => {
      this.router.navigate(['accounts/registration-confirmation']);
    });

    return false;
  }

  validatePassword(c: AbstractControl) {
    if (c.get('password').value !== c.get('confirmPassword').value) {
      return { passwordsDoNotMatch: true };
    }

    return null;
  }
}
