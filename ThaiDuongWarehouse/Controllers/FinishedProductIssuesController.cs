﻿using ThaiDuongWarehouse.Api.Applications.Commands.FinishedProductIssues;

namespace ThaiDuongWarehouse.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FinishedProductIssuesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IFinishedProductIssueQueries _queries;
    public FinishedProductIssuesController(IMediator mediator, IFinishedProductIssueQueries queries)
    {
        _mediator = mediator;
        _queries = queries;
    }

    [HttpGet]
    [Route("{productIssueId}")]
    public async Task<IActionResult> GetFinishedProductIssue(string productIssueId)
    {
        var result = await _queries.GetProductIssueById(productIssueId);
        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpGet]
    [Route("Ids")]
    public async Task<IEnumerable<string>> GetAll()
    {
        return await _queries.GetAllIds();
    }

    [HttpGet]
    [Route("TimeRange")]
    public async Task<IEnumerable<FinishedProductIssueViewModel>> GetByTimeAsync([FromQuery]TimeRangeQuery query)
    {
        return await _queries.GetByTime(query);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(CreateFinishedProductIssueCommand command)
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

    [HttpPatch]
    [Route("addedEntry")]
    public async Task<IActionResult> AddEntriesAsync(AddFinishedProductIssueEntriesCommand command)
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

    [HttpPatch]
    [Route("removedEntry")]
    public async Task<IActionResult> PatchAsync(RemoveFinishedProductIssueEntryCommand command)
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
