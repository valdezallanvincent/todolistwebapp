import { Injectable } from '@angular/core';
import { HttpService } from 'src/app/shared/core/http.service';
import { CookieService } from 'ngx-cookie-service';
import { environment } from 'src/environments/environment';
import * as jwt_decode from 'jwt-decode';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class TodoService {
  constructor(
    private http: HttpService,
    private cookieService: CookieService
  ) {}
  public getTodoList() {
    let baseEndpoint = `/todo/gettodolist`;

    let url = `${baseEndpoint}`;
    return new Promise((resolve, reject) => {
      this.http.get(`${url}`).subscribe(
        (res) => resolve(res),
        (err) => reject(err)
      );
    });
  }

  public addTask(payload) : Promise<any> {
    return new Promise((resolve, reject) => {
      const updatedPayload = {
        ...payload,
      };
      this.http.post('/todo/addtodotransaction', updatedPayload).subscribe(
        (res) => {
          resolve(res);
        },
        (err) => {
            reject(err);
        }
      );
    });
  }

  public toggleStatus(id) : Promise<any> {
    return new Promise((resolve, reject) => {

      this.http.put(`/todo/completetodotransaction?todoTransactionId=${id}`).subscribe(
        (res) => {
          resolve(res);
        },
        (err) => {
            reject(err);
        }
      );
    });
  }

  public deleteTodoTransaction(id) : Promise<any> {
    return new Promise((resolve, reject) => {

      this.http.delete(`/todo/deletetodotransaction?todoTransactionId=${id}`).subscribe(
        (res) => {
          resolve(res);
        },
        (err) => {
            reject(err);
        }
      );
    });
  }

  public clearCompletedTodoTransaction() : Promise<any> {
    return new Promise((resolve, reject) => {

      this.http.delete(`/todo/clearcompletedtodotransaction`).subscribe(
        (res) => {
          resolve(res);
        },
        (err) => {
            reject(err);
        }
      );
    });
  }
}
