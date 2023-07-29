namespace ThaiDuongWarehouse.Infrastructure.Repositories;
public class ItemClassRepository : BaseRepository, IItemClassRepository
{
    public ItemClassRepository(WarehouseDbContext context) : base(context)
    {    }

    public async Task<ItemClass?> GetById(string id)
    {
        return await _context.ItemClasses.FirstOrDefaultAsync(i => i.ItemClassId == id);
    }
}
