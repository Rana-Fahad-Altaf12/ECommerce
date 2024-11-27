import { createFeatureSelector, createSelector } from '@ngrx/store';
import { AuthState } from './auth.reducer';

export const selectAuthState = createFeatureSelector<AuthState>('auth');

export const selectIsLoggedIn = createSelector(
  selectAuthState,
  (authState) => {
    return !!authState.token;
  }
);

export const selectCurrentUser  = createSelector(
  selectAuthState,
  (authState) => authState.user
);

export const selectAuthError = createSelector(
  selectAuthState,
  (authState) => authState.error
);

export const selectIsLoading = createSelector(
  selectAuthState,
  (authState) => authState.loading
);

export const selectForgotPasswordError = createSelector(
  selectAuthState,
  (authState) => authState.error
);
