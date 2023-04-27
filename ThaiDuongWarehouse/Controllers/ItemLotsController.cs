﻿using ThaiDuongWarehouse.Api.Applications.Commands.ItemLots;

namespace ThaiDuongWarehouse.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemLotsController : ControllerBase
{
    private readonly IItemLotQueries _queries;
	private readonly IMediator _mediator;
	public ItemLotsController(IItemLotQueries queries, IMediator mediator)
    {
        _queries = queries;
        _mediator = mediator;
    }
    [HttpGet]
	[Route("Isolated")]
	public async Task<IEnumerable<ItemLotViewModel>> GetIsolatedItemLotsAsync()
	{
		return await _queries.GetIsolatedItemLots();
	}
	[HttpGet]
	[Route("ByLotId/{itemLotId}")]
	public async Task<ItemLotViewModel> GetItemLotByLotIdAsync(string itemLotId)
	{
		return await _queries.GetItemLotByLotId(itemLotId);
	}
	[HttpGet]
	[Route("ByItemId/{itemId}")]
	public async Task<IEnumerable<ItemLotViewModel>> GetItemLotByItemIdAsync(string itemId)
	{
		return await _queries.GetItemLotsByItemId(itemId);
	}
	[HttpGet]
	[Route("ByPO/{purchaseOrderNumber}")]
	public async Task<IEnumerable<ItemLotViewModel>> GetItemLotByPoAsync(string purchaseOrderNumber)
	{
		return await _queries.GetItemLotsByPO(purchaseOrderNumber);
	}
	[HttpPatch]
	[Route("{itemLotId}")]
	public async Task<IActionResult> UpdateItemLotStateAsync([FromRoute] string itemLotId, bool isIsolated)
	{
		var command = new UpdateItemLotCommand(itemLotId, isIsolated);
		try
		{
			var result = await _mediator.Send(command);
			if (result != true)
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
