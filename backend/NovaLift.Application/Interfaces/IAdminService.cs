using NovaLift.Application.DTOs;
using NovaLift.Domain.Enums;

namespace NovaLift.Application.Interfaces;

public interface IAdminService
{
    Task<ApiResponse<DashboardStatsDto>> GetDashboardStatsAsync();
    Task<ApiResponse<IEnumerable<TransactionDto>>> GetTransactionsAsync(TransactionType? type, TransactionStatus? status);
    Task<ApiResponse<bool>> ReviewTransactionAsync(int adminId, ReviewTransactionRequest request);
    Task<ApiResponse<IEnumerable<UserDto>>> GetUsersAsync();
    Task<ApiResponse<bool>> UpdateUserAsync(int userId, UpdateUserRoleRequest request);
    Task<ApiResponse<IEnumerable<PaymentMethodDto>>> GetPaymentMethodsAsync();
    Task<ApiResponse<bool>> UpdatePaymentMethodAsync(int id, PaymentMethodDto dto);
}
