using ThaiDuongWarehouse.Domain.AggregateModels.GoodsReceiptAggregate;

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

    public async Task<IEnumerable<GoodsReceiptViewModel>> GetCompletedGoodsReceipts()
    {
        var goodsReceipts = await _context.GoodsReceipts
            .AsNoTracking()
            .Include(gr => gr.Employee)
            .Include(g => g.Lots)
                .ThenInclude(grl => grl.Item)
            .Include(gr => gr.Lots)
                .ThenInclude(gr => gr.Employee)
            .Where(g => g.Lots
                .All(lot => lot.ProductionDate != null &&
                            lot.ExpirationDate != null))
            .ToListAsync();

        var goodsReceiptViewModels = _mapper.Map<IEnumerable<GoodsReceiptViewModel>>(goodsReceipts);
        return goodsReceiptViewModels;
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

    public async Task<IEnumerable<GoodsReceiptViewModel>> GetGoodsReceiptsByTime(TimeRangeQuery query, bool isCompleted)
    {
        IEnumerable<GoodsReceiptViewModel> goodsReceipts;
        if (isCompleted)
        {
            goodsReceipts = await GetCompletedGoodsReceipts();           
        }
        else
        {
            goodsReceipts = await GetUnCompletedGoodsReceipts();
        }
        var resultedGoodsReceipts = goodsReceipts
                .Where(gr => gr.Timestamp >= query.StartTime
                && gr.Timestamp <= query.EndTime)
                .Skip((query.Page - 1) * query.ItemsPerPage)
                .Take(query.ItemsPerPage)
                .ToList();

        return resultedGoodsReceipts;
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

    public async Task<IEnumerable<GoodsReceiptViewModel>> GetUnCompletedGoodsReceipts()
    {
        var goodsReceipts = await _context.GoodsReceipts
            .AsNoTracking()
            .Include(g => g.Employee)
            .Include(g => g.Lots)
                .ThenInclude(grl => grl.Item)
            .Include(gr => gr.Lots)
                .ThenInclude(gr => gr.Employee)
            .Where(g => g.Lots
                .Any(lot => lot.ProductionDate == null ||
                          lot.ExpirationDate == null) || 
                          g.Supplier == null)
            .ToListAsync();

        var goodsReceiptViewModels = _mapper.Map<IEnumerable<GoodsReceiptViewModel>>(goodsReceipts);
        return goodsReceiptViewModels;
    }
}
