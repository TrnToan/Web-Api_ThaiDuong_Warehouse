﻿namespace ThaiDuongWarehouse.Api.Controllers;

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
	public async Task<IEnumerable<ItemLotViewModel>> GetAllAsync()
	{
		return await _queries.GetAll();
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
	[Route("{itemId}")]
	public async Task<IEnumerable<ItemLotViewModel>> GetItemLots(string itemId)
	{
		return await _queries.GetItemLots(itemId);
	}

    [HttpGet]
    [Route("{locationId}/lots")]
    public async Task<IEnumerable<ItemLotViewModel>> GetItemLotsByLocationAsync(string locationId)
    {
        return await _queries.GetItemLotsByLocation(locationId);
    }

    [HttpPatch]
	[Route("{itemLotId}/Isolate")]
	public async Task<IActionResult> UpdateItemLotStateAsync([FromRoute]string itemLotId, [FromBody]List<IsolatedItemSublotViewModel> isolatedItemSublots)
	{
		var command = new IsolateItemLotCommand(itemLotId, isolatedItemSublots);
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
	public async Task<IActionResult> RemoveLotsAsync([FromQuery] string itemLotId)
	{
		var command = new RemoveItemLotCommand(itemLotId);
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