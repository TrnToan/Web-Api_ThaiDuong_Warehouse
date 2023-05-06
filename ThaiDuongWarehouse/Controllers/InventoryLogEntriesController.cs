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
		return await _queries.GetEntriesByItem(itemId, query);
	}

	[HttpGet]
	public async Task<IEnumerable<InventoryLogEntryViewModel>> GetByTimeAsync([FromQuery] TimeRangeQuery query)
	{
		return await _queries.GetByTime(query);	
	}

	[HttpGet]
	[Route("extendedLogEntry/{itemId}&{unit}")]
	public async Task<ExtendedInventoryLogEntryViewModel> GetLogEntryByItemAsync([FromRoute] string itemId, [FromRoute] string unit, [FromQuery]TimeRangeQuery query)
	{
		return await _queries.GetEntryByItem(query, itemId, unit);
	}

	[HttpGet]
	[Route("extendedLogEntries/{itemClassId}")]
	public async Task<IEnumerable<ExtendedInventoryLogEntryViewModel>> GetLogEntriesByItemClassAsync([FromRoute] string itemClassId, [FromQuery]TimeRangeQuery query) 
	{
		return await _queries.GetEntriesByItemClass(query, itemClassId);
	}
}
