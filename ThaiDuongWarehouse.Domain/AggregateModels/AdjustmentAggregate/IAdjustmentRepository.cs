namespace ThaiDuongWarehouse.Domain.AggregateModels.AdjustmentAggregate;
public interface IAdjustmentRepository : IRepository<LotAdjustment>
{
    Task<IEnumerable<LotAdjustment>> GetAll();
}
