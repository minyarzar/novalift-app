[ApiController]
[Route("api/admin/payments")]
[Authorize(Roles="Admin,SuperAdmin")]

public class AdminPaymentsController:ControllerBase
{

private readonly IPaymentAdminService _service;


public AdminPaymentsController(IPaymentAdminService service)
{
_service=service;
}



[HttpGet]
public async Task<IActionResult> Get()
{
return Ok(await _service.GetAllAsync());
}



[HttpPost]
public async Task<IActionResult> Create(AdminPaymentMethodDto dto)
{
return Ok(await _service.CreateAsync(dto));
}



[HttpPut("{id}")]
public async Task<IActionResult> Update(
int id,
AdminPaymentMethodDto dto)
{
return Ok(await _service.UpdateAsync(id,dto));
}



[HttpPut("{id}/toggle")]
public async Task<IActionResult> Toggle(int id)
{
return Ok(await _service.ToggleAsync(id));
}


}