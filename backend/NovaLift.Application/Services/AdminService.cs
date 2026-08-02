using NovaLift.Application.DTOs;
using NovaLift.Application.Interfaces;
using NovaLift.Domain.Enums;
using NovaLift.Domain.Interfaces;

namespace NovaLift.Application.Services;

public class AdminService : IAdminService
{
    private readonly IUnitOfWork _unitOfWork;

    public AdminService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse<DashboardStatsDto>> GetDashboardStatsAsync()
    {
        var users = await _unitOfWork.Users.GetAllAsync();
        var transactions = await _unitOfWork.Transactions.GetAllAsync();
        var tasks = await _unitOfWork.Tasks.GetAllAsync();
        var orders = await _unitOfWork.Orders.GetAllAsync();

        var stats = new DashboardStatsDto
        {
            TotalUsers = users.Count(),
            TotalBalance = users.Sum(u => u.Balance),
            TotalDeposits = transactions.Where(t => t.Type == TransactionType.Deposit && t.Status == TransactionStatus.Approved).Sum(t => t.Amount),
            TotalWithdrawals = transactions.Where(t => t.Type == TransactionType.Withdrawal && t.Status == TransactionStatus.Approved).Sum(t => t.Amount),
            PendingDeposits = transactions.Count(t => t.Type == TransactionType.Deposit && t.Status == TransactionStatus.Pending),
            PendingWithdrawals = transactions.Count(t => t.Type == TransactionType.Withdrawal && t.Status == TransactionStatus.Pending),
            TotalTasks = tasks.Count(),
            TotalOrders = orders.Count(),
        };

        return new ApiResponse<DashboardStatsDto> { Success = true, Data = stats };
    }

    public async Task<ApiResponse<IEnumerable<TransactionDto>>> GetTransactionsAsync(TransactionType? type, TransactionStatus? status)
    {
        var transactions = await _unitOfWork.Transactions.GetAllAsync();
        if (type.HasValue) transactions = transactions.Where(t => t.Type == type.Value);
        if (status.HasValue) transactions = transactions.Where(t => t.Status == status.Value);

        var dtos = transactions.OrderByDescending(t => t.CreatedAt).Select(t => new TransactionDto
        {
            Id = t.Id,
            Type = t.Type.ToString(),
            Amount = t.Amount,
            Fee = t.Fee,
            NetAmount = t.NetAmount,
            Status = t.Status.ToString(),
            Method = t.Method.ToString(),
            SenderName = t.SenderName,
            SenderPhone = t.SenderPhone,
            TransactionNumber = t.TransactionNumber,
            ReceiverName = t.ReceiverName,
            ReceiverPhone = t.ReceiverPhone,
            ScreenshotUrl = t.ScreenshotUrl,
            ReviewNote = t.ReviewNote,
            CreatedAt = t.CreatedAt,
            User = t.User == null ? null : new UserDto
            {
                Id = t.User.Id,
                Email = t.User.Email,
                Name = t.User.Name,
                Phone = t.User.Phone,
            }
        });

        return new ApiResponse<IEnumerable<TransactionDto>> { Success = true, Data = dtos };
    }

