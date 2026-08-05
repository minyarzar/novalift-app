using NovaLift.Application.DTOs;

namespace NovaLift.Application.Interfaces;

public interface IPaymentAdminService
{

    Task<ApiResponse<IEnumerable<AdminPaymentMethodDto>>>
        GetAllAsync();


    Task<ApiResponse<AdminPaymentMethodDto>>
        CreateAsync(AdminPaymentMethodDto dto);


    Task<ApiResponse<bool>>
        UpdateAsync(int id, AdminPaymentMethodDto dto);


    Task<ApiResponse<bool>>
        ToggleAsync(int id);

}