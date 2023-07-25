namespace ThaiDuongWarehouse.Api.Applications.Queries.FinishedProductIssues;

public interface IFinishedProductIssueQueries
{
    Task<FinishedProductIssueViewModel?> GetProductIssueById(string id);
    Task<IEnumerable<string>> GetAllIds();
    Task<IEnumerable<FinishedProductIssueViewModel>> GetByTime(TimeRangeQuery query);
    Task<IEnumerable<FinishedProductIssueViewModel>> GetHistoryRecords(string? itemClassId, string? itemId, string? purchaseOrderNumber,
        TimeRangeQuery query);
}
