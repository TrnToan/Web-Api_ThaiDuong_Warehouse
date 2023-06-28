namespace ThaiDuongWarehouse.Api.Applications.Queries.Histories.Import;

public class ImportHistoryQueries : IImportHistoryQueries
{
    private readonly WarehouseDbContext _context;
    private readonly IMapper _mapper;
    public ImportHistoryQueries(WarehouseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GoodsReceiptsHistoryViewModel>> GetByClassOrItem(TimeRangeQuery query, string? itemClassId, string? itemId)
    {
        var goodsReceipts = new List<Domain.AggregateModels.GoodsReceiptAggregate.GoodsReceipt>();
        if (itemClassId == null && itemId != null)
        {
            goodsReceipts = await _context.GoodsReceipts
                .AsNoTracking()
                .Where(g => g.Lots.Any(lot => lot.Item.ItemId == itemId))
                .Include(g => g.Lots.Where(lot => lot.Item.ItemId == itemId))
                .ThenInclude(lot => lot.Item)
                .ToListAsync();
        }
        else if (itemClassId != null && itemId == null)
        {
            goodsReceipts = await _context.GoodsReceipts
                .AsNoTracking()
                .Where(g => g.Lots.Any(lot => lot.Item.ItemClassId == itemClassId))
                .Include(g => g.Lots.Where(lot => lot.Item.ItemClassId == itemClassId))
                .ThenInclude(lot => lot.Item)
                .ToListAsync();
        }
        else
            throw new NotImplementedException();

        return _mapper.Map<IEnumerable<GoodsReceiptsHistoryViewModel>>(goodsReceipts);
    }

    //public async Task<IEnumerable<GoodsReceiptsHistoryViewModel>> GetByPO(string purchaseOrderNumber)
    //{
    //    var goodsReceipts = await _context.GoodsReceipts
    //        .AsNoTracking()
    //        .Include(g => g.Lots)
    //        .Where(g => g.Lots.Any(lot => lot.PurchaseOrderNumber == purchaseOrderNumber))
    //        .Include(g => g.Lots.Where(lot => lot.PurchaseOrderNumber == purchaseOrderNumber))
    //        .ThenInclude(lot => lot.Item)
    //        .ToListAsync();

    //    return _mapper.Map<IEnumerable<GoodsReceiptsHistoryViewModel>>(goodsReceipts);
    //}

    public async Task<IEnumerable<GoodsReceiptsHistoryViewModel>> GetBySupplier(TimeRangeQuery query, string supplier)
    {
        var goodsReceipts = await _context.GoodsReceipts
            .AsNoTracking()
            .Where(g => g.Supplier == supplier)
            .Where(g =>
            g.Timestamp.CompareTo(query.StartTime) >= 0 &&
            g.Timestamp.CompareTo(query.EndTime) <= 0)
            .Include(g => g.Lots)
            .ThenInclude(lot => lot.Item)
            .ToListAsync();

        return _mapper.Map<IEnumerable<GoodsReceiptsHistoryViewModel>>(goodsReceipts);
    }
}
