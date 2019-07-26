import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { delay } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {

  constructor() { }

  registerClient(): Observable<any> {
    return of(null).pipe(
      delay(3000)
    );
  }
}
