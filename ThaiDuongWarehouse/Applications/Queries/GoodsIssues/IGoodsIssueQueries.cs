namespace ThaiDuongWarehouse.Api.Applications.Queries.GoodsIssues;

public interface IGoodsIssueQueries
{
    Task<IEnumerable<GoodsIssueViewModel>> GetAll();
    Task<IEnumerable<string>> GetAllGoodsIssueIds(bool isExported);
    Task<GoodsIssueViewModel?> GetGoodsIssueById(string id);
    Task<IEnumerable<GoodsIssueViewModel>> GetGoodsIssuesByTime(TimeRangeQuery query, bool isExported);
    Task<IList<string>> GetReceivers();
}
