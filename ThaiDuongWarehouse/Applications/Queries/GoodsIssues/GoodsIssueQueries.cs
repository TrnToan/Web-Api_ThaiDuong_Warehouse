using ThaiDuongWarehouse.Domain.AggregateModels.GoodsIssueAggregate;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ThaiDuongWarehouse.Api.Applications.Queries.GoodsIssues;

public class GoodsIssueQueries : IGoodsIssueQueries
{
    private readonly WarehouseDbContext _context;
    private readonly IMapper _mapper;
    public GoodsIssueQueries(WarehouseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GoodsIssueViewModel?> GetGoodsIssueById(string id)
    {
        var goodsIssue = await _context.GoodsIssues
            .AsNoTracking()
            .Include(gi => gi.Employee)
            .Include(gi => gi.Entries)
                .ThenInclude(gie => gie.Item)
            .Include(gi => gi.Entries)
                .ThenInclude(gie => gie.Lots)
                .ThenInclude(gil => gil.Employee)
            .FirstOrDefaultAsync(gi => gi.GoodsIssueId == id);

        return _mapper.Map<GoodsIssue, GoodsIssueViewModel>(goodsIssue);
    }

    public async Task<IEnumerable<GoodsIssueViewModel>> GetConfirmedGoodsIssuesByTime(TimeRangeQuery query)
    {
        var goodsIssues = await _context.GoodsIssues
            .AsNoTracking()
            .Include(gi => gi.Employee)
            .Include(gi => gi.Entries)
                .ThenInclude(gie => gie.Item)
            .Include(gi => gi.Entries)
                .ThenInclude(gie => gie.Lots)
                .ThenInclude(gil => gil.Employee)
            .Where(gi => gi.IsConfirmed == true)
            .Where(gi =>
                gi.Timestamp.CompareTo(query.StartTime) >= 0 &&
                gi.Timestamp.CompareTo(query.EndTime) <= 0)
            .OrderByDescending(gi => gi.Timestamp)
            .ToListAsync();

        return _mapper.Map<IEnumerable<GoodsIssue>, IEnumerable<GoodsIssueViewModel>>(goodsIssues);
    }

    public async Task<IEnumerable<GoodsIssueViewModel>> GetUnconfirmedGoodsIssues()
    {
        var goodsIssues = await _context.GoodsIssues
            .AsNoTracking()
            .Include(gi => gi.Employee)
            .Include(gi => gi.Entries)
                .ThenInclude(gie => gie.Item)
            .Include(gi => gi.Entries)
                .ThenInclude(gie => gie.Lots)
                .ThenInclude(gil => gil.Employee)
            .Where(gi => gi.IsConfirmed == false)
            .OrderByDescending(gi => gi.Timestamp)
            .ToListAsync();

        return _mapper.Map<IEnumerable<GoodsIssue>, IEnumerable<GoodsIssueViewModel>>(goodsIssues);

    }

    public async Task<IEnumerable<GoodsIssueViewModel>> GetAll()
    {
        var goodsIssues = await _context.GoodsIssues
            .AsNoTracking()
            .Include(gi => gi.Employee)
            .Include(gi => gi.Entries)
                .ThenInclude(gie => gie.Item)
            .Include(gi => gi.Entries)
                .ThenInclude(gie => gie.Lots)
                .ThenInclude(gil => gil.Employee)
            .OrderByDescending(gi => gi.Timestamp)
            .ToListAsync();

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
        allReceivers.AddRange(goodsIssueReceivers);
        allReceivers.AddRange(departmentReceivers);

        List<string> receivers = allReceivers
            .Distinct()
            .ToList();
        return receivers;
    }

    public async Task<IList<string>> GetAllGoodsIssueIds()
    {
        var goodsIssueIds = await _context.GoodsIssues
            .AsNoTracking()
            .Select(g => g.GoodsIssueId)
            .ToListAsync();

        return goodsIssueIds;
    }
}
