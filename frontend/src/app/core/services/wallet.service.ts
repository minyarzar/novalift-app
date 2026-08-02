import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ApiResponse, Wallet, Transaction, CreateDepositRequest, CreateWithdrawalRequest, CreateWalletRequest } from '../models';

@Injectable({ providedIn: 'root' })
export class WalletService {
  private apiUrl = 'http://localhost:5000/api/wallet';

  constructor(private http: HttpClient) {}

  getWallets(): Observable<ApiResponse<Wallet[]>> {
    return this.http.get<ApiResponse<Wallet[]>>(this.apiUrl);
  }

  addWallet(request: CreateWalletRequest): Observable<ApiResponse<Wallet>> {
    return this.http.post<ApiResponse<Wallet>>(this.apiUrl, request);
  }

  getTransactions(type?: string): Observable<ApiResponse<Transaction[]>> {
    const params = type ? `?type=${type}` : '';
    return this.http.get<ApiResponse<Transaction[]>>(`${this.apiUrl}/transactions${params}`);
  }

  deposit(request: CreateDepositRequest): Observable<ApiResponse<Transaction>> {
    return this.http.post<ApiResponse<Transaction>>(`${this.apiUrl}/deposit`, request);
  }

  withdraw(request: CreateWithdrawalRequest): Observable<ApiResponse<Transaction>> {
    return this.http.post<ApiResponse<Transaction>>(`${this.apiUrl}/withdraw`, request);
  }
}
