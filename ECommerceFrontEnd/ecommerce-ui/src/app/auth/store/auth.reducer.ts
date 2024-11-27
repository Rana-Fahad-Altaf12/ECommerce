import { createReducer, on } from '@ngrx/store';
import * as AuthActions from './auth.actions';

export interface AuthState {
    user: { firstName: string; lastName: string } | null;
    token: string | null;
    error: string | null;
    loading: boolean;
}

export const initialState: AuthState = {
    user: JSON.parse(localStorage.getItem('auth_user') || 'null'),
    token: localStorage.getItem('auth_token') || null,
    error: null,
    loading: false,
};

export const authReducer = createReducer(
    initialState,
    on(AuthActions.login, state => ({ ...state, loading: true, error: null })),
    on(AuthActions.loginSuccess, (state, { token, firstName, lastName }) => ({
        ...state,
        user: { firstName, lastName },
        token,
        loading: false,
    })),
    on(AuthActions.loginFailure, (state, { error }) => ({
        ...state,
        error,
        loading: false,
    })),
    on(AuthActions.logout, state => ({ ...state, user: null, token: null })),
    on(AuthActions.sendPasswordResetLink, (state) => ({
      ...state,
      loading: true,
    })),
    on(AuthActions.sendPasswordResetLinkSuccess, (state) => ({
      ...state,
      loading: false,
    })),
    on(AuthActions.sendPasswordResetLinkFailure, (state, { error }) => ({
      ...state,
      error,
      loading: false,
    })),
    on(AuthActions.resetPassword, state => ({
      ...state,
      loading: true,
      error: null,
    })),
    on(AuthActions.resetPasswordSuccess, state => ({
      ...state,
      loading: false,
      error: null,
    })),
    on(AuthActions.resetPasswordFailure, (state, { error }) => ({
      ...state,
      loading: false,
      error,
    }))
);