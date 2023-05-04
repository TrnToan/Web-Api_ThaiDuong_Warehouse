namespace ThaiDuongWarehouse.Infrastructure.Repositories;
public class ItemLotRepository : BaseRepository, IItemLotRepository
{
    public ItemLotRepository(WarehouseDbContext context) : base(context)
    {
    }
    public void AddLot(ItemLot itemLot)
    {
        _context.ItemLots.Add(itemLot);
    }

    public void Addlots(IEnumerable<ItemLot> itemLots)
    {
        _context.ItemLots.AddRange(itemLots);
    }

    public async Task<IEnumerable<ItemLot>> GetAll()
    {
        return await _context.ItemLots
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<ItemLot>> GetIsolatedItemLots()
    {
        return await _context.ItemLots
            .AsNoTracking()
            .Where(il => il.IsIsolated == true)
            .ToListAsync();
    }

    public async Task<IEnumerable<ItemLot>> GetLotsByItemId(string itemId, string unit)
    {
        return await _context.ItemLots
            .Where(il => il.Item.ItemId == itemId)
            .Where(il => il.Unit == unit)
            .ToListAsync();
    }

    public async Task<ItemLot?> GetLotByLotId(string lotId)
    {
        return await _context.ItemLots.FirstOrDefaultAsync(il => il.LotId == lotId);
    }

    public async Task<IEnumerable<ItemLot>> GetLotByPO(string purchaseOrderNumber)
    {
        return await _context.ItemLots.Where(il => il.PurchaseOrderNumber == purchaseOrderNumber).ToListAsync();
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
