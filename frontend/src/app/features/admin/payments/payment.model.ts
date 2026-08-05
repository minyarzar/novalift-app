export interface PaymentMethod {
  id: number;
  type: string;
  name: string;
  nameLocal?: string;

  icon?: string;

  receiverName?: string;
  receiverPhone?: string;
  receiverAccount?: string;

  minDeposit: number;
  maxDeposit: number;

  minWithdrawal: number;
  maxWithdrawal: number;

  depositFee: number;
  withdrawalFee: number;

  processingTime?: string;
  instructions?: string;

  qrCodeUrl?: string;

  sortOrder: number;

  isActive: boolean;
}