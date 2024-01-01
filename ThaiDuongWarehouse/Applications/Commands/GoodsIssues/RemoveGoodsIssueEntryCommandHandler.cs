using ThaiDuongWarehouse.Domain.AggregateModels;

namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsIssues;

public class RemoveGoodsIssueEntryCommandHandler : IRequestHandler<RemoveGoodsIssueEntryCommand, bool>
{
    private readonly IGoodsIssueRepository _goodsIssueRepository;
    private readonly IItemLotRepository _itemLotRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IStorageRepository _storageRepository;
    private readonly IGoodsReceiptRepository _goodsReceiptRepository;
    private readonly IInventoryLogEntryRepository _inventoryLogEntryRepository;

    public RemoveGoodsIssueEntryCommandHandler(IGoodsIssueRepository goodsIssueRepository, IItemLotRepository itemLotRepository,
        IItemRepository itemRepository, IStorageRepository storageRepository, IGoodsReceiptRepository goodsReceiptRepository,
        IInventoryLogEntryRepository inventoryLogEntryRepository)
    {
        _goodsIssueRepository = goodsIssueRepository;
        _itemLotRepository = itemLotRepository;
        _itemRepository = itemRepository;
        _storageRepository = storageRepository;
        _goodsReceiptRepository = goodsReceiptRepository;
        _inventoryLogEntryRepository = inventoryLogEntryRepository;
    }

    public async Task<bool> Handle(RemoveGoodsIssueEntryCommand request, CancellationToken cancellationToken)
    {
        var goodsIssue = await _goodsIssueRepository.GetGoodsIssueById(request.GoodsIssueId);
        if (goodsIssue is null)
        {
            throw new EntityNotFoundException(nameof(goodsIssue), request.GoodsIssueId);
        }

        var item = await _itemRepository.GetItemById(request.ItemId, request.Unit);
        if (item is null)
        {
            throw new EntityNotFoundException(nameof(item), request.ItemId);
        }
        var goodsIssueEntry = goodsIssue.Entries.Find(e => e.ItemId == item.Id);
        if (goodsIssueEntry is null)
        {
            throw new EntityNotFoundException(nameof(GoodsIssueEntry), request.ItemId);
        }

        if (goodsIssueEntry.Lots.Count > 0)
        {
            var restoredItemLots = new List<ItemLot>();
            var existingItemLots = new List<ItemLot>();
            var logEntries = new List<InventoryLogEntry>();

            await ClassifyItemLots(goodsIssue, goodsIssueEntry.Lots, restoredItemLots, existingItemLots);
            var orderedGoodsIssueLots = await OrderGoodsIssueLots(goodsIssue, goodsIssueEntry, logEntries);
            goodsIssue.RestoreGoodsIssueLots(goodsIssueEntry, orderedGoodsIssueLots, restoredItemLots, existingItemLots);
        }   
        
        goodsIssue.RemoveEntry(request.ItemId, request.Unit);
        _goodsIssueRepository.Update(goodsIssue);
        return await _goodsIssueRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }

#pragma warning disable CS8602
    private async Task ClassifyItemLots(GoodsIssue goodsIssue, List<GoodsIssueLot> restoredGoodsIssueLots,
        List<ItemLot> restoredItemLots, List<ItemLot> existingItemLots)
    {
        foreach (var lot in restoredGoodsIssueLots)
        {
            var itemLot = await _itemLotRepository.GetLotByLotId(lot.GoodsIssueLotId);
            var goodsReceiptLot = await _goodsReceiptRepository.GetGoodsReceiptLotById(lot.GoodsIssueLotId);
            var goodsReceipt = await _goodsReceiptRepository.GetGoodsReceiptById(goodsReceiptLot.GoodsReceiptId);
            if (itemLot is null)
            {
                // lô đã bị xuất hết --> tạo mới lại rồi thêm vào
                int itemId = goodsIssue.Entries.Find(e => e.Id == lot.GoodsIssueEntryId).ItemId;
                var restoreLot = new ItemLot(lot.GoodsIssueLotId, itemId, lot.Quantity, goodsReceipt.Timestamp, goodsReceiptLot.ProductionDate,
                    goodsReceiptLot.ExpirationDate);
                foreach (var sublot in lot.Sublots)
                {
                    var location = await _storageRepository.GetLocationById(sublot.LocationId);
                    ItemLotLocation itemSublot = new(restoreLot.Id, location.Id, sublot.QuantityPerLocation);
                    restoreLot.ItemLotLocations.Add(itemSublot);
                }
                restoredItemLots.Add(restoreLot);
            }
            else
            {
                // lô chưa bị xuất hết --> Cộng bù số lượng xuất vào lô
                itemLot.Update(lot.Quantity);
                foreach (var sublot in lot.Sublots)
                {
                    var itemSublot = itemLot.ItemLotLocations.Find(i => i.Location.LocationId == sublot.LocationId);
                    if (itemSublot is null)
                    {
                        // Nếu vị trí đó hết hàng --> Tạo mới lô tại vị trí đó
                        var location = await _storageRepository.GetLocationById(sublot.LocationId);
                        itemSublot = new ItemLotLocation(itemLot.Id, location.Id, sublot.QuantityPerLocation);
                        itemLot.ItemLotLocations.Add(itemSublot);
                    }
                    else
                        itemSublot.UpdateQuantity(sublot.QuantityPerLocation);
                }
                existingItemLots.Add(itemLot);
            }
        }
    }
#pragma warning restore CS8602

    private async Task<IEnumerable<GoodsIssueLot>> OrderGoodsIssueLots(GoodsIssue goodsIssue, GoodsIssueEntry goodsIssueEntry,
        List<InventoryLogEntry> logEntries)
    {
        foreach (var lot in goodsIssueEntry.Lots)
        {
            var logEntry = await _inventoryLogEntryRepository.GetLogEntry(goodsIssueEntry.ItemId, lot.GoodsIssueLotId, 
                goodsIssue.Timestamp);
            logEntries.Add(logEntry);
        }
        var orderedGoodsIssueLots = goodsIssueEntry.Lots
            .OrderBy(lot => logEntries.Find(e => e.ItemLotId == lot.GoodsIssueLotId)?.TrackingTime);
        return orderedGoodsIssueLots;
    }
}