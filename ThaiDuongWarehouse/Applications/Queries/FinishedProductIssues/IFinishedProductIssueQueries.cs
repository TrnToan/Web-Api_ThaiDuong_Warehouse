namespace ThaiDuongWarehouse.Api.Applications.Queries.FinishedProductIssues;

public interface IFinishedProductIssueQueries
{
    Task<FinishedProductIssueViewModel?> GetProductIssueById(string id);
    Task<IEnumerable<string>> GetAllIds();
    Task<IEnumerable<string?>> GetAllReceivers();
    Task<IEnumerable<FinishedProductIssueViewModel>> GetByTime(TimeRangeQuery query);
    Task<IEnumerable<FinishedProductIssueEntryViewModel>> GetHistoryRecords(string? receiver, string? itemId, 
        string? purchaseOrderNumber, TimeRangeQuery query);
}
