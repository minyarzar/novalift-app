using NovaLift.Domain.Enums;

namespace NovaLift.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? PasswordHash { get; set; }
    public string? Name { get; set; }
    public string? Avatar { get; set; }
    public UserRole Role { get; set; } = UserRole.User;
    public UserStatus Status { get; set; } = UserStatus.Pending;
    public VipLevel VipLevel { get; set; } = VipLevel.None;
    public DateTime? VipExpiry { get; set; }
    public string? ReferralCode { get; set; }
    public int? ReferredBy { get; set; }
    public int? CountryId { get; set; }
    public string Language { get; set; } = "en";
    public decimal Balance { get; set; } = 0;
    public decimal FrozenBalance { get; set; } = 0;
    public decimal TotalDeposited { get; set; } = 0;
    public decimal TotalWithdrawn { get; set; } = 0;
    public decimal TotalEarned { get; set; } = 0;
    public int TaskCount { get; set; } = 0;
    public DateTime? LastLoginAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public User? Referrer { get; set; }
    public Country? Country { get; set; }
    public ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<Commission> Commissions { get; set; } = new List<Commission>();
}
