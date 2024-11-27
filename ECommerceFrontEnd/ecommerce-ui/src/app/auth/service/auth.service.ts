import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { UserRegisterDto } from '../models/user-register.dto';
import { UserLoginDto } from '../models/user-login.dto';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private basePath = environment.apiUrl;
  private apiUrl = `${this.basePath}/authentication`; 
  private readonly TOKEN_KEY = 'auth_token';

  private userSubject: BehaviorSubject<any> = new BehaviorSubject(null);
  public user$: Observable<any> = this.userSubject.asObservable();

  constructor(private http: HttpClient) {
    const token = this.getToken();
    if (token) {
      const user = this.getUser();
      this.userSubject.next(user);
    }
  }

  register(user: UserRegisterDto): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, user);
  }

  setToken(token: string): void {
    localStorage.setItem(this.TOKEN_KEY, token);
  }
  isLoggedIn(): boolean {
    const token = this.getToken();
    return !!token;
  }
  getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  removeToken(): void {
    localStorage.removeItem(this.TOKEN_KEY);
  }
  
  login(user: UserLoginDto): Observable<any> {
    return this.http.post<{ token: string; firstName: string,lastName: string }>(`${this.apiUrl}/login`, user).pipe(
      tap(response => {
        this.setToken(response.token);
        this.setUser (response.firstName, response.lastName);
      })
    );
  }

  setUser (firstName: string, lastName: string): void {
    const user = { firstName, lastName };
    localStorage.setItem('auth_user', JSON.stringify(user));
    this.userSubject.next(user);
  }
  
  getUser (): any {
    const user = localStorage.getItem('auth_user');
    return user ? JSON.parse(user) : null;
  }
  
  removeUser (): void {
    console.log('Removing user from localStorage');
    localStorage.removeItem('auth_user');
    console.log('User  removed:', localStorage.getItem('auth_user'));
}

  logout(): void {
    this.removeUser();
    this.userSubject.next(null);
    this.removeToken();
  }

  sendPasswordResetLink(email: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/forgot-password`, { email });
  }

  resetPassword(token: string, newPassword: string): Observable<any> {
    return this.http.post(`${this.apiUrl}/reset-password`, { token, newPassword });
  }
}