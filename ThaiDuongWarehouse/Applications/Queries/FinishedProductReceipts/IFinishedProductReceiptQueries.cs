namespace ThaiDuongWarehouse.Api.Applications.Queries;

public interface IFinishedProductReceiptQueries
{
    Task<FinishedProductReceiptViewModel?> GetReceiptById(string id);
    Task<IEnumerable<FinishedProductReceiptViewModel>> GetReceiptsAsync(TimeRangeQuery query);
}
