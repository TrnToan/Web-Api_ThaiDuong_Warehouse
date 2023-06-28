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

    //[HttpGet]
    //[Route("ByPO/Import")]
    //public async Task<IEnumerable<GoodsReceiptsHistoryViewModel>> GetImportHistoriesByPOAsync([FromQuery]string purchaseOrderNumber)
    //{
    //    return await _importQueries.GetByPO(purchaseOrderNumber);
    //}

    [HttpGet]
    [Route("BySupplier/Import")]
    public async Task<IEnumerable<GoodsReceiptsHistoryViewModel>> GetImportHistoriesBySupplierAsync([FromQuery]string supplier, [FromQuery]TimeRangeQuery query)
    {
        return await _importQueries.GetBySupplier(query, supplier);
    }

    [HttpGet]
    [Route("Import")]
    public async Task<IEnumerable<GoodsReceiptsHistoryViewModel>> GetImportHistoriesByClassOrItemAsync([FromQuery]TimeRangeQuery query, [FromQuery]string? itemClassId, [FromQuery]string? itemId)
    {
        return await _importQueries.GetByClassOrItem(query, itemClassId, itemId);
    }

    //[HttpGet]
    //[Route("ByPO/Export")]
    //public async Task<IEnumerable<GoodsIssuesHistoryViewModel>> GetExportHistoriesByPOAsync([FromQuery] string purchaseOrderNumber)
    //{
    //    return await _exportQueries.GetByPO(purchaseOrderNumber);
    //}

    [HttpGet]
    [Route("ByReceiver/Export")]
    public async Task<IEnumerable<GoodsIssuesHistoryViewModel>> GetExportHistoriesByReceiverAsync([FromQuery]TimeRangeQuery query, string receiver)
    {
        return await _exportQueries.GetByReceiver(query, receiver);
    }

    [HttpGet]
    [Route("Export")]
    public async Task<IEnumerable<GoodsIssuesHistoryViewModel>> GetExportedHistoriesByClassOrItemIdAsync([FromQuery]TimeRangeQuery query, string? itemClassId, string? itemId)
    {
        return await _exportQueries.GetByClassOrItem(query, itemClassId, itemId);
    }
}
