import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { UserService } from 'src/app/shared/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  private loginForm: FormGroup;
  private fb: FormBuilder;
  submitted = false;
  hasError = false;
  isLoading = false;
  constructor(
    fb: FormBuilder,
    private router: Router,
    private userService: UserService,
  ) {
    this.fb = fb;
  }

  ngOnInit() {
    this.loginForm = this.fb.group({
      userName: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  get fval() { return this.loginForm.controls; }

  async login() {
    this.submitted = true;

    if (this.loginForm.invalid) {
       return;
     }
    this.isLoading = true;

    try {
      const res = await this.userService.login(this.loginForm.value);

      this.userService.storeToken(res);
      localStorage.setItem('isExpandedMenu', 'true');

      this.router.navigate(['/']);
    } catch (err) {
      this.isLoading = false;
      this.hasError = true;
    }
  }
}
class Login {
  fullName: string;
  userName: string;
}