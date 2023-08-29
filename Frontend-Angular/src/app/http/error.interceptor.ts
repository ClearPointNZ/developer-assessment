import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpErrorResponse,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { EMPTY, Observable } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  
  public intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request)
        .pipe(
          catchError(response => {
              if (response instanceof HttpErrorResponse && response.status >= 400) {
                const error = response.error || response.statusText;
                alert(error);
              }

              return EMPTY;
            })
        );
  }
}
