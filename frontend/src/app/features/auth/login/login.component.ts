import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink, MatCardModule, MatInputModule, MatButtonModule, MatProgressSpinnerModule],
  template: `
    <div class="min-h-screen flex items-center justify-center bg-gradient-to-b from-blue-50 to-white p-4">
      <mat-card class="w-full max-w-md p-6">
        <h2 class="text-2xl font-bold text-center mb-6 text-blue-600">NovaLift</h2>
        <form [formGroup]="loginForm" (ngSubmit)="onSubmit()">
          <mat-form-field class="w-full mb-4">
            <mat-label>Email</mat-label>
            <input matInput formControlName="email" type="email" placeholder="you@example.com">
          </mat-form-field>
          <mat-form-field class="w-full mb-4">
            <mat-label>Password</mat-label>
            <input matInput formControlName="password" type="password">
          </mat-form-field>
          <div *ngIf="error" class="text-red-600 text-sm mb-4">{{ error }}</div>
          <button mat-raised-button color="primary" class="w-full" type="submit" [disabled]="loading">
            <mat-spinner *ngIf="loading" diameter="20" class="inline-block mr-2"></mat-spinner>
            {{ loading ? 'Signing in...' : 'Sign In' }}
          </button>
        </form>
        <div class="mt-4 text-center text-sm text-gray-600">
          <a routerLink="/register" class="text-blue-600 hover:underline">Create account</a>
        </div>
      </mat-card>
    </div>
  `
})
export class LoginComponent {
  loginForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(6)]]
  });
  loading = false;
  error = '';

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {}

  onSubmit(): void {
    if (this.loginForm.invalid) return;
    this.loading = true;
    this.error = '';
    this.authService.login(this.loginForm.value as any).subscribe({
      next: (res) => {
        if (res.success) this.router.navigate(['/home']);
        else this.error = res.message || 'Login failed';
        this.loading = false;
      },
      error: () => {
        this.error = 'Server error';
        this.loading = false;
      }
    });
  }
}
