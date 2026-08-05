using NovaLift.Domain.Enums;

namespace NovaLift.Application.DTOs;

public class AdminPaymentMethodDto
{
    public int Id { get; set; }

    public PaymentMethod Type { get; set; }

    public string Name { get; set; } = "";

    public string? NameLocal { get; set; }

    public string? Icon { get; set; }


    public decimal MinDeposit { get; set; }

    public decimal MaxDeposit { get; set; }


    public decimal MinWithdrawal { get; set; }

    public decimal MaxWithdrawal { get; set; }


    public decimal DepositFee { get; set; }

    public decimal WithdrawalFee { get; set; }


    public string? ReceiverName { get; set; }

    public string? ReceiverPhone { get; set; }

    public string? ReceiverAccount { get; set; }


    public string? QrCodeUrl { get; set; }

    public string? Instructions { get; set; }


    public bool IsActive { get; set; }

    public int SortOrder { get; set; }
}