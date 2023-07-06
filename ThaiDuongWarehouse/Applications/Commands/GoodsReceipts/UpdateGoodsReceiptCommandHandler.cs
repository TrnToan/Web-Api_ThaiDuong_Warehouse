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
            // Truy xuất lô với mã phiếu cũ
            var goodsReceiptLot = goodsReceipt.Lots.FirstOrDefault(l => l.GoodsReceiptLotId == lot.OldGoodsReceiptLotId);
            if (goodsReceiptLot is null)
            {
                throw new EntityNotFoundException($"GoodsReceiptLot with Id {lot.OldGoodsReceiptLotId} does not exist.");
            }
            // Chênh lệch giữa số lượng mới chỉnh sửa và trước khi chính sửa
            double changedQuantity = lot.Quantity - goodsReceiptLot.Quantity;

            // Cập nhật thông tin mới của lô trong phần Lịch sử GoodsReceiptLot (ghi đè mã lô mới hoặc số lượng mới nếu có)
            goodsReceipt.UpdateLot(lot.OldGoodsReceiptLotId, lot.NewGoodsReceiptLotId, lot.Quantity,
                lot.ProductionDate, lot.ExpirationDate, lot.Note);

            List<Location>? locations = new ();
            // Kiểm tra nếu người dùng cập nhật các vị trí của lô hàng
            if (lot.LocationIds is not null)
            {
                foreach (var locationId in lot.LocationIds)
                {
                    var location = await _storageRepository.GetLocationById(locationId);
                    if (locationId != null && location is null)
                    {
                        throw new EntityNotFoundException($"Location with Id {locationId} does not exist");
                    }
                    if (location is not null)
                    {
                        locations.Add(location);
                    }
                }
            }            

            // Cập nhật thông tin lô hàng ở mục tồn kho - DomainEvent
            goodsReceipt.UpdateItemLotEntity(lot.OldGoodsReceiptLotId, lot.NewGoodsReceiptLotId, locations, lot.Quantity, 
                lot.ProductionDate, lot.ExpirationDate);
            
            // Nếu người dùng thay đổi mã lô thì cập nhật lại InventoryLogEntry tương ứng
            if (lot.NewGoodsReceiptLotId != null && lot.NewGoodsReceiptLotId != lot.OldGoodsReceiptLotId)
            {
                goodsReceipt.UpdateLogEntry(lot.NewGoodsReceiptLotId, lot.OldGoodsReceiptLotId, goodsReceiptLot.ItemId, 
                    goodsReceipt.Timestamp);
            }

            // Nếu Số lượng hàng trong lô thay đổi thì ghi nhận lại ở InventoryLogEntry
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
