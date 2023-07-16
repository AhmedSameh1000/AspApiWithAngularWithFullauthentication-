import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Login } from 'src/app/admin/Context/DTO';
import { environment } from 'src/environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private Http: HttpClient, private Route: Router) {}
  login(Login: Login) {
    return this.Http.post(
      environment.BaseApi + 'Api/Auth/GetTokenAsync',
      Login
    );
  }
  Register(Signup: Login) {
    return this.Http.post(environment.BaseApi + 'Api/Auth/Register', Signup);
  }
  loggedIn() {
    return !!localStorage.getItem('token');
  }
  getToken() {
    return localStorage.getItem('token');
  }
  LogOut() {
    localStorage.removeItem('token');
    this.Route.navigate(['Login']);
  }
}
