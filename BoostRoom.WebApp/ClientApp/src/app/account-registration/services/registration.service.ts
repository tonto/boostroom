import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { delay } from 'rxjs/operators';
import { RegisterClientDto } from '../model/register-client-dto';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class RegistrationService {

  constructor(
    private httpClient: HttpClient
  ) { }

  registerClient(request: RegisterClientDto): Observable<any> {
    return this.httpClient.post('/api/accounts/clients/register', request);
  }
}
