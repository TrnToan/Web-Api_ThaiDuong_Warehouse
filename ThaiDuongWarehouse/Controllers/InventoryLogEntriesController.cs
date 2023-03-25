using ThaiDuongWarehouse.Api.Applications.Queries;
using ThaiDuongWarehouse.Api.Applications.Queries.InventoryLogEntries;

namespace ThaiDuongWarehouse.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InventoryLogEntriesController : ControllerBase
{
    private readonly IInventoryLogEntryQueries _queries;
	public InventoryLogEntriesController(IInventoryLogEntryQueries queries)
	{
		_queries = queries;
	}
	[HttpGet]
	public async Task<IEnumerable<InventoryLogEntryViewModel>> GetAllAsync()
	{
		return await _queries.GetAll();
	}
	[HttpGet]
	[Route("{itemId}")]
	public async Task<IEnumerable<InventoryLogEntryViewModel>> GetByItemIdAsync([FromRoute] string itemId)
	{
		return await _queries.GetByItem(itemId);
	}
	[HttpGet]
	[Route("GetInventoryLogEntriesByTime")]
	public async Task<IEnumerable<InventoryLogEntryViewModel>> GetByTimeAsync([FromQuery] TimeRangeQuery query)
	{
		return await _queries.GetByTime(query);	
	}
}
