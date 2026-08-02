using NovaLift.Domain.Enums;

namespace NovaLift.Domain.Entities;

public class PaymentMethodConfig
{
    public int Id { get; set; }
    public PaymentMethod Type { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? NameLocal { get; set; }
    public string? Icon { get; set; }
    public decimal MinDeposit { get; set; } = 1000;
    public decimal MaxDeposit { get; set; } = 10000000;
    public decimal MinWithdrawal { get; set; } = 5000;
    public decimal MaxWithdrawal { get; set; } = 10000000;
    public decimal DepositFee { get; set; } = 0;
    public decimal WithdrawalFee { get; set; } = 0;
    public string? ProcessingTime { get; set; }
    public string? Instructions { get; set; }
    public string? ReceiverName { get; set; }
    public string? ReceiverPhone { get; set; }
    public string? ReceiverAccount { get; set; }
    public string? QrCodeUrl { get; set; }
    public bool IsActive { get; set; } = true;
    public int SortOrder { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
