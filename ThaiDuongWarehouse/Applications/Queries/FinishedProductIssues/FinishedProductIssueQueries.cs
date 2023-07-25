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
}
