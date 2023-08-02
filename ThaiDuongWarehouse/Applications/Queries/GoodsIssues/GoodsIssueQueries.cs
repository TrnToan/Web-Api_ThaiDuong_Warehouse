namespace ThaiDuongWarehouse.Api.Applications.Queries.GoodsIssues;

public class GoodsIssueQueries : IGoodsIssueQueries
{
    private readonly WarehouseDbContext _context;
    private readonly IMapper _mapper;

    private IQueryable<GoodsIssue> _goodsIssues => _context.GoodsIssues
        .Include(gi => gi.Employee)
        .Include(gi => gi.Entries)
            .ThenInclude(gie => gie.Item)
        .Include(gi => gi.Entries)
            .ThenInclude(gie => gie.Lots)
                .ThenInclude(gil => gil.Sublots)
        .Include(gi => gi.Entries)
            .ThenInclude(gie => gie.Lots)
                .ThenInclude(gil => gil.Employee)
        .AsNoTracking();

    public GoodsIssueQueries(WarehouseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GoodsIssueViewModel?> GetGoodsIssueById(string id)
    {
        var goodsIssue = await _goodsIssues
            .FirstOrDefaultAsync(gi => gi.GoodsIssueId == id);

        if (goodsIssue is null)
            throw new EntityNotFoundException($"GoodsIssue with Id {id} does not exist.");

        return _mapper.Map<GoodsIssue, GoodsIssueViewModel>(goodsIssue);
    }

    public async Task<IEnumerable<GoodsIssueViewModel>> GetGoodsIssuesByTime(TimeRangeQuery query, bool isExported)
    {
        List<GoodsIssue> goodsIssues;
        if (isExported)
        {
            goodsIssues = await _goodsIssues
                .Where(gi => gi.Entries.All(gie => gie.Lots.Count != 0) &&
                             gi.Entries.All(gie => gie.RequestedQuantity <= gie.Lots.Sum(lot => lot.Quantity)))
                .Where(g => g.Timestamp >= query.StartTime &&
                            g.Timestamp <= query.EndTime)
                .ToListAsync();
        }
        else
        {
            goodsIssues = await _goodsIssues
                .Where(gi => gi.Entries.Any(gie => gie.Lots.Count == 0) ||
                             gi.Entries.Any(gie => gie.RequestedQuantity > gie.Lots.Sum(lot => lot.Quantity)))
                .Where(g => g.Timestamp >= query.StartTime &&
                            g.Timestamp <= query.EndTime)
                .ToListAsync();
        }

        return _mapper.Map<IEnumerable<GoodsIssue>, IEnumerable<GoodsIssueViewModel>>(goodsIssues);
    }

    public async Task<IEnumerable<GoodsIssueViewModel>> GetAll()
    {
        var goodsIssues = await _goodsIssues.ToListAsync();

        return _mapper.Map<IEnumerable<GoodsIssue>, IEnumerable<GoodsIssueViewModel>>(goodsIssues);
    }

    public async Task<IList<string>> GetReceivers()
    {
        var goodsIssueReceivers = await _context.GoodsIssues
            .AsNoTracking()
            .Select(g => g.Receiver)
            .ToListAsync();

        var departmentReceivers = await _context.Departments
            .AsNoTracking()
            .Select(d => d.Name)
            .ToListAsync();
        
        var allReceivers = new List<string>();
        if (goodsIssueReceivers is not null)
            allReceivers.AddRange(goodsIssueReceivers);

        allReceivers.AddRange(departmentReceivers);

        List<string> receivers = allReceivers
            .Distinct()
            .ToList();
        return receivers;
    }

    public async Task<IEnumerable<string>> GetAllGoodsIssueIds(bool isExported)
    {
        List<string> goodsIssueIds;
        if (isExported)
        {
            goodsIssueIds = await _goodsIssues
                .Where(gi => gi.Entries.All(gie => gie.Lots.Count != 0) && 
                             gi.Entries.All(gie => gie.RequestedQuantity <= gie.Lots.Sum(lot => lot.Quantity)))
                .Select(gi => gi.GoodsIssueId)
                .ToListAsync();
        }
        else
        {
            goodsIssueIds = await _goodsIssues
                .Where(gi => gi.Entries.Any(gie => gie.Lots.Count == 0) || 
                             gi.Entries.Any(gie => gie.RequestedQuantity > gie.Lots.Sum(lot => lot.Quantity)))
                .Select(gi => gi.GoodsIssueId)
                .ToListAsync();
        }

        return goodsIssueIds;
    }
}