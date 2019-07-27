import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
  HttpResponse,
  HttpErrorResponse
 } from '@angular/common/http';
 import { Observable, throwError } from 'rxjs';
 import { retry, catchError } from 'rxjs/operators';
import { ToasterService } from '../services/toaster.service';

 export class HttpErrorInterceptor implements HttpInterceptor {

  constructor(private toasterService: ToasterService) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request)
      .pipe(
        catchError(error => {
          let errMsg = '';

          if (error instanceof HttpErrorResponse) {

            errMsg = error.error.errorMessage;

            if (error.status === 400) {
              console.log(error.error);
              this.toasterService.show(errMsg, { classname: 'alert-danger' });
              return throwError(errMsg);
            }
          }

          this.toasterService.show('Oops, something went wrong!', { classname: 'alert-danger' });

          return throwError(error);
        })
      );
  }
 }
