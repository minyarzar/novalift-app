import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { AdminService, DashboardStats } from '../../../core/services/admin.service';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [CommonModule, MatCardModule],
  template: `
    <div class="p-6"><h1 class="text-2xl font-bold mb-6">Admin Dashboard</h1>
    <div class="grid grid-cols-1 md:grid-cols-4 gap-4">
      <mat-card class="p-4"><h3 class="text-gray-500 text-sm">Total Users</h3><p class="text-3xl font-bold">{{ stats?.totalUsers }}</p></mat-card>
      <mat-card class="p-4"><h3 class="text-gray-500 text-sm">Total Deposits</h3><p class="text-3xl font-bold">{{ stats?.totalDeposits | currency:'MMK' }}</p></mat-card>
      <mat-card class="p-4"><h3 class="text-gray-500 text-sm">Pending Deposits</h3><p class="text-3xl font-bold text-yellow-600">{{ stats?.pendingDeposits }}</p></mat-card>
      <mat-card class="p-4"><h3 class="text-gray-500 text-sm">Pending Withdrawals</h3><p class="text-3xl font-bold text-red-600">{{ stats?.pendingWithdrawals }}</p></mat-card>
    </div></div>
  `
})
export class AdminDashboardComponent implements OnInit {
  stats: DashboardStats | null = null;
  constructor(private adminService: AdminService) {}
  ngOnInit(): void { this.adminService.getStats().subscribe(res => { if (res.success) this.stats = res.data || null; }); }
}
