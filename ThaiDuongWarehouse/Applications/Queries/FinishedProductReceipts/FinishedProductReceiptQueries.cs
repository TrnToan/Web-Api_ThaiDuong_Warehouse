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
}
