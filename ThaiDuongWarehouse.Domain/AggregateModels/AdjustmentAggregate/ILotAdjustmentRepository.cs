namespace ThaiDuongWarehouse.Domain.AggregateModels.AdjustmentAggregate;
public interface ILotAdjustmentRepository : IRepository<LotAdjustment>
{
    Task<LotAdjustment?> GetAdjustmentByLotId(string lotId);
    Task<IEnumerable<LotAdjustment>> GetUnConfirmedAdjustments();
    LotAdjustment Add(LotAdjustment lotAdjustment);
    LotAdjustment Update(LotAdjustment lotAdjustment);
    void RemoveAdjustment(LotAdjustment lotAdjustment);
}
