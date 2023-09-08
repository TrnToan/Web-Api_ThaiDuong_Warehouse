using System.Diagnostics;
using ThaiDuongWarehouse.Domain.AggregateModels.ProductInventoryAggregate;

namespace ThaiDuongWarehouse.Api.Applications.Queries.FinishedProductInventories;

public class FinishedProductInventoryQueries : IFinishedProductInventoryQueries
{
    private readonly WarehouseDbContext _context;
    private readonly IMapper _mapper;

    private IQueryable<FinishedProductInventory> _productInventories => _context.FinishedProductInventories
        .AsNoTracking()
        .Include(p => p.Item);
    private IQueryable<FinishedProductIssue> _productIssues => _context.FinisedProductIssues
        .AsNoTracking();
    private IQueryable<FinishedProductReceipt> _productReceipts => _context.FinishedProductReceipts
        .AsNoTracking();

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

    private async Task<ExtendedProductInventoryLogEntryViewModel> GetProductInventoryLog(Item item,
        IEnumerable<FinishedProductIssueEntry> previousProductIssueEntries, IEnumerable<FinishedProductReceiptEntry> previousProductReceiptEntries,
        IEnumerable<FinishedProductIssueEntry> productIssueEntries, IEnumerable<FinishedProductReceiptEntry> productReceiptEntries)
    {
        double beforeQuantity, receivedQuantity, shippedQuantity, afterQuantity;
        var itemViewModel = _mapper.Map<Item, ItemViewModel>(item);

        double previousProductReceiptsQuantity = previousProductIssueEntries
            .Where(e => e.Item.Id == item.Id)
            .Sum(e => e.Quantity);

        double previousProductIssuesQuantity = previousProductReceiptEntries
            .Where(e => e.Item.Id == item.Id)
            .Sum(e => e.Quantity);

        beforeQuantity = previousProductReceiptsQuantity - previousProductIssuesQuantity;

        shippedQuantity = productIssueEntries
            .Where(e => e.Item.Id == item.Id)
            .Sum(p => p.Quantity);

        receivedQuantity = productReceiptEntries
            .Where(e => e.Item.Id == item.Id)
            .Sum(p => p.Quantity);

        afterQuantity = beforeQuantity + receivedQuantity - shippedQuantity;
        ExtendedProductInventoryLogEntryViewModel log = new(itemViewModel, beforeQuantity, receivedQuantity, shippedQuantity,
            afterQuantity);

        return log;
    }

    public async Task<QueryResult<ExtendedProductInventoryLogEntryViewModel>> GetProductInventoryLogs(TimeRangeQuery query, 
        string? itemId, string? unit)
    {
        List<ExtendedProductInventoryLogEntryViewModel> logs = new List<ExtendedProductInventoryLogEntryViewModel>();
        var previousProductReceiptEntries = await _productReceipts
            .Where(p => p.Timestamp < query.StartTime)
            .SelectMany(p => p.Entries)
            .Include(e => e.Item)
            .ToListAsync();

        var previousProductIssueEntries = await _productIssues
            .Where(p => p.Timestamp < query.StartTime)
            .SelectMany(p => p.Entries)
            .Include(e => e.Item)
            .ToListAsync();

        var productReceiptEntries = await _productReceipts
            .Where(p => p.Timestamp >= query.StartTime && p.Timestamp <= query.EndTime)
            .SelectMany(p => p.Entries)
            .Include(e => e.Item)
            .ToListAsync();

        var productIssueEntries = await _productIssues
            .Where(p => p.Timestamp >= query.StartTime && p.Timestamp <= query.EndTime)
            .SelectMany(p => p.Entries)
            .Include(e => e.Item)
            .ToListAsync();

        IQueryable<Item> items = _context.Items
            .AsNoTracking()
            .Where(i => i.ItemClassId == "TP");

        if (itemId != null && unit != null)
        {
            items = items.Where(i => i.ItemId == itemId && i.Unit == unit);
        } 

        foreach (var item in items)
        {
            var log = await GetProductInventoryLog(item, previousProductIssueEntries, previousProductReceiptEntries,
                productIssueEntries, productReceiptEntries);
            if (log.ReceivedQuantity != 0 || log.ShippedQuantity != 0)
                logs.Add(log);
        }

        int totalNumOfEntries = logs.Count;
        var logVMs = logs
            .Skip((query.Page - 1) * query.ItemsPerPage)
            .Take(query.ItemsPerPage)
            .ToList();

        QueryResult<ExtendedProductInventoryLogEntryViewModel> viewModels = new (logVMs, totalNumOfEntries);
        return viewModels;
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
            FinishedProductInventoryViewModel viewModel = new (group.Key, quantity, itemVM);
            viewModels.Add(viewModel);
        }
        return viewModels;
    }
}
