using ThaiDuongWarehouse.Api.Applications.Commands.FinishedProductReceipts;
using ThaiDuongWarehouse.Api.Applications.Queries;

namespace ThaiDuongWarehouse.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FinishedProductReceiptsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IFinishedProductReceiptQueries _queries;

    public FinishedProductReceiptsController(IMediator mediator, IFinishedProductReceiptQueries queries)
    {
        _mediator = mediator;
        _queries = queries;
    }

    [HttpGet]
    [Route("{finishedProductReceiptId}")]
    public async Task<FinishedProductReceiptViewModel?> GetAsync(string finishedProductReceiptId)
    {
        return await _queries.GetReceiptById(finishedProductReceiptId);
    }

    [HttpGet]
    [Route("TimeRange")]
    public async Task<IEnumerable<FinishedProductReceiptViewModel>> GetReceiptsAsync(TimeRangeQuery query)
    {
        return await _queries.GetReceiptsAsync(query);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] CreateFinishedProductReceiptCommand cmd)
    {
        try
        {
            var result = await _mediator.Send(cmd);

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

    [HttpPatch]
    [Route("{finishedProductReceiptId}/updatedEntry")]
    public async Task<IActionResult> PatchAsync(string finishedProductReceiptId, List<UpdateFinishedProductReceiptEntryViewModel> entries)
    {
        UpdateFinishedProductReceiptEntryCommand cmd = new UpdateFinishedProductReceiptEntryCommand(finishedProductReceiptId, entries);
        try
        {
            var result = await _mediator.Send(cmd);

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

    [HttpPatch]
    [Route("{finishedProductReceiptId}/addedEntry")]
    public async Task<IActionResult> AddEntryAsync(string finishedProductReceiptId, List<CreateFinishedProductReceiptEntryViewModel> entries)
    {
        AddEntryToFinishedProductReceiptCommand command = new (finishedProductReceiptId, entries);
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

    [HttpPatch]
    [Route("{finishedProductReceiptId}/removedEntry")]
    public async Task<IActionResult> RemoveEntryAsync(string finishedProductReceiptId, List<RemoveFinishedProductEntryViewModel> entries)
    {
        RemoveFinishedProductReceiptEntriesCommand command = new (finishedProductReceiptId, entries);
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

    [HttpDelete]
    public async Task<IActionResult> RemoveAsync(string finishedProductReceiptId)
    {
        DeleteFinishedProductReceiptCommand command = new DeleteFinishedProductReceiptCommand(finishedProductReceiptId);
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
