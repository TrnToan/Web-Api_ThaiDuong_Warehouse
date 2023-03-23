namespace ThaiDuongWarehouse.Api.Applications.Queries.GoodsReceipt;

public class GoodsReceiptQueries : IGoodsReceiptQueries
{
    private readonly WarehouseDbContext _context;
    private readonly IMapper _mapper;
    public GoodsReceiptQueries(WarehouseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GoodsReceiptViewModel>> GetConfirmedGoodsReceipt()
    {
        var goodsReceipts = await _context.GoodsReceipts
            .AsNoTracking()
            .Include(gr => gr.Employee)
            .Include(gr => gr.Lots)
                .ThenInclude(grl => grl.Item)
            .Include(gr => gr.Lots)
                .ThenInclude(grl => grl.Employee)
            .Where(gr => gr.IsConfirmed == true)
            .ToListAsync();

        return _mapper.Map<IEnumerable<GoodsReceiptViewModel>>(goodsReceipts);
    }

    public async Task<GoodsReceiptViewModel?> GetGoodsReceiptById(string goodsReceiptId)
    {
        var goodsReceipt = await _context.GoodsReceipts
            .AsNoTracking()
            .Include(gr => gr.Employee)
            .Include(gr => gr.Lots)
                .ThenInclude(grl => grl.Item)
            .Include(gr => gr.Lots)
                .ThenInclude(grl => grl.Employee)
            .FirstOrDefaultAsync(gr => gr.GoodsReceiptId == goodsReceiptId);

        return _mapper.Map<GoodsReceiptViewModel?>(goodsReceipt);
    }

    public async Task<IEnumerable<GoodsReceiptViewModel>> GetGoodsReceiptsByTime(TimeRangeQuery query)
    {
        var goodsReceipts = await _context.GoodsReceipts
            .AsNoTracking()
            .Include(gr => gr.Employee)
            .Include(gr => gr.Lots)
                .ThenInclude(grl => grl.Item)
            .Include(gr => gr.Lots)
                .ThenInclude(grl => grl.Employee)
            .Where(gr =>
            gr.Timestamp.CompareTo(query.StartTime) > 0 &&
            gr.Timestamp.CompareTo(query.EndTime) < 0)
            .Where(gr => gr.IsConfirmed == true)
            .ToListAsync();
        goodsReceipts = (List<Domain.AggregateModels.GoodsReceiptAggregate.GoodsReceipt>)goodsReceipts.OrderByDescending(gr => gr.Timestamp);

        return _mapper.Map<IEnumerable<GoodsReceiptViewModel>>(goodsReceipts);
    }

    public async Task<IEnumerable<GoodsReceiptViewModel>> GetUnConfirmedGoodsReceipt()
    {
        var goodsReceipts = await _context.GoodsReceipts
            .AsNoTracking()
            .Include(gr => gr.Employee)
            .Include(gr => gr.Lots)
                .ThenInclude(grl => grl.Item)
            .Include(gr => gr.Lots)
                .ThenInclude(grl => grl.Employee)
            .Where(gr => gr.IsConfirmed == false)
            .ToListAsync();

        var goodsReceiptViewModels = _mapper.Map<IEnumerable<GoodsReceiptViewModel>>(goodsReceipts);
        return goodsReceiptViewModels;
    }
}
