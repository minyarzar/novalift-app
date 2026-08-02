export interface User {
  id: number;
  email: string;
  name?: string;
  phone?: string;
  avatar?: string;
  role: string;
  status: string;
  vipLevel: string;
  balance: number;
  totalEarned: number;
  taskCount: number;
  referralCode?: string;
  createdAt: string;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  name: string;
  email: string;
  phone: string;
  password: string;
  confirmPassword: string;
  referralCode?: string;
}

export interface AuthResponse {
  token: string;
  user: User;
}
