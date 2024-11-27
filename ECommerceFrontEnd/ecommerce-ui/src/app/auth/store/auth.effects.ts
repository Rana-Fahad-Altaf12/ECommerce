import { Router, NavigationEnd, NavigationStart } from '@angular/router';
import { Injectable } from '@angular/core';
import { inject } from '@angular/core';
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { AuthService } from '../service/auth.service';
import * as AuthActions from './auth.actions';
import { catchError, map, mergeMap, take, tap } from 'rxjs/operators';
import { of } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { filter } from 'rxjs/operators';

@Injectable()
export class AuthEffects {
  private actions$ = inject(Actions);

  constructor(
    private authService: AuthService,
    private router: Router,
    private toastr: ToastrService,
  ) {  }

  login$ = createEffect(() => { 
    return this.actions$.pipe(
      ofType(AuthActions.login),
      mergeMap(action =>
        this.authService.login(action.user).pipe(
          map(response => {
            this.authService.setToken(response.token);
            this.authService.setUser(response.firstName, response.lastName);
            return AuthActions.loginSuccess({ 
              token: response.token, 
              firstName: response.firstName, 
              lastName: response.lastName 
            });
          }),
          catchError(error => {
            const errorMessage = error.error?.errorMessage || 'Login failed';
            return of(AuthActions.loginFailure({ error: errorMessage }));
          })
        )
      )
    );
  });

  redirectAfterLogin$ = createEffect(() => 
    this.actions$.pipe(
      ofType(AuthActions.loginSuccess),
      tap(() => {
        this.router.navigate(['/']);
      })
    ),
    { dispatch: false }
  );

  logout$ = createEffect(() => 
    this.actions$.pipe(
      ofType(AuthActions.logout),
      tap(() => {
        this.authService.logout();
        this.router.navigate(['/login']);
      })
    ),
    { dispatch: false }
  );

  
  checkAuth$ = createEffect(() =>
    this.actions$.pipe(
      ofType(AuthActions.checkAuth),
      tap(() => {
        this.router.events.pipe(
          filter(event => event instanceof NavigationEnd),
          take(1)
        ).subscribe(() => {
          const token = this.authService.getToken();
          const currentRoute = this.router.url.split('?')[0];
          if (token) {
            this.router.navigate(['/products']);
          }
          else if (!token && currentRoute !== '/login' && currentRoute !== '/forgot-password'
             && currentRoute !== '/reset-password' && currentRoute !== '/register'
          ) {
            this.router.navigate(['/login']);
          }
        });
      })
    ),
    { dispatch: false }
  );
  

  sendPasswordResetLink$ = createEffect(() => 
    this.actions$.pipe(
      ofType(AuthActions.sendPasswordResetLink),
      mergeMap(action =>
        this.authService.sendPasswordResetLink(action.email).pipe(
          map(() => {
            this.toastr.success('Reset password link sent successfully');
            return AuthActions.sendPasswordResetLinkSuccess();
          }),
          catchError(error => {
            const errorMessage = error.error?.errorMessage || 'Reset password request failed';
            return of(AuthActions.sendPasswordResetLinkFailure({ error: errorMessage }));
          })
        )
      )
    )
  );

  resetPassword$ = createEffect(() => 
    this.actions$.pipe(
      ofType(AuthActions.resetPassword),
      mergeMap(action =>
        this.authService.resetPassword(action.token, action.newPassword).pipe(
          map(() => {
            this.toastr.success('Password reset successfully');
            return AuthActions.resetPasswordSuccess();
          }),
          catchError(error => {
            const errorMessage = error.error?.errorMessage || 'Password reset failed';
            return of(AuthActions.resetPasswordFailure({ error: errorMessage }));
          })
        )
      )
    )
  );
}
