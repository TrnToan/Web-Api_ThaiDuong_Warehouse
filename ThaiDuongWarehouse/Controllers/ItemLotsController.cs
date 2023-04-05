using ThaiDuongWarehouse.Api.Applications.Queries.ItemLots;

namespace ThaiDuongWarehouse.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ItemLotsController : ControllerBase
{
    private readonly IItemLotQueries _queries;
	public ItemLotsController(IItemLotQueries queries)
	{
		_queries = queries;
	}
	[HttpGet]
	[Route("Isolated")]
	public async Task<IEnumerable<ItemLotViewModel>> GetIsolatedItemLotsAsync()
	{
		return await _queries.GetIsolatedItemLots();
	}
	[HttpGet]
	[Route("{itemLotId}")]
	public async Task<ItemLotViewModel> GetItemLotByLotIdAsync(string itemLotId)
	{
		return await _queries.GetItemLotByLotId(itemLotId);
	}
	[HttpGet]
	[Route("{itemId}")]
	public async Task<IEnumerable<ItemLotViewModel>> GetItemLotByItemIdAsync(string itemId)
	{
		return await _queries.GetItemLotsByItemId(itemId);
	}
	[HttpGet]
	[Route("{purchaseOrderNumber}")]
	public async Task<IEnumerable<ItemLotViewModel>> GetItemLotByPoAsync(string purchaseOrderNumber)
	{
		return await _queries.GetItemLotsByPO(purchaseOrderNumber);
	}
}
