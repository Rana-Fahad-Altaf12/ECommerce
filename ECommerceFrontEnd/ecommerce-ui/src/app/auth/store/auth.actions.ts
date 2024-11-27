import { createAction, props } from '@ngrx/store';
import { UserLoginDto } from '../../auth/models/user-login.dto';

export const login = createAction(
    '[Auth] Login',
    props<{ user: UserLoginDto }>()
  );
  
  export const loginSuccess = createAction(
    '[Auth] Login Success',
    props<{ token: string; firstName: string; lastName: string }>()
  );
  
  export const loginFailure = createAction(
    '[Auth] Login Failure',
    props<{ error: string }>()
  );
  
  export const logout = createAction('[Auth] Logout');
  
export const checkAuth = createAction('[Auth] Check Auth');

export const forgotPassword = createAction(
  '[Auth] Forgot Password',
  props<{ email: string }>()
);

export const forgotPasswordSuccess = createAction(
  '[Auth] Forgot Password Success'
);

export const forgotPasswordFailure = createAction(
  '[Auth] Forgot Password Failure',
  props<{ error: string }>()
);

export const sendPasswordResetLink = createAction(
  '[Auth] Send Password Reset Link',
  props<{ email: string }>()
);

export const sendPasswordResetLinkSuccess = createAction(
  '[Auth] Send Password Reset Link Success'
);

export const sendPasswordResetLinkFailure = createAction(
  '[Auth] Send Password Reset Link Failure',
  props<{ error: string }>()
);

export const resetPassword = createAction(
  '[Auth] Reset Password',
  props<{ token: string; newPassword: string }>()
);

export const resetPasswordSuccess = createAction(
  '[Auth] Reset Password Success'
);

export const resetPasswordFailure = createAction(
  '[Auth] Reset Password Failure',
  props<{ error: string }>()
);