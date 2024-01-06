using ThaiDuongWarehouse.Domain.AggregateModels.ItemLotLocationAggregate;

namespace ThaiDuongWarehouse.Infrastructure.Repositories;
public class ItemLotLocationRepository : BaseRepository, IItemLotLocationRepository
{
    public ItemLotLocationRepository(WarehouseDbContext context) : base(context)
    {
    }

    public async Task<ItemLotLocation?> GetByIdAsync(int itemLotId, int locationId)
    {
        return await _context.ItemLotLocations
            .FirstOrDefaultAsync(ill => ill.ItemLotId == itemLotId && ill.LocationId == locationId);
    }
    public void Update (ItemLotLocation itemLotLocation)
    {
        _context.ItemLotLocations.Update(itemLotLocation);
    }

    public void Remove (ItemLotLocation itemLotLocation)
    {
        _context.ItemLotLocations.Remove(itemLotLocation);
    }

    public async Task AddAsync(ItemLotLocation itemLotLocation)
    {
        await _context.ItemLotLocations.AddAsync(itemLotLocation);
    }
}
