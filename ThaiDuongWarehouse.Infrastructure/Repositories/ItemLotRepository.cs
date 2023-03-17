﻿namespace ThaiDuongWarehouse.Infrastructure.Repositories;
public class ItemLotRepository : BaseRepository, IItemLotRepository
{
    public ItemLotRepository(WarehouseDbContext context) : base(context)
    {
    }
    public void AddLot(ItemLot itemLot)
    {
        _context.ItemLots.Add(itemLot);
    }

    public async Task<IEnumerable<ItemLot>> GetAll()
    {
        return await _context.ItemLots.ToListAsync();
    }

    public async Task<IEnumerable<ItemLot>> GetLotByItemId(string itemId)
    {
        return await _context.ItemLots.Where(il => il.Item.ItemId == itemId).ToListAsync();
    }

    public async Task<ItemLot?> GetLotByLotId(string lotId)
    {
        return await _context.ItemLots.FirstOrDefaultAsync(il => il.LotId == lotId);
    }

    public async Task<IEnumerable<ItemLot>> GetLotByPO(string purchaseOrderNumber)
    {
        return await _context.ItemLots.Where(il => il.PurchaseOrderNumber == purchaseOrderNumber).ToListAsync();
    }

    public void UpdateLot(ItemLot itemLot)
    {
        _context.ItemLots.Update(itemLot);
    }
}
