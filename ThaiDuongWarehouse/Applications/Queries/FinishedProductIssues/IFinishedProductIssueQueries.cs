namespace ThaiDuongWarehouse.Api.Applications.Queries.FinishedProductIssues;

public interface IFinishedProductIssueQueries
{
    Task<FinishedProductIssueViewModel?> GetProductIssueById(string id);
    Task<IEnumerable<FinishedProductIssueViewModel>> GetAll();
}
