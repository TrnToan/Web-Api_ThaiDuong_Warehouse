namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

public class UpdateGoodsReceiptCommandHandler : IRequestHandler<UpdateGoodsReceiptCommand, bool>
{
    private readonly IGoodsReceiptRepository _goodsReceiptRepository;
    private readonly IStorageRepository _storageRepository;

    public UpdateGoodsReceiptCommandHandler(IGoodsReceiptRepository goodsReceiptRepository, IStorageRepository storageRepository)
    {
        _goodsReceiptRepository = goodsReceiptRepository;
        _storageRepository = storageRepository;
    }

    public async Task<bool> Handle(UpdateGoodsReceiptCommand request, CancellationToken cancellationToken)
    {
        var goodsReceipt = await _goodsReceiptRepository.GetGoodsReceiptById(request.GoodsReceiptId);
        if (goodsReceipt is null)
        {
            throw new EntityNotFoundException($"GoodsReceipt with Id {request.GoodsReceiptId} doesn't exist.");
        }

        foreach (var lot in request.GoodsReceiptLots)
        {
            var goodsReceiptLot = goodsReceipt.Lots.FirstOrDefault(l => l.GoodsReceiptLotId == lot.OldGoodsReceiptLotId);
            double changedQuantity = lot.Quantity - goodsReceiptLot.Quantity;

            goodsReceipt.UpdateLot(lot.OldGoodsReceiptLotId, lot.NewGoodsReceiptLotId, lot.Quantity, lot.LocationId, 
                lot.ProductionDate, lot.ExpirationDate, lot.Note);

            var location = await _storageRepository.GetLocationById(lot.LocationId);
            if (lot.LocationId != null && location is null)
            {
                throw new EntityNotFoundException($"Location with Id {lot.LocationId} does not exist");
            }

            goodsReceipt.UpdateItemLotEntity(lot.OldGoodsReceiptLotId, lot.NewGoodsReceiptLotId, location, lot.Quantity, 
                lot.ProductionDate, lot.ExpirationDate);
            
            if (lot.NewGoodsReceiptLotId != null && lot.NewGoodsReceiptLotId != lot.OldGoodsReceiptLotId)
            {
                goodsReceipt.UpdateLogEntry(lot.NewGoodsReceiptLotId, lot.OldGoodsReceiptLotId, goodsReceiptLot.ItemId, 
                    goodsReceipt.Timestamp);
            }

            if (changedQuantity != 0)
            {
                goodsReceipt.AddUpdatedGoodsReceiptLogEntry(lot.NewGoodsReceiptLotId ?? lot.OldGoodsReceiptLotId, goodsReceiptLot.ItemId, 
                    changedQuantity, goodsReceipt.Timestamp);
            }
        }

        _goodsReceiptRepository.Update(goodsReceipt);
        return await _goodsReceiptRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
