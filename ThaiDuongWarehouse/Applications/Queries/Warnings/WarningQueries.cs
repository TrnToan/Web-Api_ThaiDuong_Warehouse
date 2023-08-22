namespace ThaiDuongWarehouse.Api.Applications.Queries.Warnings;

public class WarningQueries : IWarningQueries
{
    private readonly WarehouseDbContext _context;
    private readonly IMapper _mapper;
    public WarningQueries(WarehouseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ItemLotViewModel>> ExpirationWarnings(int months)
    {
        DateTime warningDateThreshold = DateTime.UtcNow.AddHours(7).AddMonths(months);

        List<ItemLotViewModel> warningLots = await _context.ItemLots
            .AsNoTracking()
            .Include(il => il.ItemLotLocations)
                .ThenInclude(il => il.Location)
            .Include(il => il.Item)
            .Where(il => il.ExpirationDate.HasValue && il.ExpirationDate.Value <= warningDateThreshold)
            .Select(il => _mapper.Map<ItemLot, ItemLotViewModel>(il))
            .ToListAsync();

        return warningLots;
    }

    public async Task<IEnumerable<ItemLotViewModel>> StockLevelWarnings(string itemClassId)
    {
        var itemLots = await _context.ItemLots
        .AsNoTracking()
        .Include(il => il.ItemLotLocations)
            .ThenInclude(il => il.Location)
        .Include(il => il.Item)
        .Where(il => il.Item.ItemClassId == itemClassId)
        .ToListAsync();

        var lowStockItemLots = itemLots
            .GroupBy(il => il.Item)
            .Where(group => group.Sum(il => il.Quantity) <= group.Key.MinimumStockLevel)
            .SelectMany(group => group)
            .ToList();

        return _mapper.Map<IEnumerable<ItemLot>, IEnumerable<ItemLotViewModel>>(lowStockItemLots);
    }
}