namespace ThaiDuongWarehouse.Domain.AggregateModels.AdjustmentAggregate;
public interface ILotAdjustmentRepository : IRepository<LotAdjustment>
{
    Task<IEnumerable<LotAdjustment>> GetAll();
    Task<IEnumerable<LotAdjustment>> GetConfirmedAdjustments();
    LotAdjustment Add(LotAdjustment lotAdjustment);
}
