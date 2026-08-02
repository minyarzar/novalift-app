import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterOutlet, RouterLink, Router } from '@angular/router';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { AuthService } from '../../../core/services/auth.service';

@Component({
  selector: 'app-layout',
  standalone: true,
  imports: [CommonModule, RouterOutlet, RouterLink, MatSidenavModule, MatListModule, MatIconModule, MatToolbarModule, MatButtonModule],
  template: `
    <mat-sidenav-container class="h-screen">
      <mat-sidenav mode="side" opened class="w-64 bg-slate-900 text-white">
        <div class="p-4 border-b border-slate-700"><h2 class="text-xl font-bold">NovaLift</h2></div>
        <mat-nav-list>
          <a mat-list-item routerLink="/home"><mat-icon>home</mat-icon><span class="ml-2">Home</span></a>
          <a mat-list-item routerLink="/wallet"><mat-icon>account_balance_wallet</mat-icon><span class="ml-2">Wallet</span></a>
          <a mat-list-item routerLink="/tasks"><mat-icon>assignment</mat-icon><span class="ml-2">Tasks</span></a>
          <a mat-list-item routerLink="/records"><mat-icon>history</mat-icon><span class="ml-2">Records</span></a>
          <a mat-list-item routerLink="/profile"><mat-icon>person</mat-icon><span class="ml-2">Profile</span></a>
          <a mat-list-item routerLink="/admin/dashboard" *ngIf="isAdmin"><mat-icon>admin_panel_settings</mat-icon><span class="ml-2">Admin</span></a>
        </mat-nav-list>
        <div class="absolute bottom-0 w-full p-4 border-t border-slate-700">
          <button mat-button color="warn" class="w-full" (click)="logout()"><mat-icon>logout</mat-icon> Logout</button>
        </div>
      </mat-sidenav>
      <mat-sidenav-content class="bg-gray-50">
        <mat-toolbar class="bg-white shadow-sm"><span class="text-gray-600">Welcome, {{ userName }}</span></mat-toolbar>
        <div class="p-4"><router-outlet></router-outlet></div>
      </mat-sidenav-content>
    </mat-sidenav-container>
  `
})
export class LayoutComponent {
  userName = '';
  isAdmin = false;

  constructor(private authService: AuthService, private router: Router) {
    this.authService.currentUser$.subscribe(u => {
      this.userName = u?.name || u?.email || '';
      this.isAdmin = u?.role === 'Admin' || u?.role === 'SuperAdmin';
    });
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
