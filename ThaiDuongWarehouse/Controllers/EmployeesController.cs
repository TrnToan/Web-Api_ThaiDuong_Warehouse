using ThaiDuongWarehouse.Api.Applications.Commands.Employees;
using ThaiDuongWarehouse.Api.Applications.Queries.Employees;

namespace ThaiDuongWarehouse.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ControllerBase
{
	private readonly IEmployeeQueries _queries;
    private readonly IMediator _mediator;
	public EmployeesController(IMediator mediator, IEmployeeQueries queries)
	{
		_mediator = mediator;
		_queries = queries;
	}
	[HttpGet]
	public async Task<IEnumerable<EmployeeViewModel>> GetAllAsync()
	{
		return await _queries.GetAllEmployee();
	}

	[HttpGet]
	[Route("{employeeId}")]
	public async Task<EmployeeViewModel?> GetAsync([FromRoute] string employeeId)
	{
		return await _queries.GetEmployeeById(employeeId);
	}

	[HttpPost]
	public async Task<IActionResult> PostAsync([FromBody] CreateEmployeeCommand command)
	{
		try
		{
			var result = await _mediator.Send(command);
			if(result != true)
			{
				return BadRequest();
			}
			return Ok();
		}
		catch (Exception ex)
		{
			return BadRequest(ex.Message);
		}
	}
}
