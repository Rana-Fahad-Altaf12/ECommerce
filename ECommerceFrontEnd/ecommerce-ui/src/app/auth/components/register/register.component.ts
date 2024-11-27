import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormGroup, Validators, AbstractControl, ValidationErrors } from '@angular/forms';
import { AuthService } from '../../service/auth.service';
import { UserRegisterDto } from '../../models/user-register.dto';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerationForm!: UntypedFormGroup;
  user: UserRegisterDto = { username: '', email: '', password: '', firstName: '', lastName: '', roleIds: [] };
  passwordStrength: string = '';
  passwordStrengthClass: string = '';
  passwordVisible: boolean = false;
  
  constructor(
    private formBuilder: UntypedFormBuilder,
    private authService: AuthService,
    private toastr: ToastrService
  ) { 
    
  }

  ngOnInit() {
    console.log('register');
    this.registerationForm = this.formBuilder.group({
      username: ['', [Validators.required, Validators.minLength(3), this.usernameValidator]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(8), this.passwordValidator]],
      firstName: ['', [Validators.required, Validators.minLength(3), this.nameValidator]],
      lastName: ['', [Validators.required, Validators.minLength(3), this.nameValidator]],
    });
  }

  get formFields() {
    return this.registerationForm.controls;
  }

  usernameValidator(control: AbstractControl): ValidationErrors | null {
    const usernamePattern = /^[a-zA-Z0-9_]+$/;
    const valid = usernamePattern.test(control.value);
    return valid ? null : { invalidUsername: true };
  }

  passwordValidator(control: AbstractControl): ValidationErrors | null {
    const password = control.value;
    const hasUpperCase = /[A-Z]/.test(password);
    const hasLowerCase = /[a-z]/.test(password);
    const hasNumber = /[0-9]/.test(password);
    const hasSpecialChar = /[!@#$%^&*(),.?":{}|<>]/.test(password);

    const valid = hasUpperCase && hasLowerCase && hasNumber && hasSpecialChar;
    return valid ? null : { invalidPassword: true };
  }
  togglePasswordVisibility() {
    this.passwordVisible = !this.passwordVisible;
  }
  nameValidator(control: AbstractControl): ValidationErrors | null {
    const namePattern = /^[a-zA-Z]+$/;
    const valid = namePattern.test(control.value);
    return valid ? null : { invalidName: true };
  }

  checkPasswordStrength(password: string) {
    if (!password) {
      this.passwordStrength = '';
      this.passwordStrengthClass = '';
      return;
    }

    let score = 0;

    if (password.length >= 8) score++;
    if (/[A-Z]/.test(password)) score++;
    if (/[a-z]/.test(password)) score++;
    if (/[0-9]/.test(password)) score++;
    if (/[!@#$%^&*(),.?":{}|<>]/.test(password)) score++;

    switch (score) {
      case 0:
      case 1:
        this.passwordStrength = 'Very Weak';
        this.passwordStrengthClass = 'bg-danger';
        break;
      case 2:
        this.passwordStrength = 'Weak';
        this.passwordStrengthClass = 'bg-warning';
        break;
      case 3:
        this.passwordStrength = 'Medium';
        this.passwordStrengthClass = 'bg-info';
        break;
      case 4:
        this.passwordStrength = 'Strong';
        this.passwordStrengthClass = 'bg-success';
        break;
      default:
        this.passwordStrength = '';
        this.passwordStrengthClass = '';
    }
  }

  register() {
    if (this.registerationForm.invalid) {
      Object.keys(this.formFields).forEach(field => {
        this.formFields[field].markAsTouched();
      });
      return;
    }

    this.user.email = this.formFields['email'].value;
    this.user.username = this.formFields['username'].value;
    this.user.password = this.formFields['password'].value;
    this.user.firstName = this.formFields['firstName']. value;
    this.user.lastName = this.formFields['lastName'].value;
    
    this.authService.register(this.user).subscribe((response: any) => {

      this.toastr.success('Registration successful!');

    }, (error: any) => {

      this.toastr.error('Registration failed. Please try again.');

    });
  }
}