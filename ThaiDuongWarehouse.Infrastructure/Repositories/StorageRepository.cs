namespace ThaiDuongWarehouse.Infrastructure.Repositories;
public class StorageRepository : BaseRepository, IStorageRepository
{
    public StorageRepository(WarehouseDbContext context) : base(context)
    {
    }

    public Location Add(Location location)
    {
        return _context.Locations.Add(location).Entity;
    }

    public async Task<IEnumerable<Warehouse>> GetAll()
    {
        return await _context.Warehouses.ToListAsync();
    }

    public async Task<Location?> GetLocationById(string locationId)
    {
        return await _context.Locations.FirstOrDefaultAsync(l => l.LocationId == locationId);
    }

    public async Task<Warehouse?> GetWarehouseById(string warehouseId)
    {
        return await _context.Warehouses
            .Include(w => w.Locations)
            .FirstOrDefaultAsync(w => w.WarehouseId == warehouseId);
    }
}
