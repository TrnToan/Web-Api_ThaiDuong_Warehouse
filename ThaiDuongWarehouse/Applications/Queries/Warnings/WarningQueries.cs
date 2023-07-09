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
        List<ItemLot> lots = await _context.ItemLots
            .AsNoTracking()
            .Include(il => il.ItemLotLocations)
            .Include(il => il.Item)
            .ToListAsync();

        List<ItemLot> warningLots = new();
        foreach (ItemLot lot in lots)
        {
            if (lot.ExpirationDate == null)
                continue;
            DateTime warningDate = lot.ExpirationDate.Value.AddMonths(-months);
            if (DateTime.Now - warningDate > TimeSpan.Zero)
            {
                warningLots.Add(lot);
            }
        }

        return _mapper.Map<IEnumerable<ItemLot>, IEnumerable<ItemLotViewModel>>(warningLots);
    }

    public async Task<IEnumerable<ItemLotViewModel>> StockLevelWarnings(string itemClassId)
    {
        List<Item> items = await _context.Items
            .AsNoTracking()
            .Where(gi => gi.ItemClassId == itemClassId)
            .ToListAsync();

        List<ItemLot> itemLots = new();
        foreach (Item item in items)
        {
            List<ItemLot> lots = await _context.ItemLots
                .AsNoTracking()
                .Include(il => il.ItemLotLocations)
                .Include(il => il.Item)
                .Where(il => il.Item.ItemId == item.ItemId)
                .ToListAsync();

            double totalQuantity = 0;
            foreach (ItemLot lot in lots)
            {
                totalQuantity += lot.Quantity;
            }
            if (totalQuantity <= item.MinimumStockLevel)
            {
                itemLots.AddRange(lots);
            }
        }

        return _mapper.Map<IEnumerable<ItemLot>, IEnumerable<ItemLotViewModel>>(itemLots);
    }
}