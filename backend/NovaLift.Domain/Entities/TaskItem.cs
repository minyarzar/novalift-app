using NovaLift.Domain.Enums;

namespace NovaLift.Domain.Entities;

public class TaskItem
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int? ProductId { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Reward { get; set; }
    public TaskStatus Status { get; set; } = TaskStatus.Pending;
    public string? ProofUrl { get; set; }
    public string? ProofDataJson { get; set; }
    public DateTime? CompletedAt { get; set; }
    public int? ReviewedBy { get; set; }
    public string? ReviewNote { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = null!;
    public Product? Product { get; set; }
}
