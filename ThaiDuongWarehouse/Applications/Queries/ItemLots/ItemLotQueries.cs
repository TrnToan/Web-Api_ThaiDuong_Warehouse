namespace ThaiDuongWarehouse.Api.Applications.Queries.ItemLots;

public class ItemLotQueries : IItemLotQueries
{
    private readonly WarehouseDbContext _context;
    private readonly IMapper _mapper;

    private IQueryable<ItemLot> _itemLots => _context.ItemLots
        .AsNoTracking()
        .Include(il => il.ItemLotLocations)
            .ThenInclude(ill => ill.Location)
        .Include(il => il.Item);

    public ItemLotQueries(WarehouseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ItemLotViewModel>> GetIsolatedItemLots()
    {
        var itemLots = await _itemLots
            .Where(il => il.IsIsolated)
            .ToListAsync();

        var viewModels = _mapper.Map<IEnumerable<ItemLot>, IEnumerable<ItemLotViewModel>>(itemLots);
        return viewModels;
    }

    public async Task<ItemLotViewModel> GetItemLotByLotId(string lotId)
    {
        var itemLot = await _itemLots
            .FirstOrDefaultAsync(il => il.LotId == lotId);

        var viewModel = _mapper.Map<ItemLot?, ItemLotViewModel>(itemLot);
        return viewModel;
    }

    public async Task<IEnumerable<ItemLotViewModel>> GetItemLotsByItemId(DateTime timestamp, string itemId)
    {
        var itemLots = await _itemLots
            .Where(il => il.Item.ItemId == itemId)
            .Where(il => il.Timestamp <= timestamp)
            .Where(il => !il.IsIsolated)
            .ToListAsync();

        var viewModels = _mapper.Map<IEnumerable<ItemLot>, IEnumerable<ItemLotViewModel>>(itemLots);
        return viewModels;
    }

    public async Task<IEnumerable<ItemLotViewModel>> GetAll()
    {
        List<ItemLot> itemlots = await _itemLots
            .ToListAsync();

        var viewModels = _mapper.Map<IEnumerable<ItemLot>, IEnumerable<ItemLotViewModel>>(itemlots);
        return viewModels;
    }

    public async Task<IEnumerable<ItemLotViewModel>> GetItemLots(string itemId)
    {
        List<ItemLot> itemLots = await _itemLots
            .Where(i => i.Item.ItemId == itemId)
            .ToListAsync();

        var viewModels = _mapper.Map<IEnumerable<ItemLot>, IEnumerable<ItemLotViewModel>>(itemLots);
        return viewModels;
    }

    public async Task<IEnumerable<ItemLotViewModel>> GetItemLotsByLocation(string locationId)
    {
        List<ItemLot> itemLots = await _itemLots
            .Where(i => i.ItemLotLocations.Any(ill => ill.Location.LocationId == locationId))
            .ToListAsync();

        var viewModels = _mapper.Map<IEnumerable<ItemLot>, IEnumerable<ItemLotViewModel>>(itemLots);
        return viewModels;
    }
}
