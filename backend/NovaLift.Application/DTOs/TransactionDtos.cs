using NovaLift.Domain.Enums;

namespace NovaLift.Application.DTOs;

public class TransactionDto
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public decimal Fee { get; set; }
    public decimal NetAmount { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Method { get; set; } = string.Empty;
    public string? SenderName { get; set; }
    public string? SenderPhone { get; set; }
    public string? TransactionNumber { get; set; }
    public string? ReceiverName { get; set; }
    public string? ReceiverPhone { get; set; }
    public string? ScreenshotUrl { get; set; }
    public string? ReviewNote { get; set; }
    public DateTime CreatedAt { get; set; }
    public UserDto? User { get; set; }
}

public class CreateDepositRequest
{
    public decimal Amount { get; set; }
    public PaymentMethod Method { get; set; }
    public string SenderName { get; set; } = string.Empty;
    public string SenderPhone { get; set; } = string.Empty;
    public string TransactionNumber { get; set; } = string.Empty;
    public string? ScreenshotUrl { get; set; }
}

public class CreateWithdrawalRequest
{
    public decimal Amount { get; set; }
    public PaymentMethod Method { get; set; }
    public int WalletId { get; set; }
}

public class ReviewTransactionRequest
{
    public int Id { get; set; }
    public TransactionStatus Status { get; set; }
    public string? ReviewNote { get; set; }
}
