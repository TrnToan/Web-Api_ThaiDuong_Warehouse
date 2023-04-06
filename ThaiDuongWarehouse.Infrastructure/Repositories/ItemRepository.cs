namespace ThaiDuongWarehouse.Infrastructure.Repositories;
public class ItemRepository : BaseRepository, IItemRepository
{
    public ItemRepository(WarehouseDbContext context) : base(context)
    {
    }

    public Item Add(Item item)
    {
        if (item.IsTransient())
        {
            return _context.Items.Add(item).Entity;
        }
        else return item;
    }

    public async Task<IEnumerable<Item>> GetAllAsync()
    {
        return await _context.Items.ToListAsync();
    }

    public async Task<Item?> GetItemById(string itemId, string unit)
    {
        return await _context.Items
            .Where(x => x.ItemId == itemId)
            .Where(x => x.Unit == unit)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Item>> GetItemsByItemId(string itemId)
    {
        return await _context.Items
            .Where(x => x.ItemId == itemId)
            .ToListAsync();
    }

    public Item Update(Item item)
    {
        return _context.Items.Update(item).Entity;
    }
}
