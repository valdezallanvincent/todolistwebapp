import { Injectable } from '@angular/core';
import { HttpService } from 'src/app/shared/core/http.service';
import { CookieService } from 'ngx-cookie-service';
import { environment } from 'src/environments/environment';
import jwt_decode from "jwt-decode";
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(
    private http: HttpService,
    private cookieService: CookieService
  ) {}
  public login(payload): Promise<any> {
    return new Promise((resolve, reject) => {
      const updatedPayload = {
        ...payload,
      };
      this.http.post('/authentication/login', updatedPayload).subscribe(
        (res) => {
          resolve(res);
        },
        (err) => {
          if (err.error != undefined && err.error.code == 400)
          {
            alert(err.error.message)
          }
          else
          {
            reject(err);
          }
        }
      );
    });
  }

  public logout() {
    localStorage.clear();
    this.cookieService.deleteAll('/');
  }

  getUser() {
    return this.getAccessToken() !== false
      ? jwt_decode(this.getAccessToken())
      : false;
  }

  public getUserList() {
    let baseEndpoint = `/user/getuserlist`;

    let url = `${baseEndpoint}`;
    return new Promise((resolve, reject) => {
      this.http.get(`${url}`).subscribe(
        (res) => resolve(res),
        (err) => reject(err)
      );
    });
  }

  storeToken({ token, tokenExpiresAt }) {
    const expires = tokenExpiresAt / 86400;
    this.cookieService.set('token', token, expires, '/');
  }

  getAccessToken() {
    return this.cookieService.get('token')
      ? this.cookieService.get('token')
      : false;
  }

  public addUser(payload): Promise<any> {
    return new Promise((resolve, reject) => {
      const updatedPayload = {
        ...payload,
      };
      this.http.post('/user/adduser', updatedPayload).subscribe(
        (res) => {
          resolve(res);
        },
        (err) => {
          console.log(err)
          if (err.error != undefined && err.error.Code == 409)
          {
            alert(err.error.Message)
          }
          else
          {
            reject(err);
          }
        }
      );
    });
  }
}
