namespace ThaiDuongWarehouse.Infrastructure.Repositories;
public class ItemLotLocationRepository : BaseRepository
{
    public ItemLotLocationRepository(WarehouseDbContext context) : base(context)
    {
    }

    public async Task<ItemLotLocation?> GetById(int itemLotId, int LocationId)
    {
        return await _context.ItemLotLocations
            .FirstOrDefaultAsync(ill => ill.ItemLotId == itemLotId && ill.LocationId == LocationId);
    }
    public void Update (ItemLotLocation itemLotLocation)
    {
        _context.ItemLotLocations.Update(itemLotLocation);
    }

    public void Remove (ItemLotLocation itemLotLocation)
    {
        _context.ItemLotLocations.Remove(itemLotLocation);
    }
}
