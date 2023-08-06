using ThaiDuongWarehouse.Api.Applications.Commands.FinishedProductReceipts;

namespace ThaiDuongWarehouse.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FinishedProductReceiptsController : ApiControllerBase
{
    private readonly IFinishedProductReceiptQueries _queries;

    public FinishedProductReceiptsController(IMediator mediator, IFinishedProductReceiptQueries queries) : base(mediator)
    {
        _queries = queries;
    }

    [HttpGet]
    [Route("{productReceiptId}")]
    public async Task<FinishedProductReceiptViewModel?> GetAsync(string productReceiptId)
    {
        return await _queries.GetReceiptById(productReceiptId);
    }

    [HttpGet]
    [Route("Ids")]
    public async Task<IEnumerable<string>> GetReceiptIds()
    {
        return await _queries.GetReceiptIds();
    }

    [HttpGet]
    [Route("TimeRange")]
    public async Task<IEnumerable<FinishedProductReceiptViewModel>> GetByTimeAsync([FromQuery] TimeRangeQuery query)
    {
        query.EndTime = query.EndTime.AddHours(23).AddMinutes(59).AddSeconds(59);
        return await _queries.GetByTime(query);
    }

    [HttpGet]
    [Route("ImportHistory")]
    public async Task<IEnumerable<FinishedProductReceiptEntryViewModel>> GetHistoryRecordsAsync(string? itemId, 
        string? purchaseOrderNumber, [FromQuery] TimeRangeQuery query)
    {
        query.EndTime = query.EndTime.AddHours(23).AddMinutes(59).AddSeconds(59);
        return await _queries.GetHistoryRecords(itemId, purchaseOrderNumber, query);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] CreateFinishedProductReceiptCommand cmd)
    {
        return await CommandAsync(cmd);
    }

    [HttpPatch]
    [Route("{productReceiptId}/updatedEntry")]
    public async Task<IActionResult> UpdateEntryAsync(string productReceiptId, List<UpdateFinishedProductReceiptEntryViewModel> entries)
    {
        UpdateFinishedProductReceiptEntryCommand cmd = new (productReceiptId, entries);
        return await CommandAsync(cmd);
    }

    [HttpPatch]
    [Route("{productReceiptId}/addedEntry")]
    public async Task<IActionResult> AddEntryAsync(string productReceiptId, List<CreateFinishedProductReceiptEntryViewModel> entries)
    {
        AddEntryToFinishedProductReceiptCommand command = new (productReceiptId, entries);
        return await CommandAsync(command);
    }

    [HttpPatch]
    [Route("{productReceiptId}/removedEntry")]
    public async Task<IActionResult> RemoveEntryAsync(string productReceiptId, List<RemoveFinishedProductEntryViewModel> entries)
    {
        RemoveFinishedProductReceiptEntriesCommand command = new (productReceiptId, entries);
        return await CommandAsync(command);
    }

    [HttpDelete]
    public async Task<IActionResult> RemoveAsync(string productReceiptId)
    {
        DeleteFinishedProductReceiptCommand command = new DeleteFinishedProductReceiptCommand(productReceiptId);
        return await CommandAsync(command);
    }
}
