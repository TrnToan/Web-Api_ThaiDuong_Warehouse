namespace ThaiDuongWarehouse.Api.Applications.Queries.Items;

public class ItemQueries : IItemQueries
{
    private readonly WarehouseDbContext _context;
    private readonly IMapper _mapper;

    private IQueryable<Item> _items => _context.Items.AsNoTracking();
    public ItemQueries(WarehouseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ItemViewModel>> GetAllItemsAsync(string? itemClassId)
    {
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
        var viewModels = _mapper.Map<IEnumerable<Item>, IEnumerable<ItemViewModel>>(items);

        return viewModels;
    }

    public async Task<ItemViewModel?> GetItemByIdAsync(string itemId, string unit)
    {
        var item = await _items.SingleOrDefaultAsync(i => i.ItemId == itemId && i.Unit == unit);
        return _mapper.Map<Item?, ItemViewModel>(item);
    }
}
