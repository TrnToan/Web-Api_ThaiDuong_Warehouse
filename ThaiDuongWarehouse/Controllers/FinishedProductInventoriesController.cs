using ThaiDuongWarehouse.Api.Applications.Queries.FinishedProductInventories;

namespace ThaiDuongWarehouse.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FinishedProductInventoriesController : ControllerBase
{
    private readonly IFinishedProductInventoryQueries _queries;

    public FinishedProductInventoriesController(IFinishedProductInventoryQueries queries)
    {
        _queries = queries;
    }

    [HttpGet]
    [Route("{itemId}")]
    public async Task<IEnumerable<FinishedProductInventoryViewModel>> GetProductInventoriesAsync(string itemId)
    {
        return await _queries.GetProductInventoriesByItemId(itemId);
    }

    [HttpGet]
    [Route("POs")]
    public async Task<IEnumerable<string>> GetPOsAsync()
    {
        return await _queries.GetPOs();
    }

    [HttpGet]
    [Route("extendedProductLogEntry")]
    public async Task<ExtendedProductInventoryLogEntryViewModel> GetLogAsync([FromQuery]string itemId, [FromQuery] string unit, [FromQuery] TimeRangeQuery query)
    {
        query.EndTime = query.EndTime.AddHours(23).AddMinutes(59).AddSeconds(59);
        return await _queries.GetProductInventoryLog(itemId, unit, query);
    }

    [HttpGet]
    [Route("extendedProductLogEntries")]
    public async Task<IEnumerable<ExtendedProductInventoryLogEntryViewModel>> GetLogsAsync([FromQuery] TimeRangeQuery query)
    {
        return await _queries.GetProductInventoryLogs(query);
    }

    [HttpGet]
    [Route("{itemId}/Timestamp")]
    public async Task<IEnumerable<FinishedProductInventoryViewModel>> GetProductInventoryRecordsAsync(string itemId, string unit, DateTime timestamp)
    {
        timestamp = timestamp.AddHours(23).AddMinutes(59).AddSeconds(59);
        return await _queries.GetProductInventoryRecords(timestamp, itemId, unit);
    }
}
