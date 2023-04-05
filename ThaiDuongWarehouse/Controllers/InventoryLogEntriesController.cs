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
	[Route("{itemId}")]
	public async Task<IEnumerable<InventoryLogEntryViewModel>> GetByItemIdAsync([FromRoute] string itemId,[FromQuery] TimeRangeQuery query)
	{
		return await _queries.GetByItem(itemId, query);
	}
	[HttpGet]
	public async Task<IEnumerable<InventoryLogEntryViewModel>> GetByTimeAsync([FromQuery] TimeRangeQuery query)
	{
		return await _queries.GetByTime(query);	
	}
}
