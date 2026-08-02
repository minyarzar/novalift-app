import { Routes } from '@angular/router';
import { authGuard } from './core/guards/auth.guard';
import { adminGuard } from './core/guards/admin.guard';
import { LayoutComponent } from './shared/components/layout/layout.component';

export const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  {
    path: 'login',
    loadComponent: () => import('./features/auth/login/login.component').then(m => m.LoginComponent)
  },
  {
    path: 'register',
    loadComponent: () => import('./features/auth/register/register.component').then(m => m.RegisterComponent)
  },
  {
    path: '',
    component: LayoutComponent,
    canActivate: [authGuard],
    children: [
      { path: 'home', loadComponent: () => import('./features/dashboard/home/home.component').then(m => m.HomeComponent) },
      { path: 'wallet', loadComponent: () => import('./features/dashboard/wallet/wallet.component').then(m => m.WalletComponent) },
      { path: 'tasks', loadComponent: () => import('./features/dashboard/tasks/tasks.component').then(m => m.TasksComponent) },
      { path: 'records', loadComponent: () => import('./features/dashboard/records/records.component').then(m => m.RecordsComponent) },
      { path: 'profile', loadComponent: () => import('./features/dashboard/profile/profile.component').then(m => m.ProfileComponent) }
    ]
  },
  {
    path: 'admin',
    canActivate: [adminGuard],
    children: [
      { path: 'dashboard', loadComponent: () => import('./features/admin/dashboard/dashboard.component').then(m => m.AdminDashboardComponent) },
      { path: 'deposits', loadComponent: () => import('./features/admin/deposits/deposits.component').then(m => m.AdminDepositsComponent) },
      { path: 'withdrawals', loadComponent: () => import('./features/admin/withdrawals/withdrawals.component').then(m => m.AdminWithdrawalsComponent) },
      { path: 'users', loadComponent: () => import('./features/admin/users/users.component').then(m => m.AdminUsersComponent) },
      { path: 'payments', loadComponent: () => import('./features/admin/payments/payments.component').then(m => m.AdminPaymentsComponent) }
    ]
  },
  { path: '**', redirectTo: '/login' }
];
