using System.Diagnostics;
using System.Net.WebSockets;

namespace ThaiDuongWarehouse.Api.Applications.Queries.Items;

public class ItemQueries : IItemQueries
{
    private readonly WarehouseDbContext _context;
    private readonly IMapper _mapper;
    private readonly IItemRepository _itemRepository;

    private IQueryable<Item> _items => _context.Items.AsNoTracking();
    public ItemQueries(WarehouseDbContext context, IMapper mapper, IItemRepository itemRepository)
    {
        _context = context;
        _mapper = mapper;
        _itemRepository = itemRepository;
    }

    public async Task<IEnumerable<ItemViewModel>> GetAllItemsAsync(string? itemClassId)
    {
        var watch = new Stopwatch();
        watch.Start();
        List<Item> items;
        if (itemClassId != null)
        {
            items = await _items
                .Where(i => i.ItemClassId == itemClassId)
                .ToListAsync();
        }
        else
        {
            items = await _items
                .ToListAsync();
        }
        //var viewModels = _mapper.Map<IEnumerable<Item>?, IEnumerable<ItemViewModel>>(await _itemRepository.GetItemsByItemClass(itemClassId));
        var viewModels = _mapper.Map<IEnumerable<Item>, IEnumerable<ItemViewModel>>(items);
        watch.Stop();
        Console.WriteLine(watch.ElapsedMilliseconds);
        return viewModels;
    }

    public async Task<ItemViewModel?> GetItemByIdAsync(string itemId, string unit)
    {
        var item = await _items.SingleOrDefaultAsync(i => i.ItemId == itemId && i.Unit == unit);
        return _mapper.Map<Item?, ItemViewModel>(item);
    }
}
