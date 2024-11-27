import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { AuthService } from '../../service/auth.service';
import * as AuthActions from '../../store/auth.actions';
import { selectAuthError, selectIsLoading, selectIsLoggedIn } from '../../store/auth.selectors';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {
  loginForm: FormGroup;
  isLoading$: Observable<boolean>;
  loginError$: Observable<string | null>;
  passwordVisible: boolean = false;
  private unsubscribe$ = new Subject<void>();

  constructor(
    private fb: FormBuilder,
    private store: Store,
    private toastr: ToastrService,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });

    this.isLoading$ = this.store.select(selectIsLoading);
    this.loginError$ = this.store.select(selectAuthError);
  }

  ngOnInit() {
    this.store.select(selectIsLoggedIn)
    .pipe(takeUntil(this.unsubscribe$))
    .subscribe(isLoggedIn => {
      if (isLoggedIn) {
        this.router.navigate(['/']);
      }
    });
    this.loginError$.subscribe(error => {
      if (error) {
        this.toastr.error(error, 'Error');
      }
    });
  }
  ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }
  onSubmit() {
    if (this.loginForm.valid) {
      this.store.dispatch(AuthActions.login({ user: this.loginForm.value }));
    } else {
      this.loginForm.markAllAsTouched();
    }
  }

  togglePasswordVisibility() {
    this.passwordVisible = !this.passwordVisible;
  }
}
