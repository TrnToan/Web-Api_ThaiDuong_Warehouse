using ThaiDuongWarehouse.Domain.AggregateModels.AdjustmentAggregate;

namespace ThaiDuongWarehouse.Api.Applications.Queries.LotAdjustments;

public interface ILotAdjustmentQueries
{
    Task<IEnumerable<LotAdjustmentViewModel>> GetAll();
    Task<IEnumerable<LotAdjustmentViewModel>> GetUnconfirmedAdjustments();
    Task<IEnumerable<LotAdjustmentViewModel>> GetAdjustmentsByTime(TimeRangeQuery query);
}
