using NovaLift.Domain.Enums;

namespace NovaLift.Application.DTOs;

public class UserDto
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Name { get; set; }
    public string? Avatar { get; set; }
    public string Role { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string VipLevel { get; set; } = string.Empty;
    public decimal Balance { get; set; }
    public decimal TotalEarned { get; set; }
    public int TaskCount { get; set; }
    public string? ReferralCode { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class UpdateUserRequest
{
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public string? Avatar { get; set; }
    public string? Language { get; set; }
}

public class UpdateUserRoleRequest
{
    public UserRole Role { get; set; }
    public UserStatus Status { get; set; }
    public VipLevel VipLevel { get; set; }
    public decimal Balance { get; set; }
}
