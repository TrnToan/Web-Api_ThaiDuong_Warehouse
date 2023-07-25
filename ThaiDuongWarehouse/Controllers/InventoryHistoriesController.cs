using ThaiDuongWarehouse.Api.Applications.Queries;

namespace ThaiDuongWarehouse.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InventoryHistoriesController : ControllerBase
{
    private readonly IImportHistoryQueries _importQueries;
    private readonly IExportHistoryQueries _exportQueries;
	public InventoryHistoriesController(IImportHistoryQueries importQueries, IExportHistoryQueries exportQueries)
    {
        _importQueries = importQueries;
        _exportQueries = exportQueries;
    }

    [HttpGet]
    [Route("BySupplier/Import")]
    public async Task<IEnumerable<GoodsReceiptHistoryViewModel>> GetImportHistoriesBySupplierAsync([FromQuery]string supplier, [FromQuery]TimeRangeQuery query)
    {
        return await _importQueries.GetBySupplier(query, supplier);
    }

    [HttpGet]
    [Route("Import")]
    public async Task<IEnumerable<GoodsReceiptHistoryViewModel>> GetImportHistoriesByClassOrItemAsync([FromQuery]TimeRangeQuery query, [FromQuery]string? itemClassId, [FromQuery]string? itemId)
    {
        return await _importQueries.GetByClassOrItem(query, itemClassId, itemId);
    }

    [HttpGet]
    [Route("ByReceiver/Export")]
    public async Task<IEnumerable<GoodsIssueHistoryViewModel>> GetExportHistoriesByReceiverAsync([FromQuery]TimeRangeQuery query, string receiver)
    {
        return await _exportQueries.GetByReceiver(query, receiver);
    }

    [HttpGet]
    [Route("Export")]
    public async Task<IEnumerable<GoodsIssueHistoryViewModel>> GetExportedHistoriesByClassOrItemIdAsync([FromQuery]TimeRangeQuery query, string? itemClassId, string? itemId)
    {
        return await _exportQueries.GetByClassOrItem(query, itemClassId, itemId);
    }
}
