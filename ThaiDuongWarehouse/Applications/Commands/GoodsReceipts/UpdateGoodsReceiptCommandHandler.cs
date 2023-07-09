using ThaiDuongWarehouse.Domain.AggregateModels;

namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

public class UpdateGoodsReceiptCommandHandler : IRequestHandler<UpdateGoodsReceiptCommand, bool>
{
    private readonly IGoodsReceiptRepository _goodsReceiptRepository;
    private readonly IItemLotRepository _itemLotRepository;
    private readonly IStorageRepository _storageRepository;

    public UpdateGoodsReceiptCommandHandler(IGoodsReceiptRepository goodsReceiptRepository, 
        IStorageRepository storageRepository, IItemLotRepository itemLotRepository)
    {
        _goodsReceiptRepository = goodsReceiptRepository;
        _storageRepository = storageRepository;
        _itemLotRepository = itemLotRepository;
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

            var itemLot = await _itemLotRepository.GetLotByLotId(lot.OldGoodsReceiptLotId);
            if (itemLot is null)
                throw new EntityNotFoundException($"ItemLot with Id {lot.OldGoodsReceiptLotId} no longer exists.");

            // Chênh lệch giữa số lượng mới chỉnh sửa và trước khi chính sửa
            double changedQuantity = lot.Quantity - goodsReceiptLot.Quantity;

            // Cập nhật thông tin mới của lô trong phần Lịch sử GoodsReceiptLot (ghi đè mã lô mới hoặc số lượng mới nếu có)
            goodsReceipt.UpdateLot(lot.OldGoodsReceiptLotId, lot.NewGoodsReceiptLotId, lot.Quantity,
                lot.ProductionDate, lot.ExpirationDate, lot.Note);

            List<ItemLotLocation>? itemLotLocations = new ();
            // Kiểm tra nếu người dùng cập nhật các vị trí của lô hàng
            if (lot.ItemLotLocations is not null)
            {
                double totalLotQuantity = 0;
                foreach (var itemLotLocationVM in lot.ItemLotLocations)
                {
                    var location = await _storageRepository.GetLocationById(itemLotLocationVM.LocationId);
                    if (location is null)
                    {
                        throw new EntityNotFoundException($"Location with Id {itemLotLocationVM.LocationId} does not exist");
                    }
                    else
                    {
                        ItemLotLocation itemLotLocation = new (itemLot.Id, location.Id, itemLotLocationVM.QuantityPerLocation); 
                        itemLotLocations.Add(itemLotLocation);
                        totalLotQuantity += itemLotLocationVM.QuantityPerLocation;
                    }
                }
                if (totalLotQuantity != lot.Quantity)
                    throw new InvalidItemLotException($"The sum of quantity per location {totalLotQuantity} is not equal to that of total quantity {lot.Quantity}");
            }            

            // Cập nhật thông tin lô hàng ở mục tồn kho - DomainEvent
            goodsReceipt.UpdateItemLotEntity(lot.OldGoodsReceiptLotId, lot.NewGoodsReceiptLotId, itemLotLocations, lot.Quantity, 
                lot.ProductionDate, lot.ExpirationDate);
            
            // Nếu người dùng thay đổi mã lô thì sửa lại InventoryLogEntry tương ứng
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
