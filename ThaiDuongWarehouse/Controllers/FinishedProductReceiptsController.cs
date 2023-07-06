using ThaiDuongWarehouse.Api.Applications.Commands.FinishedProductReceipts;
using ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

namespace ThaiDuongWarehouse.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FinishedProductReceiptsController : ControllerBase
{
    private readonly IMediator _mediator;

    public FinishedProductReceiptsController(IMediator mediator)
    {
        _mediator = mediator;
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
}
