using Microsoft.AspNetCore.Mvc;
using ThaiDuongWarehouse.Api.Applications.Commands.Department;

namespace ThaiDuongWarehouse.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DepartmentsController : ControllerBase
{
	private readonly IDepartmentQueries _queries;
	private readonly IMediator _mediator;
	public DepartmentsController(IDepartmentQueries queries, IMediator mediator)
    {
        _queries = queries;
        _mediator = mediator;
    }
    [HttpGet]
    public async Task<IEnumerable<DepartmentViewModel>> GetAllAsync()
    {
        return await _queries.GetAllDepartments();
    }
    [HttpPost]
    public async Task<IActionResult> AddDepartment([FromBody] CreateDepartmentCommand command) 
    {
        var result = await _mediator.Send(command);
        if (result)
        {
            return Ok();
        }
        else return BadRequest(result);
    }
}
