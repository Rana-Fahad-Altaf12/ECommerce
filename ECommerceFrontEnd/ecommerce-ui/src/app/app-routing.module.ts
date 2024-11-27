import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { RegisterComponent } from './auth/components/register/register.component';
import { LoginComponent } from './auth/components/login/login.component';
import { HomeComponent } from './home/home.component'; 
import { AuthGuard } from './auth/authGuard/auth-guard.guard';
import { ForgotPasswordComponent } from "./auth/components/forgot-password/forgot-password.component";
import { ResetPasswordComponent } from "./auth/components/reset-password/reset-password.component";
import { ProductListComponent } from "./product/components/product-list/product-list.component";
import { ProductDetailComponent } from "./product/components/product-detail/product-detail.component";

const routes: Routes = [
  { path: 'reset-password', component: ResetPasswordComponent },
  { path: 'forgot-password', component: ForgotPasswordComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
  { path: '', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'products', component: ProductListComponent, canActivate: [AuthGuard] },
  { path: 'product/:id', component: ProductDetailComponent, canActivate: [AuthGuard] },
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: '**', redirectTo: '/login' }
];

  @NgModule({
    imports: [
        RouterModule.forRoot(routes)
    ],
    exports: [RouterModule]
})

export class AppRoutingModule {}