namespace NovaLift.Application.DTOs;

public class DashboardStatsDto
{
    public decimal TotalBalance { get; set; }
    public decimal TotalDeposits { get; set; }
    public decimal TotalWithdrawals { get; set; }
    public int TotalUsers { get; set; }
    public int PendingDeposits { get; set; }
    public int PendingWithdrawals { get; set; }
    public int TotalTasks { get; set; }
    public int TotalOrders { get; set; }
}

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }
}
