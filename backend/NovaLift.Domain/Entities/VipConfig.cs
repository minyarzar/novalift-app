using NovaLift.Domain.Enums;
using System.Text.Json;

namespace NovaLift.Domain.Entities;

public class VipConfig
{
    public int Id { get; set; }
    public VipLevel Level { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal MinDeposit { get; set; }
    public int MinTasks { get; set; } = 0;
    public decimal CommissionRate { get; set; } = 0;
    public decimal TaskRewardMultiplier { get; set; } = 1;
    public decimal WithdrawalFee { get; set; } = 0;
    public int DailyTaskLimit { get; set; } = 10;
    public decimal DailyWithdrawalLimit { get; set; } = 1000;
    public string? Icon { get; set; }
    public string? Color { get; set; }
    public string? BenefitsJson { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
