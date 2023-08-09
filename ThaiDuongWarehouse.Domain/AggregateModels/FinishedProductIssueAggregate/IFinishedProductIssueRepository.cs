namespace ThaiDuongWarehouse.Domain.AggregateModels.FinishedProductIssueAggregate;
public interface IFinishedProductIssueRepository : IRepository<FinishedProductIssue>
{
    Task<FinishedProductIssue?> GetIssueById(string id);
    Task<FinishedProductIssueEntry?> GetProductIssueEntry(string itemId, string unit, string purchaseOrderNumber);
    Task<FinishedProductIssue> AddAsync(FinishedProductIssue finishedProductIssue);
    void Update(FinishedProductIssue finishedProductIssue);
}
