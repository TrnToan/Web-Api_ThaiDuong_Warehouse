using Microsoft.AspNetCore.Mvc;
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
    [Route("goodsReceipts/{isConfirmed}")]
    public async Task<IEnumerable<GoodsReceiptViewModel>> GetIsConfirmedGoodsReceiptsAsync(bool isConfirmed)
    {
        if (isConfirmed)
        {
            return await _queries.GetConfirmedGoodsReceipt();
        }
        else
        {
            return await _queries.GetUnConfirmedGoodsReceipt();
        }
    }

    [HttpGet]
    public async Task<IEnumerable<GoodsReceiptViewModel>> GetAllAsync()
    {
        return await _queries.GetAll();
    }

    [HttpGet]
    [Route("TimeRange")]
    public async Task<IEnumerable<GoodsReceiptViewModel>> GetGoodsReceiptsAsync([FromQuery] TimeRangeQuery query)
    {
        return await _queries.GetGoodsReceiptsByTime(query);
    }

    [HttpGet]
    [Route("Suppliers")]
    public async Task<IList<string?>> GetSuppliersAsync()
    {
        return await _queries.GetSuppliers();
    }

    [HttpGet]
    [Route("PurchaseOrderNumbers")]
    public async Task<IList<string?>> GetPOsAsync()
    {
        return await _queries.GetPOs();
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
    [Route("{goodsReceiptId}")]
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
    [Route("Confirm/{goodsReceiptId}")]
    public async Task<IActionResult> PatchAsync([FromRoute] string goodsReceiptId)
    {
        ConfirmGoodsReceiptCommand command = new (goodsReceiptId);
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
    public async Task<IActionResult> RemoveAsync([FromRoute] string goodsReceiptId)
    {
        RemoveGoodsReceiptCommand command = new(goodsReceiptId);
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
