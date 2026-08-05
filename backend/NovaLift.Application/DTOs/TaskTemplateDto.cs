namespace NovaLift.Application.DTOs;

public class TaskTemplateDto
{
    public int Id { get; set; }

    public int VipLevel { get; set; }

    public string Type { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal Reward { get; set; }

    public int DailyLimit { get; set; }

    public int SortOrder { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }
}