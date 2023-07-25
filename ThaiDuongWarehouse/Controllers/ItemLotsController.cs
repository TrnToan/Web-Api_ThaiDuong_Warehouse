using ThaiDuongWarehouse.Api.Applications.Commands.ItemLots;

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
	[Route("{itemId}/TimeRange")]
	public async Task<IEnumerable<ItemLotViewModel>> GetItemLotByItemIdAsync(DateTime timestamp, string itemId)
	{
        timestamp = timestamp.AddHours(23).AddMinutes(59).AddSeconds(59);
        return await _queries.GetItemLotsByItemId(timestamp, itemId);
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

	[HttpDelete]
	public async Task<IActionResult> RemoveLotsAsync([FromQuery] string itemLotId)
	{
		var command = new RemoveItemLotsCommand(itemLotId);
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
