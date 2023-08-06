using ThaiDuongWarehouse.Api.Applications.Commands.Employees;

namespace ThaiDuongWarehouse.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeesController : ApiControllerBase
{
	private readonly IEmployeeQueries _queries;
	public EmployeesController(IMediator mediator, IEmployeeQueries queries) : base(mediator)
	{
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
		return await CommandAsync(command);
	}
}
