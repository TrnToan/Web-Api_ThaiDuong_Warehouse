using ThaiDuongWarehouse.Domain.AggregateModels.GoodsReceiptAggregate;

namespace ThaiDuongWarehouse.Api.Applications.Queries.Histories.Export;

public class ExportHistoryQueries : IExportHistoryQueries
{
    private readonly WarehouseDbContext _context;
    private readonly IMapper _mapper;
    public ExportHistoryQueries(WarehouseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GoodsIssuesHistoryViewModel>> GetByClassOrItem(TimeRangeQuery query, string? itemClassId, string? itemId)
    {
        var goodsIssues = new List<GoodsIssue>();
        if (itemClassId == null && itemId != null)
        {
            goodsIssues = await _context.GoodsIssues
                .AsNoTracking()
                .Where(g =>
                g.Timestamp.CompareTo(query.StartTime) >= 0 &&
                g.Timestamp.CompareTo(query.EndTime) <= 0)
                .Where(g => g.Entries.Any(e => e.Item.ItemId == itemId))
                .Include(g => g.Entries)
                .ThenInclude(e => e.Lots)
                .ToListAsync();
        }
        else if (itemClassId != null && itemId == null)
        {
            goodsIssues = await _context.GoodsIssues
                .AsNoTracking()
                .Where(g =>
                g.Timestamp.CompareTo(query.StartTime) >= 0 &&
                g.Timestamp.CompareTo(query.EndTime) <= 0)
                .Where(g => g.Entries.Any(e => e.Item.ItemClassId == itemClassId))
                .Include(g => g.Entries)
                .ThenInclude(e => e.Lots)
                .ToListAsync();
        }
        else
            throw new NotImplementedException();

        return _mapper.Map<IEnumerable<GoodsIssuesHistoryViewModel>>(goodsIssues);
    }

    public async Task<IEnumerable<GoodsIssuesHistoryViewModel>> GetByPO(string purchaseOrderNumber)
    {
        var goodsIssues = await _context.GoodsIssues
            .AsNoTracking()
            .Where(g => g.PurchaseOrderNumber == purchaseOrderNumber)
            .Include(g => g.Entries)
            .ThenInclude(e => e.Lots)
            .ToListAsync();

        return _mapper.Map<IEnumerable<GoodsIssuesHistoryViewModel>>(goodsIssues);
    }

    public async Task<IEnumerable<GoodsIssuesHistoryViewModel>> GetByReceiver(TimeRangeQuery query, string receiver)
    {
        var goodsIssues = await _context.GoodsIssues
            .AsNoTracking()
            .Where(g => g.Receiver == receiver)
            .Where(g =>
            g.Timestamp.CompareTo(query.StartTime) >= 0 &&
            g.Timestamp.CompareTo(query.EndTime) <= 0)
            .Include(g => g.Entries)
            .ThenInclude(e => e.Lots)
            .ToListAsync();

        return _mapper.Map<IEnumerable<GoodsIssuesHistoryViewModel>>(goodsIssues);
    }
}
