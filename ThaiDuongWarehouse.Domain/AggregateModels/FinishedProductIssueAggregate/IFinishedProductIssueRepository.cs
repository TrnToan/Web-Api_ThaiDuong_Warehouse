namespace ThaiDuongWarehouse.Domain.AggregateModels.FinishedProductIssueAggregate;
public interface IFinishedProductIssueRepository : IRepository<FinishedProductIssue>
{
    Task<FinishedProductIssue?> GetIssueById(string id);
    Task<FinishedProductIssue> AddAsync(FinishedProductIssue finishedProductIssue);
    void Update(FinishedProductIssue finishedProductIssue);
}
