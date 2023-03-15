namespace ThaiDuongWarehouse.Infrastructure.Repositories;
public class LotAdjustmentRepository : BaseRepository, ILotAdjustmentRepository
{
    public LotAdjustmentRepository(WarehouseDbContext context) : base(context)
    {
    }

    public LotAdjustment Add(LotAdjustment lotAdjustment)
    {
        if (lotAdjustment.IsTransient())
            return _context.LotAdjustments.Add(lotAdjustment).Entity;

        else return lotAdjustment;
    }

    public async Task<IEnumerable<LotAdjustment>> GetAll()
    {
        return await _context.LotAdjustments.ToListAsync();
    }

    public async Task<IEnumerable<LotAdjustment>> GetConfirmedAdjustments()
    {
        return await _context.LotAdjustments
            .Where(la => la.IsConfirmed == true)
            .ToListAsync();
    }
}
