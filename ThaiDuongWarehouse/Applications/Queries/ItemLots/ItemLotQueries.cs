namespace ThaiDuongWarehouse.Api.Applications.Queries.ItemLots;

public class ItemLotQueries : IItemLotQueries
{
    private readonly WarehouseDbContext _context;
    private readonly IMapper _mapper;
    public ItemLotQueries(WarehouseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ItemLotViewModel>> GetIsolatedItemLots()
    {
        var itemLots = await _context.ItemLots
            .AsNoTracking()
            .Include(il => il.Location)
            .Include(il => il.Item)
            .Where(il => il.IsIsolated == true)
            .ToListAsync();

        var viewModels = _mapper.Map<IEnumerable<ItemLot>, IEnumerable<ItemLotViewModel>>(itemLots);
        return viewModels;
    }

    public async Task<ItemLotViewModel> GetItemLotByLotId(string lotId)
    {
        var itemLot = await _context.ItemLots
            .AsNoTracking()
            .Include(il => il.Location)
            .Include(il => il.Item)
            .FirstOrDefaultAsync(il => il.LotId == lotId);
        var viewModel = _mapper.Map<ItemLot?, ItemLotViewModel>(itemLot);
        return viewModel;
    }

    public async Task<IEnumerable<ItemLotViewModel>> GetItemLotsByItemId(string itemId)
    {
        var itemLots = await _context.ItemLots
            .AsNoTracking()
            .Include(il => il.Location)
            .Include(il => il.Item)
            .Where(il => il.Item.ItemId == itemId)
            .ToListAsync();
        var viewModels = _mapper.Map<IEnumerable<ItemLot>, IEnumerable<ItemLotViewModel>>(itemLots);
        return viewModels;
    }

    public async Task<IEnumerable<ItemLotViewModel>> GetItemLotsByPO(string purchaseOrderNumber)
    {
        var itemLots = await _context.ItemLots
            .AsNoTracking()
            .Include(il => il.Location)
            .Include(il => il.Item)
            .Where(il => il.PurchaseOrderNumber == purchaseOrderNumber)
            .ToListAsync();
        var viewModels = _mapper.Map<IEnumerable<ItemLot>, IEnumerable<ItemLotViewModel>>(itemLots);
        return viewModels;
    }
}
