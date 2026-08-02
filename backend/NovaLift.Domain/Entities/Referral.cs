namespace NovaLift.Domain.Entities;

public class Referral
{
    public int Id { get; set; }
    public int ReferrerId { get; set; }
    public int ReferredId { get; set; }
    public decimal Commission { get; set; } = 0;
    public bool IsPaid { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User Referrer { get; set; } = null!;
    public User Referred { get; set; } = null!;
}
