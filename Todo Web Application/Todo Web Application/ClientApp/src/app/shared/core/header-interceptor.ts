import { Observable } from 'rxjs';
import {
  HttpEvent,
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
  HttpErrorResponse,
  HttpResponse
} from '@angular/common/http';
import { CookieService } from 'ngx-cookie-service';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';;
import { catchError, tap } from 'rxjs/operators';
@Injectable()

export class HeaderInterceptor implements HttpInterceptor {

  constructor(
    private cookieService: CookieService,
    private router: Router
  ) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = this.cookieService.get('token');
    let headers = {};
    if (token) {
      headers = { headers: req.headers.set('Authorization', `Bearer ${token}`) };
    }
    // Clone the request to add the new header
    const clonedRequest = req.clone(headers);

    // Pass the cloned request instead of the original request to the next handle
    return next.handle(clonedRequest).pipe(
      tap((event: HttpEvent<any>) => {
        if (event instanceof HttpResponse) {

        }
      }, (err: any) => {
        if (err instanceof HttpErrorResponse) {
          if (err.status === 403) {
            this.router.navigate(['/login']);
          }
        }
      })
  );
  }
}
