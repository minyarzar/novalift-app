namespace NovaLift.Domain.Entities;

public class Country
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? NameLocal { get; set; }
    public string? Currency { get; set; }
    public string? CurrencySymbol { get; set; }
    public string? PhoneCode { get; set; }
    public string? Flag { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<User> Users { get; set; } = new List<User>();
}
