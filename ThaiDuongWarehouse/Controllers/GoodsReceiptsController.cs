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
    [Route("goodsReceipts/{isCompleted}")]
    public async Task<IEnumerable<GoodsReceiptViewModel>> GetIsConfirmedGoodsReceiptsAsync(bool isCompleted)
    {
        if (isCompleted)
        {
            return await _queries.GetCompletedGoodsReceipts();
        }
        else
        {
            return await _queries.GetUnCompletedGoodsReceipts();
        }
    }

    [HttpGet]
    public async Task<IEnumerable<GoodsReceiptViewModel>> GetAllAsync()
    {
        return await _queries.GetAll();
    }

    [HttpGet]
    [Route("TimeRange/{isCompleted}")]
    public async Task<IEnumerable<GoodsReceiptViewModel>> GetGoodsReceiptsAsync(bool isCompleted, [FromQuery] TimeRangeQuery query)
    {
        return await _queries.GetGoodsReceiptsByTime(query, isCompleted);
    }

    [HttpGet]
    [Route("Suppliers")]
    public async Task<IList<string?>> GetSuppliersAsync()
    {
        return await _queries.GetSuppliers();
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] CreateGoodsReceiptCommand cmd)
    {
        try
        {
            var result = await _mediator.Send(cmd);

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

    [HttpPatch]
    [Route("{goodsReceiptId}/updatedGoodsReceiptLots")]
    public async Task<IActionResult> UpdateGoodsReceiptAsync(string goodsReceiptId, List<UpdateGoodsReceiptLotViewModel> goodsReceiptLots)
    {
        UpdateGoodsReceiptCommand command = new UpdateGoodsReceiptCommand(goodsReceiptId, goodsReceiptLots);
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
    [Route("{goodsReceiptId}/addedGoodsReceiptLots")]
    public async Task<IActionResult> RemoveLotsAsync([FromRoute] string goodsReceiptId, [FromBody] List<CreateGoodsReceiptLotViewModel> goodsReceiptLots)
    {
        AddLotsToGoodsReceiptCommand command = new(goodsReceiptId, goodsReceiptLots);
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
    [Route("{goodsReceiptId}/removedGoodsReceiptLots")]
    public async Task<IActionResult> RemoveLotsAsync([FromRoute] string goodsReceiptId, [FromBody] List<string> goodsReceiptLotIds)
    {
        RemoveGoodsReceiptLotsCommand command = new(goodsReceiptId, goodsReceiptLotIds);
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
    [Route("{goodsReceiptId}")]
    public async Task<IActionResult> RemoveAsync(string goodsReceiptId)
    {
        DeleteGoodsReceiptCommand command = new DeleteGoodsReceiptCommand(goodsReceiptId);
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
