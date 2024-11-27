import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';
import { AuthService } from '../../service/auth.service';
import * as AuthActions from '../../store/auth.actions';
import { selectIsLoading } from '../../store/auth.selectors';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.scss']
})
export class ForgotPasswordComponent implements OnInit, OnDestroy {
  forgotPasswordForm: FormGroup;
  isLoading$: Observable<boolean>;
  private unsubscribe$ = new Subject<void>();

  constructor(
    private fb: FormBuilder,
    private store: Store,
    private toastr: ToastrService,
    private router: Router
  ) {
    this.forgotPasswordForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]]
    });

    this.isLoading$ = this.store.select(selectIsLoading);
  }

  ngOnInit() {
  }

  ngOnDestroy() {
    this.unsubscribe$.next();
    this.unsubscribe$.complete();
  }

  onSubmit() {
    if (this.forgotPasswordForm.valid) {
      this.store.dispatch(AuthActions.sendPasswordResetLink({ email: this.forgotPasswordForm.value.email }));
    } else {
      this.forgotPasswordForm.markAllAsTouched();
    }
  }
}