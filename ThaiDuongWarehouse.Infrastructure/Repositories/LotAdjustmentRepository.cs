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

    public async Task<LotAdjustment?> GetAdjustmentByLotId(string lotId)
    {
        return await _context.LotAdjustments
            .Where(la => la.IsConfirmed == false)
            .FirstOrDefaultAsync(la => la.LotId == lotId);
    }

    public async Task<IEnumerable<LotAdjustment>> GetUnConfirmedAdjustments()
    {
        return await _context.LotAdjustments
            .Where(la => la.IsConfirmed == false)
            .ToListAsync();
    }

    public void RemoveAdjustment(LotAdjustment lotAdjustment)
    {
        _context.LotAdjustments.Remove(lotAdjustment);
    }

    public LotAdjustment Update(LotAdjustment lotAdjustment)
    {
        return _context.LotAdjustments.Update(lotAdjustment).Entity;
    }
}
