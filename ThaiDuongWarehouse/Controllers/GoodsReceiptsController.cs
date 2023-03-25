using ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;
using ThaiDuongWarehouse.Api.Applications.Queries;

namespace ThaiDuongWarehouse.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GoodsReceiptsController : ControllerBase
{
    private readonly IGoodsReceiptQueries _queries;
    private readonly IMediator _mediator;
    public GoodsReceiptsController(IGoodsReceiptQueries queries, IMediator mediator)
    {
        _queries = queries;
        _mediator = mediator;
    }
    [HttpGet]
    [Route("{goodsReceiptId}")]
    public async Task<GoodsReceiptViewModel?> GetGoodsReceiptByIdAsync(string goodsReceiptId)
    {
        return await _queries.GetGoodsReceiptById(goodsReceiptId);
    }
    [HttpGet]
    [Route("ConfirmedGoodsReceipts")]
    public async Task<IEnumerable<GoodsReceiptViewModel>> GetConfirmedGoodsReceiptsAsync()
    {
        return await _queries.GetConfirmedGoodsReceipt(); 
    }
    [HttpGet]
    [Route("UnconfirmedGoodsReceipt")]
    public async Task<IEnumerable<GoodsReceiptViewModel>> GetUnconfirmedGoodsReceiptsAsync()
    {
        return await _queries.GetUnConfirmedGoodsReceipt();
    }
    [HttpGet]
    public async Task<IEnumerable<GoodsReceiptViewModel>> GetGoodsReceiptsAsync([FromQuery] TimeRangeQuery query)
    {
        return await _queries.GetGoodsReceiptsByTime(query);
    }
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] CreateGoodsReceiptCommand command)
    {
        try
        {
            var result = await _mediator.Send(command);

            if(!result)
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
