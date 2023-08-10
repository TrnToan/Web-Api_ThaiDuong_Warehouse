using ThaiDuongWarehouse.Api.Applications.Commands.Warehouses;

namespace ThaiDuongWarehouse.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WarehousesController : ControllerBase
{
    private readonly IWarehouseQueries _queries;
	private readonly IMediator _mediator;
	public WarehousesController(IWarehouseQueries queries, IMediator mediator)
	{
		_queries = queries;
		_mediator = mediator;
	}
	[HttpGet]
	public async Task<IEnumerable<WarehouseViewModel>> GetAllAsync()
	{
		return await _queries.GetAllWarehouses();
	}

	[HttpGet]
	[Route("locations")]
	public async Task<IEnumerable<LocationViewModel>> GetAllLocationsAsync()
	{
		return await _queries.GetAllLocations();
	}

	[HttpGet]
	[Route("{warehouseId}")]
	public async Task<WarehouseViewModel?> GetWarehouseByIdAsync(string warehouseId)
	{
		return await _queries.GetWarehouseById(warehouseId);
	}

	[HttpPost]
	public async Task<IActionResult> AddLocation(CreateLocationCommand command)
	{
        try
        {
            var result = await _mediator.Send(command);
            if (!result)
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
