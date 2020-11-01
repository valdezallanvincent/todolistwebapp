import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AddUserDialog } from 'src/app/shared/modal/adduser.dialog.component'
import { AppComponent } from './app.component';
import { NavMenuComponent } from './pages/nav-menu/nav-menu.component';
import { TodoListComponent } from './pages/todo-list/todo-list.component';
import { UserListComponent } from './pages/user-list/user-list.component';
import { LoginComponent } from './pages/login/login.component';
import { HeaderInterceptor } from 'src/app/shared/core/header-interceptor';
import { AuthGuard } from 'src/app/shared/core/auth-guard';
import { CookieService } from 'ngx-cookie-service';
import { HttpService } from 'src/app/shared/core/http.service';
import { UserService } from 'src/app/shared/services/user.service';
import { TodoService } from 'src/app/shared/services/todo.service';
import { PageOutletComponent } from './shared/page-outlet/page-outlet.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AngularMaterialModule } from 'src/app/angular-material.module';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    TodoListComponent,
    UserListComponent,
    LoginComponent,
    PageOutletComponent,
    AddUserDialog
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    AngularMaterialModule,
    RouterModule.forRoot([
      { 
        path: '',
        component: PageOutletComponent, 
        canActivate: [AuthGuard],
        children: [
          { path: '', component: TodoListComponent, pathMatch: 'full'},
          { path: 'user', component: UserListComponent },
          { path: 'todo', component: TodoListComponent },
        ]
    },
      { path: 'login', component: LoginComponent },
      {path:'**', component: TodoListComponent },

    ]),
    BrowserAnimationsModule
  ],
  entryComponents: [
    AddUserDialog
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HeaderInterceptor,
      multi: true
    },
    CookieService,
    HttpService,
    UserService,
    TodoService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
