import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/Services/Auth/auth.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  constructor(
    private Fb: FormBuilder,
    private Service: AuthService,
    private Toaster: ToastrService,
    private Router: Router
  ) {}
  ngOnInit(): void {
    this.CreateForm();
  }
  submited = false;

  LogInForm!: FormGroup;
  CreateForm() {
    this.LogInForm = this.Fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
    });
  }
  login() {
    if (this.LogInForm.invalid) {
      return;
    }
    this.Service.login(this.LogInForm.value).subscribe((res: any) => {
      localStorage.setItem('token', res.token);
      this.Toaster.success('success', 'Log in Success');
      this.Router.navigate(['/']);
    });
  }
  Onsubmit() {
    this.submited = true;
    if (this.LogInForm.invalid) {
      return;
    }
  }
}
