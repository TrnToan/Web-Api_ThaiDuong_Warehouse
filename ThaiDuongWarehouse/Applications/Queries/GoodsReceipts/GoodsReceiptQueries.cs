using System.Diagnostics;

namespace ThaiDuongWarehouse.Api.Applications.Queries.GoodsReceipt;

public class GoodsReceiptQueries : IGoodsReceiptQueries
{
    private readonly WarehouseDbContext _context;
    private readonly IMapper _mapper;

    private IQueryable<Domain.AggregateModels.GoodsReceiptAggregate.GoodsReceipt> _goodsReceipts => _context.GoodsReceipts
            .AsNoTracking()
            .Include(gr => gr.Employee)
            .Include(gr => gr.Lots)
                .ThenInclude(grl => grl.Item)
            .Include(gr => gr.Lots)
                .ThenInclude(grl => grl.Employee)
            .Include(gr => gr.Lots)
                .ThenInclude(gr => gr.Sublots);
    private IQueryable<GoodsIssueLot> _goodsIssueLots => _context.GoodsIssues
            .AsNoTracking()
            .SelectMany(gi => gi.Entries)
            .SelectMany(e => e.Lots);

    public GoodsReceiptQueries(WarehouseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GoodsReceiptViewModel>> GetAll()
    {
        var goodsReceipts = await _goodsReceipts
            .ToListAsync();
        var goodsReceiptViewModels = _mapper.Map<IEnumerable<GoodsReceiptViewModel>>(goodsReceipts);

        var results = await Filter(goodsReceiptViewModels, _goodsIssueLots);
        return results;
    }

    public async Task<IEnumerable<GoodsReceiptViewModel>> GetCompletedGoodsReceipts()
    {
        var watch = new Stopwatch();
        watch.Start();

        var goodsReceipts = await _goodsReceipts
            .Where(g => g.Lots
                .All(lot => lot.ProductionDate != null &&
                            lot.ExpirationDate != null &&
                            lot.Sublots.Count > 0) &&
                     g.Supplier != null)
            .ToListAsync();

        var goodsReceiptViewModels = _mapper.Map<IEnumerable<GoodsReceiptViewModel>>(goodsReceipts);
        var results = await Filter(goodsReceiptViewModels, _goodsIssueLots);

        watch.Stop();
        Console.WriteLine(watch.ElapsedMilliseconds);
        return results;
    }

    public async Task<GoodsReceiptViewModel?> GetGoodsReceiptById(string goodsReceiptId)
    {
        var goodsReceipt = await _goodsReceipts
            .FirstOrDefaultAsync(gr => gr.GoodsReceiptId == goodsReceiptId);

        var goodsReceiptViewModel = _mapper.Map<GoodsReceiptViewModel?>(goodsReceipt);
        if (goodsReceiptViewModel is null)
        {
            return null;
        }

        foreach (var receiptLot in goodsReceiptViewModel.Lots)
        {
            var itemLot = await _goodsIssueLots
                .FirstOrDefaultAsync(il => il.GoodsIssueLotId == receiptLot.GoodsReceiptLotId);

            if (itemLot is not null)
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
        var goodsReceipts = await _goodsReceipts
            .Where(g => g.Lots
                .Any(lot => lot.ProductionDate == null ||
                          lot.ExpirationDate == null ||
                          lot.Sublots.Count == 0) || 
                     g.Supplier == null)
            .ToListAsync();

        var goodsReceiptViewModels = _mapper.Map<IEnumerable<GoodsReceiptViewModel>>(goodsReceipts);
        var results = await Filter(goodsReceiptViewModels, _goodsIssueLots);
        return results;
    }

    private static async Task<IEnumerable<GoodsReceiptViewModel>> Filter(IEnumerable<GoodsReceiptViewModel>? goodsReceipts, 
        IQueryable<GoodsIssueLot> goodsIssueLots)
    {
        var goodsReceiptLotIds = goodsReceipts.SelectMany(gr => gr.Lots.Select(lot => lot.GoodsReceiptLotId))
            .ToList();
        var exportedGoodsIssueLots = await goodsIssueLots
            .Where(il => goodsReceiptLotIds.Contains(il.GoodsIssueLotId))
            .ToListAsync();

        foreach (var goodsReceipt in goodsReceipts)
        {
            foreach (var receiptLot in goodsReceipt.Lots)
            {
                if (exportedGoodsIssueLots.Exists(il => il.GoodsIssueLotId == receiptLot.GoodsReceiptLotId))
                {
                    receiptLot.IsExported = true;
                }
            }
        }
        return goodsReceipts;
    }
}