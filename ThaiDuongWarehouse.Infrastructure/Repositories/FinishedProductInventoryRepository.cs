using ThaiDuongWarehouse.Domain.AggregateModels.FinishedProductInventoryAggregate;

namespace ThaiDuongWarehouse.Infrastructure.Repositories;
public class FinishedProductInventoryRepository : BaseRepository, IFinishedProductInventoryRepository
{
    public FinishedProductInventoryRepository(WarehouseDbContext context) : base(context)
    {
    }

    public async Task<FinishedProductInventory> Add(FinishedProductInventory finishedProductInventory)
    {
        if (finishedProductInventory.IsTransient())
        {
            var productInventory = await _context.FinishedProductInventories
            .AddAsync(finishedProductInventory);

            return productInventory.Entity;
        }
        else
            throw new DbUpdateException("Unable to add finishedProductInventory to Database.");
    }

    public async Task<FinishedProductInventory?> GetFinishedProductInventory(string itemId, string unit, string PO)
    {
        return await _context.FinishedProductInventories
            .FirstOrDefaultAsync(f => f.Item.ItemId == itemId && f.Item.Unit == unit
                                    && f.PurchaseOrderNumber == PO);
    }

    public void Remove(FinishedProductInventory finishedProductInventory)
    {
        _context.FinishedProductInventories.Remove(finishedProductInventory);
    }

    public void Update(FinishedProductInventory finishedProductInventory)
    {
        _context.FinishedProductInventories.Update(finishedProductInventory);
    }
}
