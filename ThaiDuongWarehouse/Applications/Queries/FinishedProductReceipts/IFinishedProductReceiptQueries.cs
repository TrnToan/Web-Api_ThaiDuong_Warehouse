namespace ThaiDuongWarehouse.Api.Applications.Queries;

public interface IFinishedProductReceiptQueries
{
    Task<FinishedProductReceiptViewModel?> GetReceiptById(string id);
    Task<IEnumerable<string>> GetReceiptIds();
    Task<IEnumerable<FinishedProductReceiptViewModel>> GetByTime(TimeRangeQuery query);
    Task<IEnumerable<FinishedProductReceiptViewModel>> GetHistoryRecords(string? itemClassId, string? itemId, string? purchaseOrderNumber,
        TimeRangeQuery query);
}
