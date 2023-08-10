using ThaiDuongWarehouse.Api.Applications.Commands.GoodsIssues;

namespace ThaiDuongWarehouse.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GoodsIssuesController : ControllerBase
{
    private readonly IGoodsIssueQueries _queries;
    private readonly IMediator _mediator;
    public GoodsIssuesController(IGoodsIssueQueries queries, IMediator mediator)
    {
        _queries = queries;
        _mediator = mediator;
    }

    [HttpGet]
    [Route("all")]
    public async Task<IEnumerable<GoodsIssueViewModel>> GetAllAsync()
    {
        return await _queries.GetAll();
    }

    [HttpGet]
    [Route("Ids")]
    public async Task<IEnumerable<string>> GetGoodsIssueIdsAsync(bool isExported)
    {
        return await _queries.GetAllGoodsIssueIds(isExported);
    }

    [HttpGet]
    [Route("{goodsIssueId}")]
    public async Task<GoodsIssueViewModel?> GetGoodsIssueByIdAsync([FromRoute] string goodsIssueId)
    {
        return await _queries.GetGoodsIssueById(goodsIssueId);
    }

    [HttpGet]
    public async Task<IEnumerable<GoodsIssueViewModel>> GetGoodsIssuesAsync([FromQuery] TimeRangeQuery query, bool isExported)
    {
        query.EndTime = query.EndTime.AddHours(23).AddMinutes(59).AddSeconds(59);
        return await _queries.GetGoodsIssuesByTime(query, isExported);
    }

    [HttpGet]
    [Route("Receivers")]
    public async Task<List<string>> GetReceiversAsync()
    {
        return await _queries.GetReceivers();
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] CreateGoodsIssueCommand command)
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
    [Route("{goodsIssueId}/goodsIssueEntries")]
    public async Task<IActionResult> UpdateEntryAsync(string goodsIssueId, [FromBody] List<UpdateGoodsIssueEntryViewModel> entries)
    {
        UpdateGoodsIssueEntryCommand command = new (goodsIssueId, entries);
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
    [Route("{goodsIssueId}/goodsIssueLots")]
    public async Task<IActionResult> AddLotsAsync([FromRoute] string goodsIssueId, [FromBody] List<CreateGoodsIssueLotViewModel> goodsIssueLots)
    {
        AddLotsToGoodsIssueCommand command = new (goodsIssueId, goodsIssueLots);
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
