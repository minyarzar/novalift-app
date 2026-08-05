namespace NovaLift.Application.DTOs;

public class CreateTaskTemplateRequest
{
    public int VipLevel { get; set; }

    public string Type { get; set; } = string.Empty;

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public decimal Reward { get; set; }

    public int DailyLimit { get; set; } = 40;

    public int SortOrder { get; set; }
}