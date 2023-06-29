namespace ThaiDuongWarehouse.Infrastructure.Repositories;
public class ItemLotRepository : BaseRepository, IItemLotRepository
{
    public ItemLotRepository(WarehouseDbContext context) : base(context)
    {
    }
    public void AddLot(ItemLot itemLot)
    {
        _context.ItemLots.AddAsync(itemLot);
    }

    public void Addlots(IEnumerable<ItemLot> itemLots)
    {
        _context.ItemLots.AddRangeAsync(itemLots);
    }

    public async Task<IEnumerable<ItemLot>> GetIsolatedItemLots()
    {
        return await _context.ItemLots
            .Where(il => il.IsIsolated)
            .ToListAsync();
    }

    public async Task<IEnumerable<ItemLot>> GetLotsByItemId(string itemId, string unit)
    {
        return await _context.ItemLots
            .Where(il => il.Item.ItemId == itemId)
            .Where(il => il.Item.Unit == unit)
            .ToListAsync();
    }

    public async Task<ItemLot?> GetLotByLotId(string lotId)
    {
        return await _context.ItemLots.FirstOrDefaultAsync(il => il.LotId == lotId);
    }

    public void RemoveLot(ItemLot itemLot)
    {
        _context.ItemLots.Remove(itemLot);
    }

    public void RemoveLots(IEnumerable<ItemLot> itemLots)
    {
        _context.ItemLots.RemoveRange(itemLots);
        
    }

    public void UpdateLot(ItemLot itemLot)
    {
        _context.ItemLots.Update(itemLot);
    }
}
