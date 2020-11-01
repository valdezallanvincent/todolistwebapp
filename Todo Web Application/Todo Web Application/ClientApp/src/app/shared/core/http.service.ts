import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class HttpService {
  constructor(
      private http: HttpClient,
      private router: Router,
) {}

  get<T>(url: string): Observable<T> {
    return this.http.get(`${environment.api_endpoint}${url}`).pipe(
      catchError((err: any) => {
        this.handleUnauthorized(err);
        throw new Object({
          status: err.status,
          statusText: err.statusText,
          error: err.error
        });
      })
    ) as any;
  }

  post<T>(url: string, body?: any, options?: any): Observable<T> {
    return this.http.post(`${environment.api_endpoint}${url}`, body, options).pipe(
      catchError((err: any) => {
        this.handleUnauthorized(err);
        throw new Object({
          status: err.status,
          statusText: err.statusText,
          error: err.error
        });
      })
    ) as any;
  }

  put<T>(url: string, body?: any): Observable<T> {
    return this.http.put(`${environment.api_endpoint}${url}`, body).pipe(
      catchError((err: any) => {
        this.handleUnauthorized(err);
        throw new Object({
          status: err.status,
          statusText: err.statusText,
          error: err.error
        });
      })
    ) as any;
  }

  delete<T>(url: string): Observable<T> {
    return this.http.delete(`${environment.api_endpoint}${url}`).pipe(
      catchError((err: any) => {
        this.handleUnauthorized(err);
        throw new Object({
          status: err.status,
          statusText: err.statusText,
          error: err.error
        });
      })
    ) as any;
  }

  handleUnauthorized(err) {
    if (err.status === 401) {
      localStorage.clear();
      this.router.navigate(['/login']);
    }
  }
}
