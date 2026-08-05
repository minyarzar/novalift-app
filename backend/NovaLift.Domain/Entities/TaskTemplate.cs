using NovaLift.Domain.Enums;

namespace NovaLift.Domain.Entities;

public class TaskTemplate
{
    public int Id { get; set; }

    public VipLevel VipLevel { get; set; }

    public string Type { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal Reward { get; set; }

    public int DailyLimit { get; set; } = 40;

    public int SortOrder { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; }
        = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; }
        = DateTime.UtcNow;
}