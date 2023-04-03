using Microsoft.EntityFrameworkCore.ChangeTracking;
using ThaiDuongWarehouse.Api.Applications.Commands.GoodsIssues;
using ThaiDuongWarehouse.Api.Applications.Queries;
using ThaiDuongWarehouse.Api.Applications.Queries.GoodsIssues;

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
}
