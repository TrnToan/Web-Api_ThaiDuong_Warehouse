using ThaiDuongWarehouse.Domain.AggregateModels.LogAggregate;

namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

public class UpdateConfirmedGoodsReceiptCommandHandler : IRequestHandler<UpdateConfirmedGoodsReceiptCommand, bool>
{
    private readonly IGoodsReceiptRepository _goodsReceiptRepository;
    private readonly IItemLotRepository _itemLotRepository;
    private readonly IInventoryLogEntryRepository _inventoryLogEntryRepository;
    public UpdateConfirmedGoodsReceiptCommandHandler(IGoodsReceiptRepository goodsReceiptRepository, 
        IItemLotRepository itemLotRepository, IInventoryLogEntryRepository inventoryLogEntryRepository)
    {
        _goodsReceiptRepository = goodsReceiptRepository;
        _itemLotRepository = itemLotRepository;
        _inventoryLogEntryRepository = inventoryLogEntryRepository;
    }

    public async Task<bool> Handle(UpdateConfirmedGoodsReceiptCommand request, CancellationToken cancellationToken)
    {
        var goodsReceipt = await _goodsReceiptRepository.GetGoodsReceiptById(request.GoodsReceiptId);
        if (goodsReceipt is null)
        {
            throw new EntityNotFoundException($"GoodsReceipt with Id {request.GoodsReceiptId} doesn't exist.");
        }

        if (DateTime.Now - goodsReceipt.Timestamp > TimeSpan.FromDays(30))
        {
            throw new Exception("It is not allowed to modify GoodsReceipt that has existed for more than 1 month.");
        }

        foreach (var modifiedLot in request.GoodsReceiptLots)
        {
            goodsReceipt.SetQuantityPerLot(modifiedLot.GoodsReceiptLotId, modifiedLot.Quantity);

            var itemLot = await _itemLotRepository.GetLotByLotId(modifiedLot.GoodsReceiptLotId);
            if (itemLot is null)
            {
                throw new NotImplementedException("Not implemented.");
            }
            var goodsReceiptLot = goodsReceipt.Lots.First(lot => lot.GoodsReceiptLotId == modifiedLot.GoodsReceiptLotId);
            double newQuantity = itemLot.Quantity + (modifiedLot.Quantity - goodsReceiptLot.Quantity);
            goodsReceipt.UpdateItemLot(modifiedLot.GoodsReceiptLotId, newQuantity);

            var logEntry = await _inventoryLogEntryRepository.GetLogEntry(modifiedLot.GoodsReceiptLotId, goodsReceipt.Timestamp);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            logEntry.UpdateQuantity(modifiedLot.Quantity);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            var entries = await _inventoryLogEntryRepository.GetLatestLogEntries(goodsReceiptLot.ItemId, goodsReceipt.Timestamp);

            for (int i = 0; i < entries.Count - 1; i++)
            {
                entries[i + 1].UpdateEntry(entries[i].BeforeQuantity, entries[i].ChangedQuantity);
            }
        }

        return await _goodsReceiptRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
