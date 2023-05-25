using ThaiDuongWarehouse.Domain.AggregateModels.LogAggregate;

namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

public class UpdateConfirmedGoodsReceiptCommandHandler : IRequestHandler<UpdateConfirmedGoodsReceiptCommand, bool>
{
    private readonly IGoodsReceiptRepository _goodsReceiptRepository;
    private readonly IItemLotRepository _itemLotRepository;
    private readonly IStorageRepository _storageRepository;
    private readonly IItemRepository _itemRepository;
    public UpdateConfirmedGoodsReceiptCommandHandler(IGoodsReceiptRepository goodsReceiptRepository, 
        IItemLotRepository itemLotRepository, IStorageRepository storageRepository, IItemRepository itemRepository)
    {
        _goodsReceiptRepository = goodsReceiptRepository;
        _itemLotRepository = itemLotRepository;
        _storageRepository = storageRepository;
        _itemRepository = itemRepository;
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
            var itemLot = await _itemLotRepository.GetLotByLotId(modifiedLot.GoodsReceiptLotId);
            var goodsReceiptLot = goodsReceipt.Lots.First(lot => lot.GoodsReceiptLotId == modifiedLot.GoodsReceiptLotId);
            if (goodsReceiptLot is null)
            {
                throw new EntityNotFoundException($"GoodsReceiptLot with Id {modifiedLot.GoodsReceiptLotId} doesn't exist.");
            }
#pragma warning disable CS8604 // Possible null reference argument.
            var location = await _storageRepository.GetLocationById(goodsReceiptLot.LocationId);
#pragma warning restore CS8604 // Possible null reference argument.
            var item = await _itemRepository.GetItemByEntityId(goodsReceiptLot.ItemId);

            double newQuantity = 0;
            if (itemLot is null)
            {
                newQuantity = modifiedLot.Quantity - goodsReceiptLot.Quantity;              
            }
            else
                newQuantity = itemLot.Quantity + (modifiedLot.Quantity - goodsReceiptLot.Quantity);

            if (newQuantity < 0)
                throw new NotImplementedException();

            // raise domain event update Itemlot and InventoryLogEntry
            goodsReceipt.UpdateItemLot(modifiedLot.GoodsReceiptLotId, location.Id, goodsReceiptLot.ItemId, 
                newQuantity, goodsReceiptLot.Unit, goodsReceiptLot.SublotSize, goodsReceiptLot.SublotUnit, goodsReceiptLot.PurchaseOrderNumber,
                goodsReceiptLot.ProductionDate, goodsReceiptLot.ExpirationDate); 
            
            goodsReceipt.AddLogEntry(modifiedLot.GoodsReceiptLotId, item.Id, modifiedLot.Quantity - goodsReceiptLot.Quantity);
            goodsReceipt.SetQuantityPerLot(modifiedLot.GoodsReceiptLotId, modifiedLot.Quantity);  // update GoodsReceiptLot
        }
        _goodsReceiptRepository.Update(goodsReceipt);

        return await _goodsReceiptRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
