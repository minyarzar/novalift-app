namespace NovaLift.Domain.Entities;

public class Banner
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public string? Link { get; set; }
    public string Position { get; set; } = "home";
    public int SortOrder { get; set; } = 0;
    public bool IsActive { get; set; } = true;
    public DateTime? StartAt { get; set; }
    public DateTime? EndAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
