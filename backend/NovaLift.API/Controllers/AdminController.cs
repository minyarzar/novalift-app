using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NovaLift.Application.DTOs;
using NovaLift.Application.Interfaces;
using NovaLift.Domain.Enums;
using System.Security.Claims;

namespace NovaLift.API.Controllers;

[ApiController]
[Route("api/admin/[controller]")]
[Authorize(Roles = "Admin,SuperAdmin")]
public class TransactionsController : ControllerBase
{
    private readonly IAdminService _adminService;

    public TransactionsController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTransactions([FromQuery] string? type, [FromQuery] string? status)
    {
        TransactionType? txnType = null;
        TransactionStatus? txnStatus = null;

        if (!string.IsNullOrEmpty(type) && Enum.TryParse<TransactionType>(type, true, out var t))
            txnType = t;
        if (!string.IsNullOrEmpty(status) && Enum.TryParse<TransactionStatus>(status, true, out var s))
            txnStatus = s;

        var result = await _adminService.GetTransactionsAsync(txnType, txnStatus);
        return Ok(result);
    }

    [HttpPatch("review")]
    public async Task<IActionResult> ReviewTransaction([FromBody] ReviewTransactionRequest request)
    {
        var adminId = GetUserId();
        if (adminId == null) return Unauthorized();
        var result = await _adminService.ReviewTransactionAsync(adminId.Value, request);
        return Ok(result);
    }

    private int? GetUserId()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        return claim != null && int.TryParse(claim.Value, out var id) ? id : null;
    }
}

[ApiController]
[Route("api/admin/[controller]")]
[Authorize(Roles = "Admin,SuperAdmin")]
public class DashboardController : ControllerBase
{
    private readonly IAdminService _adminService;

    public DashboardController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet("stats")]
    public async Task<IActionResult> GetStats()
    {
        var result = await _adminService.GetDashboardStatsAsync();
        return Ok(result);
    }
}

[ApiController]
[Route("api/admin/[controller]")]
[Authorize(Roles = "Admin,SuperAdmin")]
public class UsersController : ControllerBase
{
    private readonly IAdminService _adminService;

    public UsersController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var result = await _adminService.GetUsersAsync();
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserRoleRequest request)
    {
        var result = await _adminService.UpdateUserAsync(id, request);
        return Ok(result);
    }
}

[ApiController]
[Route("api/admin/[controller]")]
[Authorize(Roles = "Admin,SuperAdmin")]
public class PaymentsController : ControllerBase
{
    private readonly IAdminService _adminService;

    public PaymentsController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet]
    public async Task<IActionResult> GetPaymentMethods()
    {
        var result = await _adminService.GetPaymentMethodsAsync();
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePaymentMethod(int id, [FromBody] PaymentMethodDto dto)
    {
        var result = await _adminService.UpdatePaymentMethodAsync(id, dto);
        return Ok(result);
    }
}
