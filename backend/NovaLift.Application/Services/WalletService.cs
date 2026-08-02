using NovaLift.Application.DTOs;
using NovaLift.Application.Interfaces;
using NovaLift.Domain.Entities;
using NovaLift.Domain.Enums;
using NovaLift.Domain.Interfaces;

namespace NovaLift.Application.Services;

public class WalletService : IWalletService
{
    private readonly IUnitOfWork _unitOfWork;

    public WalletService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse<IEnumerable<WalletDto>>> GetUserWalletsAsync(int userId)
    {
        var wallets = (await _unitOfWork.Wallets.GetAllAsync()).Where(w => w.UserId == userId);
        var dtos = wallets.Select(w => new WalletDto
        {
            Id = w.Id,
            Type = w.Type.ToString(),
            AccountName = w.AccountName,
            AccountNumber = w.AccountNumber,
            PhoneNumber = w.PhoneNumber,
            BankName = w.BankName,
            IsDefault = w.IsDefault,
            IsVerified = w.IsVerified,
            CreatedAt = w.CreatedAt,
        });
        return new ApiResponse<IEnumerable<WalletDto>> { Success = true, Data = dtos };
    }

    public async Task<ApiResponse<WalletDto>> AddWalletAsync(int userId, CreateWalletRequest request)
    {
        if (request.IsDefault)
        {
            var existing = (await _unitOfWork.Wallets.GetAllAsync()).Where(w => w.UserId == userId);
            foreach (var w in existing) w.IsDefault = false;
        }

        var wallet = new Wallet
        {
            UserId = userId,
            Type = request.Type,
            AccountName = request.AccountName,
            AccountNumber = request.AccountNumber,
            PhoneNumber = request.PhoneNumber,
            BankName = request.BankName,
            Branch = request.Branch,
            IsDefault = request.IsDefault,
        };

        await _unitOfWork.Wallets.AddAsync(wallet);
        return new ApiResponse<WalletDto> { Success = true, Data = new WalletDto
        {
            Id = wallet.Id,
            Type = wallet.Type.ToString(),
            AccountName = wallet.AccountName,
            AccountNumber = wallet.AccountNumber,
            IsDefault = wallet.IsDefault,
            CreatedAt = wallet.CreatedAt,
        }};
    }

    public async Task<ApiResponse<TransactionDto>> CreateDepositAsync(int userId, CreateDepositRequest request)
    {
        var methodConfig = (await _unitOfWork.PaymentMethodConfigs.GetAllAsync())
            .FirstOrDefault(p => p.Type == request.Method && p.IsActive);
        if (methodConfig == null)
            return new ApiResponse<TransactionDto> { Success = false, Message = "Payment method unavailable" };

        var fee = request.Amount * (methodConfig.DepositFee / 100);
        var netAmount = request.Amount - fee;

        var transaction = new Transaction
        {
            UserId = userId,
            Type = TransactionType.Deposit,
            Amount = request.Amount,
            Fee = fee,
            NetAmount = netAmount,
            Status = TransactionStatus.Pending,
            Method = request.Method,
            SenderName = request.SenderName,
            SenderPhone = request.SenderPhone,
            TransactionNumber = request.TransactionNumber,
            ReceiverName = methodConfig.ReceiverName,
            ReceiverPhone = methodConfig.ReceiverPhone,
            ScreenshotUrl = request.ScreenshotUrl,
        };

        await _unitOfWork.Transactions.AddAsync(transaction);
        return new ApiResponse<TransactionDto> { Success = true, Data = MapTransactionDto(transaction) };
    }

    public async Task<ApiResponse<TransactionDto>> CreateWithdrawalAsync(int userId, CreateWithdrawalRequest request)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId);
        if (user == null || user.Balance < request.Amount)
            return new ApiResponse<TransactionDto> { Success = false, Message = "Insufficient balance" };

        var wallet = await _unitOfWork.Wallets.GetByIdAsync(request.WalletId);
        if (wallet == null || wallet.UserId != userId)
            return new ApiResponse<TransactionDto> { Success = false, Message = "Invalid wallet" };

        var methodConfig = (await _unitOfWork.PaymentMethodConfigs.GetAllAsync())
            .FirstOrDefault(p => p.Type == request.Method && p.IsActive);
        var fee = methodConfig != null ? request.Amount * (methodConfig.WithdrawalFee / 100) : 0;
        var netAmount = request.Amount - fee;

        user.Balance -= request.Amount;
        await _unitOfWork.SaveChangesAsync();

        var transaction = new Transaction
        {
            UserId = userId,
            Type = TransactionType.Withdrawal,
            Amount = request.Amount,
            Fee = fee,
            NetAmount = netAmount,
            Status = TransactionStatus.Pending,
            Method = request.Method,
            WalletId = request.WalletId,
            ReceiverName = wallet.AccountName,
            ReceiverPhone = wallet.PhoneNumber,
        };

        await _unitOfWork.Transactions.AddAsync(transaction);
        return new ApiResponse<TransactionDto> { Success = true, Data = MapTransactionDto(transaction) };
    }

    public async Task<ApiResponse<IEnumerable<TransactionDto>>> GetUserTransactionsAsync(int userId, string? type = null)
    {
        var transactions = (await _unitOfWork.Transactions.GetAllAsync())
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.CreatedAt);

        if (!string.IsNullOrEmpty(type) && Enum.TryParse<TransactionType>(type, true, out var txnType))
        {
            var filtered = transactions.Where(t => t.Type == txnType);
            return new ApiResponse<IEnumerable<TransactionDto>> { Success = true, Data = filtered.Select(MapTransactionDto) };
        }

        return new ApiResponse<IEnumerable<TransactionDto>> { Success = true, Data = transactions.Select(MapTransactionDto) };
    }

    private static TransactionDto MapTransactionDto(Transaction t) => new()
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
    };
}
