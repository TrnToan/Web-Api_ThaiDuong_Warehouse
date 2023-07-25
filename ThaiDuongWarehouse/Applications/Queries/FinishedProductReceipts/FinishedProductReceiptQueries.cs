namespace ThaiDuongWarehouse.Api.Applications.Queries.FinishedProductReceipts;

public class FinishedProductReceiptQueries : IFinishedProductReceiptQueries
{
    private readonly WarehouseDbContext _context;
    private readonly IMapper _mapper;
    private IQueryable<FinishedProductReceipt> _productReceipts => _context.FinishedProductReceipts
        .AsNoTracking()
        .Include(gr => gr.Employee)
        .Include(gr => gr.Entries)
            .ThenInclude(gr => gr.Item);

    public FinishedProductReceiptQueries(WarehouseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<FinishedProductReceiptViewModel?> GetReceiptById(string id)
    {
        var goodsReceipt = await _productReceipts
            .FirstOrDefaultAsync(gr => gr.FinishedProductReceiptId == id);

        return _mapper.Map<FinishedProductReceiptViewModel>(goodsReceipt);
    }

    public async Task<IEnumerable<string>> GetReceiptIds()
    {
        var goodsReceiptIds = await _productReceipts
            .Select(p => p.FinishedProductReceiptId)
            .ToListAsync();

        return goodsReceiptIds;
    }

    public async Task<IEnumerable<FinishedProductReceiptViewModel>> GetByTime(TimeRangeQuery query)
    {
        var productReceipts = await _productReceipts
            .Where(p => p.Timestamp >= query.StartTime &&
                        p.Timestamp <= query.EndTime)
            .ToListAsync();

        var viewModels = _mapper.Map<IEnumerable<FinishedProductReceipt>, IEnumerable<FinishedProductReceiptViewModel>>(productReceipts);
        return viewModels;
    }

    public async Task<IEnumerable<FinishedProductReceiptViewModel>> GetHistoryRecords(string? itemClassId, string? itemId, 
        string? purchaseOrderNumber, TimeRangeQuery query)
    {
        IQueryable<FinishedProductReceipt> productReceipts;
        productReceipts = _productReceipts
            .Where(p => p.Timestamp >= query.StartTime &&
                        p.Timestamp <= query.EndTime);

        if (itemClassId is not null)
        {
            productReceipts = productReceipts
                .Where(p => p.Entries.Any(e => e.Item.ItemClassId == itemClassId))
                .Include(p => p.Entries.Where(e => e.Item.ItemClassId == itemClassId));
        }
        if (itemId is not null)
        {
            productReceipts = productReceipts
                .Where(p => p.Entries.Any(e => e.Item.ItemId == itemId))
                .Include(p => p.Entries.Where(p => p.Item.ItemId == itemId));                
        }
        if (purchaseOrderNumber is not null)
        {
            productReceipts = productReceipts
                .Where(p => p.Entries.Any(e => e.PurchaseOrderNumber == purchaseOrderNumber))
                .Include(p => p.Entries.Where(p => p.PurchaseOrderNumber == purchaseOrderNumber));
        }

        IEnumerable<FinishedProductReceipt> finishedProductReceipts = await productReceipts.ToListAsync();

        var viewModels = _mapper.Map<IEnumerable<FinishedProductReceipt>, IEnumerable<FinishedProductReceiptViewModel>>(finishedProductReceipts);
        return viewModels;
    }
}
