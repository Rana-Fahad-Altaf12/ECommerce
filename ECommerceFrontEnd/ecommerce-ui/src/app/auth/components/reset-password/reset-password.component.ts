import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import * as AuthActions from '../../store/auth.actions';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { selectIsLoading, selectAuthError } from '../../store/auth.selectors';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.scss']
})
export class ResetPasswordComponent implements OnInit {
  resetPasswordForm: FormGroup;
  isLoading$: Observable<boolean>;
  error$: Observable<string | null>;

  constructor(
    private fb: FormBuilder,
    private store: Store,
    private toastr: ToastrService,
    private route: ActivatedRoute
  ) {
    this.resetPasswordForm = this.fb.group({
      token: ['', Validators.required],
      newPassword: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required]
    });

    this.isLoading$ = this.store.select(selectIsLoading);
    this.error$ = this.store.select(selectAuthError);
  }

  ngOnInit() {
    this.route.queryParams.subscribe(params => {
      const token = params['token'];
      if (token) {
        this.resetPasswordForm.patchValue({ token });
      } else {
        this.toastr.error('Token is missing. Please check your link.');
      }
    });

    this.error$.subscribe(error => {
      if (error) {
        this.toastr.error(error);
      }
    });
  }

  onSubmit() {
    if (this.resetPasswordForm.valid) {
      const { token, newPassword } = this.resetPasswordForm.value;
      this.store.dispatch(AuthActions.resetPassword({ token, newPassword }));
    } else {
      this.resetPasswordForm.markAllAsTouched();
    }
  }
}