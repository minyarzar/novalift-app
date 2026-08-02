import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatChipsModule } from '@angular/material/chips';
import { AdminService, ReviewTransactionRequest } from '../../../core/services/admin.service';
import { Transaction } from '../../../core/models';

@Component({
  selector: 'app-admin-deposits',
  standalone: true,
  imports: [CommonModule, MatCardModule, MatButtonModule, MatTableModule, MatChipsModule],
  template: `
    <div class="p-6">
      <h1 class="text-2xl font-bold mb-6">Deposit Requests</h1>
      <mat-card>
        <table mat-table [dataSource]="deposits" class="w-full">
          <ng-container matColumnDef="user"><th mat-header-cell *matHeaderCellDef>User</th><td mat-cell *matCellDef="let d">{{ d.user?.name || d.userId }}</td></ng-container>
          <ng-container matColumnDef="amount"><th mat-header-cell *matHeaderCellDef>Amount</th><td mat-cell *matCellDef="let d" class="font-bold">{{ d.amount | currency:'MMK' }}</td></ng-container>
          <ng-container matColumnDef="method"><th mat-header-cell *matHeaderCellDef>Method</th><td mat-cell *matCellDef="let d">{{ d.method }}</td></ng-container>
          <ng-container matColumnDef="sender"><th mat-header-cell *matHeaderCellDef>Sender</th><td mat-cell *matCellDef="let d">{{ d.senderName }}<br><span class="text-xs text-gray-500">{{ d.senderPhone }}</span></td></ng-container>
          <ng-container matColumnDef="txnNo"><th mat-header-cell *matHeaderCellDef>Txn No</th><td mat-cell *matCellDef="let d" class="font-mono text-xs">{{ d.transactionNumber }}</td></ng-container>
          <ng-container matColumnDef="status"><th mat-header-cell *matHeaderCellDef>Status</th><td mat-cell *matCellDef="let d"><mat-chip [class.bg-green-100]="d.status === 'Approved'" [class.bg-yellow-100]="d.status === 'Pending'" [class.bg-red-100]="d.status === 'Rejected'">{{ d.status }}</mat-chip></td></ng-container>
          <ng-container matColumnDef="actions"><th mat-header-cell *matHeaderCellDef>Actions</th><td mat-cell *matCellDef="let d">
            <div class="flex gap-2" *ngIf="d.status === 'Pending'">
              <button mat-raised-button color="primary" (click)="review(d.id, 'Approved')">Approve</button>
              <button mat-raised-button color="warn" (click)="review(d.id, 'Rejected')">Reject</button>
            </div>
          </td></ng-container>
          <tr mat-header-row *matHeaderRowDef="displayedColumns"></tr>
          <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>
        </table>
        <p *ngIf="deposits.length === 0" class="p-8 text-center text-gray-500">No pending deposits</p>
      </mat-card>
    </div>
  `
})
export class AdminDepositsComponent implements OnInit {
  deposits: Transaction[] = [];
  displayedColumns = ['user', 'amount', 'method', 'sender', 'txnNo', 'status', 'actions'];

  constructor(private adminService: AdminService) {}
  ngOnInit(): void { this.loadDeposits(); }

  loadDeposits(): void {
    this.adminService.getTransactions('deposit').subscribe(res => {
      if (res.success) this.deposits = res.data || [];
    });
  }

  review(id: number, status: string): void {
    const req: ReviewTransactionRequest = { id, status, reviewNote: `Admin ${status}` };
    this.adminService.reviewTransaction(req).subscribe(res => {
      if (res.success) this.loadDeposits();
    });
  }
}
