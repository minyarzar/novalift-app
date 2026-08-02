namespace NovaLift.Domain.Entities;

public class Commission
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int? SourceUserId { get; set; }
    public string Type { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public int? RelatedId { get; set; }
    public string? RelatedType { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = null!;
}
