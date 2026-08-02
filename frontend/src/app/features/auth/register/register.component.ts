import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink, MatCardModule, MatInputModule, MatButtonModule],
  template: `
    <div class="min-h-screen flex items-center justify-center bg-gradient-to-b from-blue-50 to-white p-4">
      <mat-card class="w-full max-w-md p-6">
        <h2 class="text-2xl font-bold text-center mb-6 text-blue-600">Create Account</h2>
        <form [formGroup]="registerForm" (ngSubmit)="onSubmit()">
          <mat-form-field class="w-full mb-3"><mat-label>Name</mat-label><input matInput formControlName="name"></mat-form-field>
          <mat-form-field class="w-full mb-3"><mat-label>Email</mat-label><input matInput formControlName="email" type="email"></mat-form-field>
          <mat-form-field class="w-full mb-3"><mat-label>Phone</mat-label><input matInput formControlName="phone"></mat-form-field>
          <mat-form-field class="w-full mb-3"><mat-label>Password</mat-label><input matInput formControlName="password" type="password"></mat-form-field>
          <mat-form-field class="w-full mb-3"><mat-label>Confirm Password</mat-label><input matInput formControlName="confirmPassword" type="password"></mat-form-field>
          <mat-form-field class="w-full mb-3"><mat-label>Referral Code (optional)</mat-label><input matInput formControlName="referralCode"></mat-form-field>
          <div *ngIf="error" class="text-red-600 text-sm mb-4">{{ error }}</div>
          <button mat-raised-button color="primary" class="w-full" type="submit" [disabled]="loading">
            {{ loading ? 'Creating...' : 'Create Account' }}
          </button>
        </form>
        <p class="mt-4 text-center text-sm text-gray-600">
          Already have an account? <a routerLink="/login" class="text-blue-600 hover:underline">Sign in</a>
        </p>
      </mat-card>
    </div>
  `
})
export class RegisterComponent {
  registerForm = this.fb.group({
    name: ['', [Validators.required, Validators.minLength(2)]],
    email: ['', [Validators.required, Validators.email]],
    phone: ['', [Validators.required, Validators.minLength(6)]],
    password: ['', [Validators.required, Validators.minLength(6)]],
    confirmPassword: ['', Validators.required],
    referralCode: ['']
  });
  loading = false;
  error = '';

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {}

  onSubmit(): void {
    if (this.registerForm.invalid) return;
    if (this.registerForm.value.password !== this.registerForm.value.confirmPassword) {
      this.error = 'Passwords do not match';
      return;
    }
    this.loading = true;
    this.authService.register(this.registerForm.value as any).subscribe({
      next: (res) => {
        if (res.success) this.router.navigate(['/login']);
        else this.error = res.message || 'Registration failed';
        this.loading = false;
      },
      error: () => { this.error = 'Server error'; this.loading = false; }
    });
  }
}
