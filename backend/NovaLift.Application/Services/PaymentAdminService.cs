using NovaLift.Application.DTOs;
using NovaLift.Application.Interfaces;
using NovaLift.Domain.Entities;
using NovaLift.Domain.Interfaces;

namespace NovaLift.Application.Services;


public class PaymentAdminService : IPaymentAdminService
{

private readonly IUnitOfWork _unit;


public PaymentAdminService(IUnitOfWork unit)
{
    _unit = unit;
}



public async Task<ApiResponse<IEnumerable<AdminPaymentMethodDto>>> GetAllAsync()
{

var data = await _unit.PaymentMethodConfigs.GetAllAsync();


return new ApiResponse<IEnumerable<AdminPaymentMethodDto>>
{
Success=true,

Data=data.Select(x=>new AdminPaymentMethodDto
{

Id=x.Id,
Type=x.Type,
Name=x.Name,
NameLocal=x.NameLocal,

ReceiverName=x.ReceiverName,
ReceiverPhone=x.ReceiverPhone,
ReceiverAccount=x.ReceiverAccount,

MinDeposit=x.MinDeposit,
MaxDeposit=x.MaxDeposit,

MinWithdrawal=x.MinWithdrawal,
MaxWithdrawal=x.MaxWithdrawal,

DepositFee=x.DepositFee,
WithdrawalFee=x.WithdrawalFee,

QrCodeUrl=x.QrCodeUrl,
Instructions=x.Instructions,

IsActive=x.IsActive,
SortOrder=x.SortOrder

})

};

}




public async Task<ApiResponse<AdminPaymentMethodDto>> CreateAsync(AdminPaymentMethodDto dto)
{

var payment=new PaymentMethodConfig
{

Type=dto.Type,

Name=dto.Name,

NameLocal=dto.NameLocal,


ReceiverName=dto.ReceiverName,

ReceiverPhone=dto.ReceiverPhone,

ReceiverAccount=dto.ReceiverAccount,


MinDeposit=dto.MinDeposit,

MaxDeposit=dto.MaxDeposit,


MinWithdrawal=dto.MinWithdrawal,

MaxWithdrawal=dto.MaxWithdrawal,


DepositFee=dto.DepositFee,

WithdrawalFee=dto.WithdrawalFee,


QrCodeUrl=dto.QrCodeUrl,

Instructions=dto.Instructions,


IsActive=dto.IsActive,

SortOrder=dto.SortOrder

};


await _unit.PaymentMethodConfigs.AddAsync(payment);

await _unit.SaveChangesAsync();


return new ApiResponse<AdminPaymentMethodDto>
{
Success=true,
Data=dto
};

}





public async Task<ApiResponse<bool>> UpdateAsync(int id,AdminPaymentMethodDto dto)
{

var item=await _unit.PaymentMethodConfigs.GetByIdAsync(id);


if(item==null)
return new ApiResponse<bool>
{
Success=false,
Message="Payment not found"
};


item.Name=dto.Name;

item.NameLocal=dto.NameLocal;

item.ReceiverName=dto.ReceiverName;

item.ReceiverPhone=dto.ReceiverPhone;

item.ReceiverAccount=dto.ReceiverAccount;


item.MinDeposit=dto.MinDeposit;

item.MaxDeposit=dto.MaxDeposit;


item.MinWithdrawal=dto.MinWithdrawal;

item.MaxWithdrawal=dto.MaxWithdrawal;


item.DepositFee=dto.DepositFee;

item.WithdrawalFee=dto.WithdrawalFee;


item.QrCodeUrl=dto.QrCodeUrl;

item.Instructions=dto.Instructions;


item.IsActive=dto.IsActive;

item.UpdatedAt=DateTime.UtcNow;


await _unit.SaveChangesAsync();


return new ApiResponse<bool>
{
Success=true,
Data=true
};

}





public async Task<ApiResponse<bool>> ToggleAsync(int id)
{

var item=await _unit.PaymentMethodConfigs.GetByIdAsync(id);


if(item==null)
return new ApiResponse<bool>
{
Success=false
};


item.IsActive=!item.IsActive;


await _unit.SaveChangesAsync();


return new ApiResponse<bool>
{
Success=true,
Data=true
};

}

}