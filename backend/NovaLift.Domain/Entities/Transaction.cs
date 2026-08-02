using NovaLift.Domain.Enums;
using System.Text.Json;

namespace NovaLift.Domain.Entities;

public class Transaction
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public TransactionType Type { get; set; }
    public decimal Amount { get; set; }
    public decimal Fee { get; set; } = 0;
    public decimal NetAmount { get; set; }
    public TransactionStatus Status { get; set; } = TransactionStatus.Pending;
    public PaymentMethod Method { get; set; }

    // KPay / WavePay fields
    public string? SenderName { get; set; }
    public string? SenderPhone { get; set; }
    public string? TransactionNumber { get; set; }
    public string? ReceiverName { get; set; }
    public string? ReceiverPhone { get; set; }
    public string? ScreenshotUrl { get; set; }

    // Bank / Crypto
    public int? WalletId { get; set; }
    public string? BankRef { get; set; }
    public string? CryptoAddress { get; set; }
    public string? CryptoTxHash { get; set; }

    // Admin review
    public int? ReviewedBy { get; set; }
    public DateTime? ReviewedAt { get; set; }
    public string? ReviewNote { get; set; }

    public string? MetadataJson { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = null!;
    public Wallet? Wallet { get; set; }
    public User? Reviewer { get; set; }
}
