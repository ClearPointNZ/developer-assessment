import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpInterceptor } from '@angular/common/http';

@Injectable()
export class DefaultInterceptor implements HttpInterceptor {
  private baseUrl: string = 'https://localhost:5001/';

  intercept(request: HttpRequest<any>, next: HttpHandler) {
    if (!request.headers.has('Accept')) {
      request = request.clone({headers: request.headers.set('Accept', 'application/json')});
    }

    if (!request.headers.has('Content-Type')) {
      request = request.clone({headers: request.headers.set('Content-Type', 'application/json')});
    }

    request = request.clone({
      url: this.baseUrl + request.url
    });

    return next.handle(request);
  }
}
