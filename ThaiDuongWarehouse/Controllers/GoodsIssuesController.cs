using ThaiDuongWarehouse.Api.Applications.Commands.GoodsIssues;
using ThaiDuongWarehouse.Api.Applications.Queries;

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
    [Route("Ids")]
    public async Task<IList<string>> GetGoodsIssueIdsAsync()
    {
        return await _queries.GetAllGoodsIssueIds();
    }

    [HttpGet]
    [Route("{goodsIssueId}")]
    public async Task<GoodsIssueViewModel?> GetGoodsIssueByIdAsync([FromRoute] string goodsIssueId)
    {
        return await _queries.GetGoodsIssueById(goodsIssueId);
    }

    [HttpGet]
    public async Task<IEnumerable<GoodsIssueViewModel>> GetGoodsIssuesAsync([FromQuery] TimeRangeQuery query)
    {
        return await _queries.GetConfirmedGoodsIssuesByTime(query);
    }

    [HttpGet]
    [Route("Unconfirmed")]
    public async Task<IEnumerable<GoodsIssueViewModel>> GetUnconfirmedGoodsIssuesAsync()
    {
        return await _queries.GetUnconfirmedGoodsIssues();
    }

    [HttpGet]
    [Route("Receivers")]
    public async Task<IList<string>> GetReceiversAsync()
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

    [HttpPatch]
    [Route("Confirm/{goodsIssueId}")]
    public async Task<IActionResult> ConfirmGoodsIssue([FromRoute] string goodsIssueId)
    {
        ConfirmExportingGoodsIssueLotsCommand command = new(goodsIssueId);
        bool result = await _mediator.Send(command);
        try
        {
            if (result != true)
            {
                return BadRequest(result);
            }
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
