using Microsoft.Extensions.Caching.Memory;

namespace ThaiDuongWarehouse.Infrastructure.Repositories;
public class ItemRepository : BaseRepository, IItemRepository
{
    private readonly IMemoryCache _cache;
    private readonly string cacheKey = "Item";

    public ItemRepository(WarehouseDbContext context, IMemoryCache cache) : base(context)
    {
        _cache = cache;
    }

    public Item Add(Item item)
    {
        if (item.IsTransient())
        {
            return _context.Items.Add(item).Entity;
        }
        else return item;
    }

    public async Task<Item?> GetItemByEntityId(int Id)
    {
        return await _context.Items
            .Where(i => i.Id == Id)
            .FirstOrDefaultAsync();
    }

    public async Task<Item?> GetItemById(string itemId, string unit)
    {
        return await _context.Items
            .Where(x => x.ItemId == itemId)
            .Where(x => x.Unit == unit)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Item>> GetItemsByItemClass(string? itemClassId)
    {
        if (!_cache.TryGetValue(cacheKey, out IEnumerable<Item> items))
        {
            items = await _context.Items
                .Include(i => i.ItemClass)
                .ToListAsync();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(30))
                .SetPriority(CacheItemPriority.Normal);

            _cache.Set(cacheKey, items, cacheEntryOptions);
        }

        if (itemClassId != null)
            items = _cache.Get<IEnumerable<Item>>(cacheKey).Where(i => i.ItemClass.ItemClassId == itemClassId);

        return items;
    }

    public Item Update(Item item)
    {
        return _context.Items.Update(item).Entity;
    }
}
