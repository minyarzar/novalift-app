using NovaLift.Domain.Enums;

namespace NovaLift.Domain.Entities;

public class Wallet
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public PaymentMethod Type { get; set; }
    public string AccountName { get; set; } = string.Empty;
    public string AccountNumber { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string? BankName { get; set; }
    public string? Branch { get; set; }
    public bool IsDefault { get; set; } = false;
    public bool IsVerified { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = null!;
}
