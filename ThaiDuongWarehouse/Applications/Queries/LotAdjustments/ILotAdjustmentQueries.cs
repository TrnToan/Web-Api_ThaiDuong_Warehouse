namespace ThaiDuongWarehouse.Api.Applications.Queries.LotAdjustments;

public interface ILotAdjustmentQueries
{
    Task<IEnumerable<LotAdjustmentViewModel>> GetAll();
    Task<IEnumerable<LotAdjustmentViewModel>> GetIsConfirmedAdjustments(bool isConfirmed);
    Task<IEnumerable<LotAdjustmentViewModel>> GetAdjustmentsByTime(TimeRangeQuery query);
}
