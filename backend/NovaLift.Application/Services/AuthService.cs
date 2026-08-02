using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using NovaLift.Application.DTOs;
using NovaLift.Application.Interfaces;
using NovaLift.Domain.Entities;
using NovaLift.Domain.Enums;
using NovaLift.Domain.Interfaces;
using BCrypt.Net;

namespace NovaLift.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public AuthService(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }

    public async Task<ApiResponse<AuthResponse>> RegisterAsync(RegisterRequest request)
    {
        var existingEmail = (await _unitOfWork.Users.GetAllAsync())
            .FirstOrDefault(u => u.Email.ToLower() == request.Email.ToLower());
        if (existingEmail != null)
            return new ApiResponse<AuthResponse> { Success = false, Message = "Email already registered" };

        var existingPhone = (await _unitOfWork.Users.GetAllAsync())
            .FirstOrDefault(u => u.Phone == request.Phone);
        if (existingPhone != null)
            return new ApiResponse<AuthResponse> { Success = false, Message = "Phone already registered" };

        int? referredById = null;
        if (!string.IsNullOrEmpty(request.ReferralCode))
        {
            var referrer = (await _unitOfWork.Users.GetAllAsync())
                .FirstOrDefault(u => u.ReferralCode == request.ReferralCode);
            if (referrer != null) referredById = referrer.Id;
        }

        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            ReferralCode = GenerateReferralCode(),
            ReferredBy = referredById,
            CountryId = request.CountryId,
            Status = UserStatus.Active,
            Role = UserRole.User,
        };

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.SaveChangesAsync();

        var token = GenerateJwtToken(user);
        return new ApiResponse<AuthResponse>
        {
            Success = true,
            Data = new AuthResponse
            {
                Token = token,
                User = MapToDto(user)
            }
        };
    }

    public async Task<ApiResponse<AuthResponse>> LoginAsync(LoginRequest request)
    {
        var user = (await _unitOfWork.Users.GetAllAsync())
            .FirstOrDefault(u => u.Email.ToLower() == request.Email.ToLower());

        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return new ApiResponse<AuthResponse> { Success = false, Message = "Invalid credentials" };

        if (user.Status == UserStatus.Banned)
            return new ApiResponse<AuthResponse> { Success = false, Message = "Account banned" };

        user.LastLoginAt = DateTime.UtcNow;
        await _unitOfWork.SaveChangesAsync();

        var token = GenerateJwtToken(user);
        return new ApiResponse<AuthResponse>
        {
            Success = true,
            Data = new AuthResponse
            {
                Token = token,
                User = MapToDto(user)
            }
        };
    }

    public async Task<ApiResponse<UserDto>> GetCurrentUserAsync(int userId)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(userId);
        if (user == null)
            return new ApiResponse<UserDto> { Success = false, Message = "User not found" };

        return new ApiResponse<UserDto> { Success = true, Data = MapToDto(user) };
    }

    private string GenerateJwtToken(User user)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _configuration["Jwt:Key"] ?? "your-super-secret-key-min-32-chars-long!"));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Name ?? ""),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim("vipLevel", user.VipLevel.ToString()),
            new Claim("status", user.Status.ToString()),
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"] ?? "NovaLift",
            audience: _configuration["Jwt:Audience"] ?? "NovaLiftClient",
            claims: claims,
            expires: DateTime.Now.AddDays(30),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static string GenerateReferralCode()
    {
        return "NVL" + Guid.NewGuid().ToString("N")[..6].ToUpper();
    }

    private static UserDto MapToDto(User user) => new()
    {
        Id = user.Id,
        Email = user.Email,
        Phone = user.Phone,
        Name = user.Name,
        Avatar = user.Avatar,
        Role = user.Role.ToString(),
        Status = user.Status.ToString(),
        VipLevel = user.VipLevel.ToString(),
        Balance = user.Balance,
        TotalEarned = user.TotalEarned,
        TaskCount = user.TaskCount,
        ReferralCode = user.ReferralCode,
        CreatedAt = user.CreatedAt,
    };
}
