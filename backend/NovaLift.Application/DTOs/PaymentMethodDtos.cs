namespace NovaLift.Application.DTOs;

public class PaymentMethodDto
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? NameLocal { get; set; }
    public string? Icon { get; set; }
    public decimal MinDeposit { get; set; }
    public decimal MaxDeposit { get; set; }
    public decimal MinWithdrawal { get; set; }
    public decimal MaxWithdrawal { get; set; }
    public decimal DepositFee { get; set; }
    public decimal WithdrawalFee { get; set; }
    public string? ProcessingTime { get; set; }
    public string? Instructions { get; set; }
    public string? ReceiverName { get; set; }
    public string? ReceiverPhone { get; set; }
    public string? ReceiverAccount { get; set; }
    public string? QrCodeUrl { get; set; }
    public bool IsActive { get; set; }
}
