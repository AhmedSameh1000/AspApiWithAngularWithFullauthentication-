import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AuthService } from 'src/app/Services/Auth/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent {
  constructor(
    private Fb: FormBuilder,
    private Service: AuthService,
    private Router: Router,
    private Toaster: ToastrService
  ) {}
  submited = false;
  ngOnInit(): void {
    this.CreateForm();
  }
  signInForm!: FormGroup;
  CreateForm() {
    this.signInForm = this.Fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      userName: ['', [Validators.required, Validators.minLength(5)]],
      firstName: ['', [Validators.required, Validators.minLength(3)]],
      lastName: ['', [Validators.required, Validators.minLength(3)]],
    });
  }
  Errors = '';
  Register() {
    if (this.signInForm.invalid) {
      return;
    }
    this.Service.Register(this.signInForm.value).subscribe(
      (res: any) => {
        localStorage.setItem('token', res.token);
        this.Toaster.success('success', 'Log in Success');
        this.Router.navigate(['/']);
      },
      (Error) => {
        this.Errors = Error.error;
      }
    );
  }
  Onsubmit() {
    this.submited = true;
    if (this.signInForm.invalid) {
      return;
    }
  }
}
