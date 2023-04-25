using ThaiDuongWarehouse.Domain.AggregateModels.GoodsIssueAggregate;

namespace ThaiDuongWarehouse.Api.Applications.Queries.GoodsIssues;

public interface IGoodsIssueQueries
{
    Task<IEnumerable<GoodsIssueViewModel>> GetAll();
    Task<GoodsIssueViewModel?> GetGoodsIssueById(string id);
    Task<IEnumerable<GoodsIssueViewModel>> GetConfirmedGoodsIssuesByTime(TimeRangeQuery query);
    Task<IEnumerable<GoodsIssueViewModel>> GetUnconfirmedGoodsIssues();
    Task<IList<string>> GetReceivers();
}
