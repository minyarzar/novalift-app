using NovaLift.Application.DTOs;

namespace NovaLift.Application.Interfaces;

public interface IWalletService
{
    Task<ApiResponse<IEnumerable<WalletDto>>> GetUserWalletsAsync(int userId);
    Task<ApiResponse<WalletDto>> AddWalletAsync(int userId, CreateWalletRequest request);
    Task<ApiResponse<TransactionDto>> CreateDepositAsync(int userId, CreateDepositRequest request);
    Task<ApiResponse<TransactionDto>> CreateWithdrawalAsync(int userId, CreateWithdrawalRequest request);
    Task<ApiResponse<IEnumerable<TransactionDto>>> GetUserTransactionsAsync(int userId, string? type = null);
}
