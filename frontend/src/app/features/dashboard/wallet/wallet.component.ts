import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatTabsModule } from '@angular/material/tabs';
import { MatSelectModule } from '@angular/material/select';
import { WalletService } from '../../../core/services/wallet.service';
import { Wallet, Transaction } from '../../../core/models';

@Component({
  selector: 'app-wallet',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatCardModule, MatInputModule, MatButtonModule, MatTabsModule, MatSelectModule],
  template: `
    <div class="p-6 max-w-3xl">
      <h1 class="text-2xl font-bold mb-6">Wallet</h1>
      <mat-tab-group [(selectedIndex)]="activeTab">
        <mat-tab label="Deposit">
          <mat-card class="p-4 mt-4">
            <form [formGroup]="depositForm" (ngSubmit)="onDeposit()" class="space-y-4">
              <mat-form-field class="w-full"><mat-label>Amount (MMK)</mat-label><input matInput type="number" formControlName="amount"></mat-form-field>
              <mat-form-field class="w-full">
                <mat-label>Method</mat-label>
                <mat-select formControlName="method"><mat-option value="kpay">KPay</mat-option><mat-option value="wavepay">WavePay</mat-option></mat-select>
              </mat-form-field>
              <mat-form-field class="w-full"><mat-label>Sender Name (KPay/WavePay)</mat-label><input matInput formControlName="senderName"></mat-form-field>
              <mat-form-field class="w-full"><mat-label>Sender Phone</mat-label><input matInput formControlName="senderPhone"></mat-form-field>
              <mat-form-field class="w-full"><mat-label>Transaction Number</mat-label><input matInput formControlName="transactionNumber"></mat-form-field>
              <button mat-raised-button color="primary" type="submit" [disabled]="depositForm.invalid">Submit Deposit</button>
              <p class="text-xs text-gray-500 mt-2">Admin will review within 24 hours.</p>
            </form>
          </mat-card>
        </mat-tab>
        <mat-tab label="Withdraw">
          <mat-card class="p-4 mt-4">
            <form [formGroup]="withdrawForm" (ngSubmit)="onWithdraw()" class="space-y-4">
              <mat-form-field class="w-full"><mat-label>Amount (MMK)</mat-label><input matInput type="number" formControlName="amount"></mat-form-field>
              <mat-form-field class="w-full">
                <mat-label>Method</mat-label>
                <mat-select formControlName="method"><mat-option value="kpay">KPay</mat-option><mat-option value="wavepay">WavePay</mat-option></mat-select>
              </mat-form-field>
              <mat-form-field class="w-full">
                <mat-label>Wallet</mat-label>
                <mat-select formControlName="walletId">
                  <mat-option *ngFor="let w of wallets" [value]="w.id">{{ w.type | uppercase }} - {{ w.accountName }} ({{ w.phoneNumber }})</mat-option>
                </mat-select>
              </mat-form-field>
              <button mat-raised-button color="primary" type="submit" [disabled]="withdrawForm.invalid">Request Withdrawal</button>
            </form>
          </mat-card>
        </mat-tab>
        <mat-tab label="History">
          <mat-card class="p-4 mt-4">
            <div class="space-y-2">
              <div *ngFor="let t of transactions" class="flex justify-between p-3 bg-gray-50 rounded-lg border-l-4"
                [class.border-green-500]="t.status === 'Approved'"
                [class.border-yellow-500]="t.status === 'Pending'"
                [class.border-red-500]="t.status === 'Rejected'">
                <div><p class="font-medium capitalize">{{ t.type }}</p><p class="text-sm text-gray-500">{{ t.method }} • {{ t.transactionNumber }}</p></div>
                <div class="text-right"><p class="font-bold">{{ t.amount | currency:'MMK' }}</p><span class="text-xs px-2 py-1 rounded-full"
                  [class.bg-green-100]="t.status === 'Approved'"
                  [class.text-green-700]="t.status === 'Approved'"
                  [class.bg-yellow-100]="t.status === 'Pending'"
                  [class.text-yellow-700]="t.status === 'Pending'">{{ t.status }}</span></div>
              </div>
              <p *ngIf="transactions.length === 0" class="text-gray-500">No transactions.</p>
            </div>
          </mat-card>
        </mat-tab>
      </mat-tab-group>
    </div>
  `
})
export class WalletComponent implements OnInit {
  activeTab = 0;
  wallets: Wallet[] = [];
  transactions: Transaction[] = [];

  depositForm = this.fb.group({
    amount: [0, [Validators.required, Validators.min(1000)]],
    method: ['kpay', Validators.required],
    senderName: ['', Validators.required],
    senderPhone: ['', [Validators.required, Validators.minLength(6)]],
    transactionNumber: ['', Validators.required]
  });

  withdrawForm = this.fb.group({
    amount: [0, [Validators.required, Validators.min(5000)]],
    method: ['kpay', Validators.required],
    walletId: [0, Validators.required]
  });

  constructor(private fb: FormBuilder, private walletService: WalletService) {}

  ngOnInit(): void { this.loadData(); }

  loadData(): void {
    this.walletService.getWallets().subscribe(res => { if (res.success) this.wallets = res.data || []; });
    this.walletService.getTransactions().subscribe(res => { if (res.success) this.transactions = res.data || []; });
  }

  onDeposit(): void {
    if (this.depositForm.invalid) return;
    this.walletService.deposit(this.depositForm.value as any).subscribe(res => {
      if (res.success) { alert('Deposit submitted for approval'); this.depositForm.reset(); this.loadData(); }
      else alert(res.message || 'Failed');
    });
  }

  onWithdraw(): void {
    if (this.withdrawForm.invalid) return;
    this.walletService.withdraw(this.withdrawForm.value as any).subscribe(res => {
      if (res.success) { alert('Withdrawal submitted for approval'); this.withdrawForm.reset(); this.loadData(); }
      else alert(res.message || 'Failed');
    });
  }
}
