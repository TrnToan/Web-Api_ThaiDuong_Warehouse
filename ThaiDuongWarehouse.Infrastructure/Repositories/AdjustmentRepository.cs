namespace ThaiDuongWarehouse.Infrastructure.Repositories;
public class AdjustmentRepository : BaseRepository, IAdjustmentRepository
{
    public AdjustmentRepository(WarehouseDbContext context) : base(context)
    {
    }

    public IUnitOfWork unitOfWork => throw new NotImplementedException();

    public Task<IEnumerable<LotAdjustment>> GetAll()
    {
        throw new NotImplementedException();
    }
}
