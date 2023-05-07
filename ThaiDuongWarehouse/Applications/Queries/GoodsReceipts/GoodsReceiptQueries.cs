using GoodsReceipt = ThaiDuongWarehouse.Domain.AggregateModels.GoodsReceiptAggregate.GoodsReceipt;

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

    public async Task<IEnumerable<GoodsReceiptViewModel>> GetAll()
    {
        var goodsReceipts = await _context.GoodsReceipts
            .AsNoTracking()
            .Include(gr => gr.Employee)
            .Include(gr => gr.Lots)
                .ThenInclude(grl => grl.Item)
            .Include(gr => gr.Lots)
                .ThenInclude(grl => grl.Employee)
            .ToListAsync();

        return _mapper.Map<IEnumerable<GoodsReceiptViewModel>>(goodsReceipts);
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
            gr.Timestamp.CompareTo(query.StartTime) >= 0 &&
            gr.Timestamp.CompareTo(query.EndTime) <= 0)
            .Where(gr => gr.IsConfirmed == true)
            .OrderByDescending(gr => gr.Timestamp)
            .ToListAsync();

        return _mapper.Map<IEnumerable<Domain.AggregateModels.GoodsReceiptAggregate.GoodsReceipt> ,IEnumerable<GoodsReceiptViewModel>>(goodsReceipts);
    }

    public async Task<IList<string?>> GetPOs()
    {
        var purchaseOrderNumbers = await _context.GoodsReceipts
            .AsNoTracking()
            .SelectMany(gr => gr.Lots)
            .Where(lot => lot.PurchaseOrderNumber != null)
            .Select(lot => lot.PurchaseOrderNumber)
            .Distinct()
            .ToListAsync();

        return purchaseOrderNumbers;
    }

    public async Task<IList<string?>> GetSuppliers()
    {
        var suppliers = await _context.GoodsReceipts
            .AsNoTracking()
            .Select(g => g.Supplier)
            .Distinct()
            .ToListAsync();

        return suppliers;
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
