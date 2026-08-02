using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NovaLift.Application.DTOs;
using NovaLift.Application.Interfaces;

namespace NovaLift.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class WalletController : ControllerBase
{
    private readonly IWalletService _walletService;

    public WalletController(IWalletService walletService)
    {
        _walletService = walletService;
    }

    [HttpGet]
    public async Task<IActionResult> GetWallets()
    {
        var userId = GetUserId();
        if (userId == null) return Unauthorized();
        var result = await _walletService.GetUserWalletsAsync(userId.Value);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddWallet([FromBody] CreateWalletRequest request)
    {
        var userId = GetUserId();
        if (userId == null) return Unauthorized();
        var result = await _walletService.AddWalletAsync(userId.Value, request);
        return Ok(result);
    }

    [HttpGet("transactions")]
    public async Task<IActionResult> GetTransactions([FromQuery] string? type)
    {
        var userId = GetUserId();
        if (userId == null) return Unauthorized();
        var result = await _walletService.GetUserTransactionsAsync(userId.Value, type);
        return Ok(result);
    }

    [HttpPost("deposit")]
    public async Task<IActionResult> Deposit([FromBody] CreateDepositRequest request)
    {
        var userId = GetUserId();
        if (userId == null) return Unauthorized();
        var result = await _walletService.CreateDepositAsync(userId.Value, request);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    [HttpPost("withdraw")]
    public async Task<IActionResult> Withdraw([FromBody] CreateWithdrawalRequest request)
    {
        var userId = GetUserId();
        if (userId == null) return Unauthorized();
        var result = await _walletService.CreateWithdrawalAsync(userId.Value, request);
        if (!result.Success) return BadRequest(result);
        return Ok(result);
    }

    private int? GetUserId()
    {
        var claim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        return claim != null && int.TryParse(claim.Value, out var id) ? id : null;
    }
}
