using ThaiDuongWarehouse.Domain.AggregateModels.ProductInventoryAggregate;

namespace ThaiDuongWarehouse.Api.Applications.Queries.FinishedProductInventories;

public class FinishedProductInventoryQueries : IFinishedProductInventoryQueries
{
    private readonly WarehouseDbContext _context;
    private readonly IMapper _mapper;

    private IQueryable<FinishedProductInventory> _productInventories => _context.FinishedProductInventories
        .AsNoTracking()
        .Include(p => p.Item);

    public FinishedProductInventoryQueries(WarehouseDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<FinishedProductInventoryViewModel>> GetProductInventoriesByItemId(string itemId)
    {
        var productInventories = await _productInventories
            .Where(p => p.Item.ItemId == itemId)
            .ToListAsync();

        var viewModels = _mapper.Map<IEnumerable<FinishedProductInventory>, IEnumerable<FinishedProductInventoryViewModel>>(productInventories);
        return viewModels;
    }

    public async Task<IEnumerable<string>> GetPOs()
    {
        var POs = await _context.FinishedProductInventories
            .Select(p => p.PurchaseOrderNumber)
            .ToArrayAsync();

        return POs;
    }

    public async Task<ExtendedProductInventoryLogEntryViewModel> GetProductInventoryLog(string itemId, string unit, TimeRangeQuery query)
    {
        double beforeQuantity, receivedQuantity, shippedQuantity, afterQuantity;
        Item? item = await _context.Items.FirstOrDefaultAsync(i => i.ItemId == itemId && i.Unit == unit);
        if (item is null)
        {
            throw new EntityNotFoundException($"Item with Id {itemId} & unit {unit} not found.");
        }
        var itemViewModel = _mapper.Map<Item, ItemViewModel>(item);

        IQueryable<FinishedProductReceipt> productReceipts = _context.FinishedProductReceipts
            .AsNoTracking();
        IQueryable<FinishedProductIssue> productIssues = _context.FinisedProductIssues
            .AsNoTracking();

        double previousProductReceiptsQuantity = await productReceipts
            .Where(p => p.Timestamp < query.StartTime)
            .SelectMany(p => p.Entries.Where(e => e.Item.Id == item.Id))
            .SumAsync(e => e.Quantity);

        double previousProductIssuesQuantity = await productIssues
            .Where(p => p.Timestamp < query.StartTime)
            .SelectMany(p => p.Entries.Where(e => e.Item.Id == item.Id))
            .SumAsync(e => e.Quantity);

        beforeQuantity = previousProductReceiptsQuantity - previousProductIssuesQuantity;

        receivedQuantity = await productReceipts
            .Where(p => p.Timestamp >= query.StartTime && p.Timestamp <= query.EndTime)
            .SelectMany(p => p.Entries.Where(e => e.Item.Id == item.Id))
            .SumAsync(p => p.Quantity);

        shippedQuantity = await productIssues
            .Where(p => p.Timestamp >= query.StartTime && p.Timestamp <= query.EndTime)
            .SelectMany(p => p.Entries.Where(e => e.Item.Id == item.Id))
            .SumAsync(p => p.Quantity);

        afterQuantity = beforeQuantity + receivedQuantity - shippedQuantity;
        ExtendedProductInventoryLogEntryViewModel log = new(itemViewModel, beforeQuantity, receivedQuantity, shippedQuantity,
            afterQuantity);

        return log;
    }

    public async Task<IEnumerable<ExtendedProductInventoryLogEntryViewModel>> GetProductInventoryLogs(TimeRangeQuery query)
    {
        var items = await _context.Items
            .AsNoTracking()
            .Where(i => i.ItemClassId == "TP")
            .Skip(query.ItemsPerPage * (query.Page - 1))
            .Take(query.ItemsPerPage)
            .ToListAsync();

        List<ExtendedProductInventoryLogEntryViewModel> logs = new List<ExtendedProductInventoryLogEntryViewModel>();
        foreach(var item in items)
        {
            var log = await GetProductInventoryLog(item.ItemId, item.Unit, query);
            if (log.BeforeQuantity == 0 && log.ReceivedQuantity == 0 && log.ShippedQuantity == 0)
                continue;
            logs.Add(log);
        }
        return logs;
    }

    public async Task<IEnumerable<FinishedProductInventoryViewModel>> GetProductInventoryRecords(DateTime timestamp, string itemId, string unit)
    {
        Item? item = await _context.Items.FirstOrDefaultAsync(i => i.ItemId == itemId && i.Unit == unit);
        if (item is null)
        {
            throw new EntityNotFoundException($"Item with Id {itemId} & unit {unit} not found.");
        }
        ItemViewModel itemVM = _mapper.Map<ItemViewModel>(item);

        IQueryable<FinishedProductReceiptEntry> productReceiptEntries = _context.FinishedProductReceipts
            .AsNoTracking()
            .Where(p => p.Timestamp <= timestamp)
            .SelectMany(p => p.Entries.Where(entry => entry.Item.Id == item.Id));
        IQueryable<FinishedProductIssueEntry> productIssueEntries = _context.FinisedProductIssues
            .AsNoTracking()
            .Where(p => p.Timestamp <= timestamp)
            .SelectMany(p => p.Entries.Where(entry => entry.Item.Id == item.Id));

        var groupProductReceiptEntries = productReceiptEntries.GroupBy(p => p.PurchaseOrderNumber);
        List<FinishedProductInventoryViewModel> viewModels = new ();
        foreach (var group in groupProductReceiptEntries)
        {
            var matchingProductIssueEntries = productIssueEntries.Where(p => p.PurchaseOrderNumber == group.Key);
            double quantity = group.Sum(gr => gr.Quantity) - matchingProductIssueEntries.Sum(gr => gr.Quantity);
            FinishedProductInventoryViewModel viewModel = new(group.Key, quantity, itemVM);
            viewModels.Add(viewModel);
        }
        return viewModels;
    }
}
