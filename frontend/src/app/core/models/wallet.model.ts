export interface Wallet {
  id: number;
  type: string;
  accountName: string;
  accountNumber: string;
  phoneNumber?: string;
  bankName?: string;
  isDefault: boolean;
  isVerified: boolean;
  createdAt: string;
}

export interface Transaction {
  id: number;
  type: string;
  amount: number;
  fee: number;
  netAmount: number;
  status: string;
  method: string;
  senderName?: string;
  senderPhone?: string;
  transactionNumber?: string;
  receiverName?: string;
  receiverPhone?: string;
  screenshotUrl?: string;
  reviewNote?: string;
  createdAt: string;
  user?: {
    id: number;
    email: string;
    name?: string;
    phone?: string;
  };
}

export interface CreateDepositRequest {
  amount: number;
  method: string;
  senderName: string;
  senderPhone: string;
  transactionNumber: string;
  screenshotUrl?: string;
}

export interface CreateWithdrawalRequest {
  amount: number;
  method: string;
  walletId: number;
}

export interface CreateWalletRequest {
  type: string;
  accountName: string;
  accountNumber: string;
  phoneNumber?: string;
  bankName?: string;
  branch?: string;
  isDefault: boolean;
}
