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
        query.EndTime = query.EndTime.AddHours(23).AddMinutes(59).AddSeconds(59);
        return await _queries.GetEntriesByItem(itemId, query);
	}

	[HttpGet]
	public async Task<IEnumerable<InventoryLogEntryViewModel>> GetByTimeAsync([FromQuery] TimeRangeQuery query)
	{
		return await _queries.GetEntries(query);	
	}


	[HttpGet]
	[Route("extendedLogEntries")]
	public async Task<QueryResult<ExtendedInventoryLogEntryViewModel>> GetLogEntriesByItemClassAsync(string? itemClassId, string? itemId, [FromQuery]TimeRangeQuery query) 
	{
        query.EndTime = query.EndTime.AddHours(23).AddMinutes(59).AddSeconds(59);
        return await _queries.GetExtendedLogEntries(query, itemClassId, itemId);
	}

	[HttpGet]
	[Route("itemLots")]
	public async Task<ItemLogEntryViewModel> GetItemLotsAsync(DateTime trackingTime, string itemId)
	{
        trackingTime = trackingTime.AddHours(23).AddMinutes(59).AddSeconds(59);
        return await _queries.GetItemLotsLogEntry(trackingTime, itemId);
	}
}
