import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse, Transaction, User } from '../models';

export interface DashboardStats {
  totalBalance: number;
  totalDeposits: number;
  totalWithdrawals: number;
  totalUsers: number;
  pendingDeposits: number;
  pendingWithdrawals: number;
  totalTasks: number;
  totalOrders: number;
}

export interface PaymentMethod {
  id: number;
  type: string;
  name: string;
  nameLocal?: string;
  minDeposit: number;
  maxDeposit: number;
  minWithdrawal: number;
  maxWithdrawal: number;
  depositFee: number;
  withdrawalFee: number;
  receiverName?: string;
  receiverPhone?: string;
  receiverAccount?: string;
  instructions?: string;
  isActive: boolean;
}

export interface ReviewTransactionRequest {
  id: number;
  status: string;
  reviewNote?: string;
}

@Injectable({ providedIn: 'root' })
export class AdminService {
  private apiUrl = 'http://localhost:5000/api/admin';

  constructor(private http: HttpClient) {}

  getStats(): Observable<ApiResponse<DashboardStats>> {
    return this.http.get<ApiResponse<DashboardStats>>(`${this.apiUrl}/dashboard/stats`);
  }

  getTransactions(type?: string, status?: string): Observable<ApiResponse<Transaction[]>> {
    let params = '';
    if (type) params += `type=${type}&`;
    if (status) params += `status=${status}`;
    return this.http.get<ApiResponse<Transaction[]>>(`${this.apiUrl}/transactions?${params}`);
  }

  reviewTransaction(request: ReviewTransactionRequest): Observable<ApiResponse<boolean>> {
    return this.http.patch<ApiResponse<boolean>>(`${this.apiUrl}/transactions/review`, request);
  }

  getUsers(): Observable<ApiResponse<User[]>> {
    return this.http.get<ApiResponse<User[]>>(`${this.apiUrl}/users`);
  }

  getPaymentMethods(): Observable<ApiResponse<PaymentMethod[]>> {
    return this.http.get<ApiResponse<PaymentMethod[]>>(`${this.apiUrl}/payments`);
  }
}
