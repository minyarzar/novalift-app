import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { AuthService } from '../../../core/services/auth.service';
import { WalletService } from '../../../core/services/wallet.service';
import { User, Transaction } from '../../../core/models';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, MatCardModule],
  template: `
    <div class="p-6">
      <h1 class="text-2xl font-bold mb-6">Dashboard</h1>
      <div class="grid grid-cols-1 md:grid-cols-3 gap-4 mb-6">
        <mat-card class="p-4"><h3 class="text-gray-500 text-sm">Balance</h3><p class="text-3xl font-bold">{{ user?.balance | currency:'MMK' }}</p></mat-card>
        <mat-card class="p-4"><h3 class="text-gray-500 text-sm">VIP Level</h3><p class="text-3xl font-bold uppercase">{{ user?.vipLevel }}</p></mat-card>
        <mat-card class="p-4"><h3 class="text-gray-500 text-sm">Total Earned</h3><p class="text-3xl font-bold">{{ user?.totalEarned | currency:'MMK' }}</p></mat-card>
      </div>
      <mat-card class="p-4">
        <h3 class="font-semibold mb-4">Recent Transactions</h3>
        <div class="space-y-2">
          <div *ngFor="let t of transactions" class="flex justify-between p-3 bg-gray-50 rounded-lg">
            <div><p class="font-medium capitalize">{{ t.type }}</p><p class="text-sm text-gray-500">{{ t.method }} • {{ t.status }}</p></div>
            <div class="text-right"><p class="font-bold">{{ t.amount | currency:'MMK' }}</p><p class="text-xs text-gray-400">{{ t.createdAt | date:'short' }}</p></div>
          </div>
          <p *ngIf="transactions.length === 0" class="text-gray-500">No transactions yet.</p>
        </div>
      </mat-card>
    </div>
  `
})
export class HomeComponent implements OnInit {
  user: User | null = null;
  transactions: Transaction[] = [];

  constructor(private authService: AuthService, private walletService: WalletService) {}

  ngOnInit(): void {
    this.authService.currentUser$.subscribe(u => this.user = u);
    this.walletService.getTransactions().subscribe(res => {
      if (res.success) this.transactions = res.data?.slice(0, 5) || [];
    });
  }
}
