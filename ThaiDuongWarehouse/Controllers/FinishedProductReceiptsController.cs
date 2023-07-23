﻿using ThaiDuongWarehouse.Api.Applications.Commands.FinishedProductReceipts;

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
    [Route("{productReceiptId}")]
    public async Task<FinishedProductReceiptViewModel?> GetAsync(string productReceiptId)
    {
        return await _queries.GetReceiptById(productReceiptId);
    }

    [HttpGet]
    [Route("TimeRange")]
    public async Task<IEnumerable<FinishedProductReceiptViewModel>> GetReceiptsAsync([FromQuery]TimeRangeQuery query)
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
    [Route("{productReceiptId}/updatedEntry")]
    public async Task<IActionResult> PatchAsync(string productReceiptId, List<UpdateFinishedProductReceiptEntryViewModel> entries)
    {
        UpdateFinishedProductReceiptEntryCommand cmd = new (productReceiptId, entries);
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
    [Route("{productReceiptId}/addedEntry")]
    public async Task<IActionResult> AddEntryAsync(string productReceiptId, List<CreateFinishedProductReceiptEntryViewModel> entries)
    {
        AddEntryToFinishedProductReceiptCommand command = new (productReceiptId, entries);
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
    [Route("{productReceiptId}/removedEntry")]
    public async Task<IActionResult> RemoveEntryAsync(string productReceiptId, List<RemoveFinishedProductEntryViewModel> entries)
    {
        RemoveFinishedProductReceiptEntriesCommand command = new (productReceiptId, entries);
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
    public async Task<IActionResult> RemoveAsync(string productReceiptId)
    {
        DeleteFinishedProductReceiptCommand command = new DeleteFinishedProductReceiptCommand(productReceiptId);
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
