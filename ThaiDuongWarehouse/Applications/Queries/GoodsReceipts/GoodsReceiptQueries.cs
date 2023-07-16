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
            .Include(gr => gr.Lots)
                .ThenInclude(gr => gr.Sublots)
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
            .Include(gr => gr.Lots)
                .ThenInclude(gr => gr.Sublots)
            .Where(g => g.Lots
                .All(lot => lot.ProductionDate != null &&
                            lot.ExpirationDate != null &&
                            lot.Sublots.Count > 0) &&
                     g.Supplier != null)
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
            .Include(gr => gr.Lots)
                .ThenInclude(gr => gr.Sublots)
            .FirstOrDefaultAsync(gr => gr.GoodsReceiptId == goodsReceiptId);

        var goodsReceiptViewModel = _mapper.Map<GoodsReceiptViewModel?>(goodsReceipt);
        foreach (var receiptLot in goodsReceiptViewModel.Lots)
        {
            var itemLot = await _context.ItemLots
                .AsNoTracking()
                .FirstOrDefaultAsync(il => il.LotId == receiptLot.GoodsReceiptLotId);

            if (itemLot is null || receiptLot.Quantity > itemLot.Quantity) 
                receiptLot.IsExported = true;
        }
        return goodsReceiptViewModel;
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

        foreach (var goodsReceipt in resultedGoodsReceipts)
        {
            foreach (var receiptLot in goodsReceipt.Lots)
            {
                var itemLot = await _context.ItemLots
                .AsNoTracking()
                .FirstOrDefaultAsync(il => il.LotId == receiptLot.GoodsReceiptLotId);

                if (itemLot is null || receiptLot.Quantity > itemLot.Quantity)
                    receiptLot.IsExported = true;
            }
        }
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
            .Include(gr => gr.Lots)
                .ThenInclude(gr => gr.Sublots)
            .Where(g => g.Lots
                .Any(lot => lot.ProductionDate == null ||
                          lot.ExpirationDate == null ||
                          lot.Sublots.Count == 0) || 
                     g.Supplier == null)
            .ToListAsync();

        var goodsReceiptViewModels = _mapper.Map<IEnumerable<GoodsReceiptViewModel>>(goodsReceipts);
        return goodsReceiptViewModels;
    }


}
