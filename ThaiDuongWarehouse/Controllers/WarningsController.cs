using ThaiDuongWarehouse.Api.Applications.Queries.Warnings;

namespace ThaiDuongWarehouse.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WarningsController : ControllerBase
{
	private readonly IWarningQueries _queries;
	public WarningsController(IWarningQueries queries)
	{
		_queries = queries;
	}

	[HttpGet]
	[Route("MinimumStockLevel/{itemClassId}")]
	public async Task<IEnumerable<ItemLotViewModel>> GetMinimumStockLevelWarningsAsync([FromRoute] string itemClassId)
	{
		return await _queries.StockLevelWarnings(itemClassId);
	}

	[HttpGet]
	[Route("ExpirationDate/{months}")]
	public async Task<IEnumerable<ItemLotViewModel>> GetExpirationWarningsAsync([FromRoute] int months)
	{
		return await _queries.ExpirationWarnings(months);
	}
}
