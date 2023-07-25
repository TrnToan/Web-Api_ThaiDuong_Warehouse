namespace ThaiDuongWarehouse.Api.Applications.Queries.FinishedProductIssues;

public class FinishedProductIssueQueries : IFinishedProductIssueQueries
{
    private readonly WarehouseDbContext _context;
    private readonly IMapper _mapper;

    private IQueryable<FinishedProductIssue> _productIssues => _context.FinisedProductIssues
        .Include(f => f.Entries)
            .ThenInclude(entry => entry.Item)
        .Include(f => f.Employee)
        .AsNoTracking();

    public FinishedProductIssueQueries(WarehouseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<string>> GetAllIds()
    {
        var productIssueIds = await _productIssues
            .Select(p => p.FinishedProductIssueId)
            .ToArrayAsync();

        return productIssueIds;
    }

    public async Task<FinishedProductIssueViewModel?> GetProductIssueById(string id)
    {
        var productIssue = await _productIssues.FirstOrDefaultAsync(p => p.FinishedProductIssueId == id);
        if (productIssue is null)
        {
            return null;
        }
        var viewModel = _mapper.Map<FinishedProductIssue, FinishedProductIssueViewModel>(productIssue);

        return viewModel;
    }

    public async Task<IEnumerable<FinishedProductIssueViewModel>> GetByTime(TimeRangeQuery query)
    {
        var productIssues = await _productIssues
            .Where(p => p.Timestamp >= query.StartTime &&
                        p.Timestamp <= query.EndTime)
            .ToArrayAsync();

        var viewModels = _mapper.Map<IEnumerable<FinishedProductIssue>, IEnumerable<FinishedProductIssueViewModel>>(productIssues);
        return viewModels;
    }

    public async Task<IEnumerable<FinishedProductIssueViewModel>> GetHistoryRecords(string? itemClassId, string? itemId, 
        string? purchaseOrderNumber, TimeRangeQuery query)
    {
        IQueryable<FinishedProductIssue> productIssues;
        productIssues = _productIssues
            .Where(p => p.Timestamp >= query.StartTime &&
                        p.Timestamp <= query.EndTime);

        if (itemClassId is not null)
        {
            productIssues = productIssues
                .Where(p => p.Entries.Any(e => e.Item.ItemClassId == itemClassId))
                .Include(p => p.Entries.Where(e => e.Item.ItemClassId == itemClassId));
        }
        if (itemId is not null)
        {
            productIssues = productIssues
                .Where(p => p.Entries.Any(e => e.Item.ItemId == itemId))
                .Include(p => p.Entries.Where(p => p.Item.ItemId == itemId));
        }
        if (purchaseOrderNumber is not null)
        {
            productIssues = productIssues
                .Where(p => p.Entries.Any(e => e.PurchaseOrderNumber == purchaseOrderNumber))
                .Include(p => p.Entries.Where(p => p.PurchaseOrderNumber == purchaseOrderNumber));
        }

        IEnumerable<FinishedProductIssue> finishedProductIssues = await productIssues.ToListAsync();

        var viewModels = _mapper.Map<IEnumerable<FinishedProductIssue>, IEnumerable<FinishedProductIssueViewModel>>(finishedProductIssues);
        return viewModels;
    }
}