    public async Task<ApiResponse<bool>> ReviewTransactionAsync(int adminId, ReviewTransactionRequest request)
    {
        var transaction = await _unitOfWork.Transactions.GetByIdAsync(request.Id);
        if (transaction == null)
            return new ApiResponse<bool> { Success = false, Message = "Transaction not found" };

        transaction.Status = request.Status;
        transaction.ReviewNote = request.ReviewNote;
        transaction.ReviewedBy = adminId;
        transaction.ReviewedAt = DateTime.UtcNow;

        if (request.Status == TransactionStatus.Approved && transaction.Type == TransactionType.Deposit)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(transaction.UserId);
            if (user != null)
            {
                user.Balance += transaction.NetAmount;
                user.TotalDeposited += transaction.Amount;
            }
        }
        else if (request.Status == TransactionStatus.Rejected && transaction.Type == TransactionType.Withdrawal)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(transaction.UserId);
            if (user != null) user.Balance += transaction.Amount;
        }

        await _unitOfWork.SaveChangesAsync();
        return new ApiResponse<bool> { Success = true, Data = true };
    }

    public async Task<ApiResponse<IEnumerable<UserDto>>> GetUsersAsync()
    {
        var users = await _unitOfWork.Users.GetAllAsync();
        var dtos = users.Select(u => new UserDto
        {
            Id = u.Id,
            Email = u.Email,
            Phone = u.Phone,
            Name = u.Name,
            Avatar = u.Avatar,
            Role = u.Role.ToString(),
            Status = u.Status.ToString(),
            VipLevel = u.VipLevel.ToString(),
            Balance = u.Balance,
            TotalEarned = u.TotalEarned,
            TaskCount = u.TaskCount,
            ReferralCode = u.ReferralCode,
            CreatedAt = u.CreatedAt,
        });
        return new ApiResponse<IEnumerable<UserDto>> { Success = true, Data = dtos };
    }

    public async Task<ApiResponse<bool>> UpdateUserAsync(int userId, UpdateUserRoleRequest request)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId);
        if (user == null)
            return new ApiResponse<bool> { Success = false, Message = "User not found" };

        user.Role = request.Role;
        user.Status = request.Status;
        user.VipLevel = request.VipLevel;
        user.Balance = request.Balance;
        user.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.SaveChangesAsync();
        return new ApiResponse<bool> { Success = true, Data = true };
    }

    public async Task<ApiResponse<IEnumerable<PaymentMethodDto>>> GetPaymentMethodsAsync()
    {
        var methods = await _unitOfWork.PaymentMethodConfigs.GetAllAsync();
        var dtos = methods.Select(m => new PaymentMethodDto
        {
            Id = m.Id,
            Type = m.Type.ToString(),
            Name = m.Name,
            NameLocal = m.NameLocal,
            Icon = m.Icon,
            MinDeposit = m.MinDeposit,
            MaxDeposit = m.MaxDeposit,
            MinWithdrawal = m.MinWithdrawal,
            MaxWithdrawal = m.MaxWithdrawal,
            DepositFee = m.DepositFee,
            WithdrawalFee = m.WithdrawalFee,
            ProcessingTime = m.ProcessingTime,
            Instructions = m.Instructions,
            ReceiverName = m.ReceiverName,
            ReceiverPhone = m.ReceiverPhone,
            ReceiverAccount = m.ReceiverAccount,
            QrCodeUrl = m.QrCodeUrl,
            IsActive = m.IsActive,
        });
        return new ApiResponse<IEnumerable<PaymentMethodDto>> { Success = true, Data = dtos };
    }

    public async Task<ApiResponse<bool>> UpdatePaymentMethodAsync(int id, PaymentMethodDto dto)
    {
        var method = await _unitOfWork.PaymentMethodConfigs.GetByIdAsync(id);
        if (method == null)
            return new ApiResponse<bool> { Success = false, Message = "Payment method not found" };

        method.Name = dto.Name;
        method.NameLocal = dto.NameLocal;
        method.MinDeposit = dto.MinDeposit;
        method.MaxDeposit = dto.MaxDeposit;
        method.MinWithdrawal = dto.MinWithdrawal;
        method.MaxWithdrawal = dto.MaxWithdrawal;
        method.DepositFee = dto.DepositFee;
        method.WithdrawalFee = dto.WithdrawalFee;
        method.ReceiverName = dto.ReceiverName;
        method.ReceiverPhone = dto.ReceiverPhone;
        method.ReceiverAccount = dto.ReceiverAccount;
        method.Instructions = dto.Instructions;
        method.IsActive = dto.IsActive;
        method.UpdatedAt = DateTime.UtcNow;

        await _unitOfWork.SaveChangesAsync();
        return new ApiResponse<bool> { Success = true, Data = true };
    }
}
