using NovaLift.Domain.Enums;

namespace NovaLift.Application.DTOs;

public class WalletDto
{
    public int Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string AccountName { get; set; } = string.Empty;
    public string AccountNumber { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string? BankName { get; set; }
    public bool IsDefault { get; set; }
    public bool IsVerified { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateWalletRequest
{
    public PaymentMethod Type { get; set; }
    public string AccountName { get; set; } = string.Empty;
    public string AccountNumber { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string? BankName { get; set; }
    public string? Branch { get; set; }
    public bool IsDefault { get; set; } = false;
}
