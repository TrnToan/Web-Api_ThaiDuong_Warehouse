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
        var goodsReceipt = await _goodsReceiptRepository.GetGoodsReceiptByGoodsReceiptId(request.GoodsReceiptId);
        if (goodsReceipt is null)
        {
            throw new EntityNotFoundException($"GoodsReceipt with Id {request.GoodsReceiptId} doesn't exist.");
        }

        foreach (var updatedLot in request.GoodsReceiptLots)
        {
            // Truy xuất lô với mã phiếu cũ
            var goodsReceiptLot = goodsReceipt.Lots.Find(l => l.GoodsReceiptLotId == updatedLot.OldGoodsReceiptLotId);
            if (goodsReceiptLot is null)
            {
                throw new EntityNotFoundException($"GoodsReceiptLot with Id {updatedLot.OldGoodsReceiptLotId} does not exist.");
            }

            var itemLot = await _itemLotRepository.GetLotByLotId(updatedLot.OldGoodsReceiptLotId);
            if (itemLot is null)
                continue;

            // Chênh lệch giữa số lượng mới chỉnh sửa và trước khi chính sửa
            double changedQuantity = updatedLot.Quantity - goodsReceiptLot.Quantity;
            
            List<ItemLotLocation>? itemLotLocations = new ();
            List<GoodsReceiptSublot>? sublots = new ();
            // Kiểm tra nếu người dùng cập nhật các vị trí của lô hàng
            if (updatedLot.ItemLotLocations is not null)
            {
                double totalLotQuantity = 0;
                foreach (var itemLotLocationVM in updatedLot.ItemLotLocations)
                {
                    var location = await _storageRepository.GetLocationById(itemLotLocationVM.LocationId);
                    if (location is null)
                    {
                        throw new EntityNotFoundException($"Location with Id {itemLotLocationVM.LocationId} does not exist");
                    }
                    else
                    {
                        GoodsReceiptSublot sublot = new (itemLotLocationVM.LocationId, itemLotLocationVM.QuantityPerLocation);
                        sublots.Add(sublot);
                        
                        ItemLotLocation itemLotLocation = new (itemLot.Id, location.Id, itemLotLocationVM.QuantityPerLocation); 
                        itemLotLocations.Add(itemLotLocation);
                        totalLotQuantity += itemLotLocationVM.QuantityPerLocation;
                    }
                }
                if (totalLotQuantity != updatedLot.Quantity)
                    throw new InvalidItemLotException($"The sum of quantity per location {totalLotQuantity} is not equal to that of total quantity {updatedLot.Quantity}");
            }

            // Cập nhật thông tin mới của lô trong phần Lịch sử GoodsReceiptLot (ghi đè mã lô mới hoặc số lượng mới nếu có)
            goodsReceipt.UpdateLot(updatedLot.OldGoodsReceiptLotId, updatedLot.NewGoodsReceiptLotId, updatedLot.Quantity, sublots,
                updatedLot.ProductionDate, updatedLot.ExpirationDate, updatedLot.Note);

            // Cập nhật thông tin lô hàng ở mục tồn kho - DomainEvent
            goodsReceipt.UpdateItemLotEntity(updatedLot.OldGoodsReceiptLotId, updatedLot.NewGoodsReceiptLotId, itemLotLocations, updatedLot.Quantity, 
                updatedLot.ProductionDate, updatedLot.ExpirationDate);
            
            // Nếu Số lượng hàng trong lô thay đổi thì ghi nhận lại ở InventoryLogEntry
            if (changedQuantity != 0)
            {
                goodsReceipt.UpdateGoodsReceiptLogEntries(updatedLot.OldGoodsReceiptLotId, goodsReceiptLot.ItemId, 
                    updatedLot.Quantity, goodsReceipt.Timestamp);
            }

            // Nếu người dùng thay đổi mã lô thì sửa lại InventoryLogEntry tương ứng
            if (updatedLot.NewGoodsReceiptLotId != null && updatedLot.NewGoodsReceiptLotId != updatedLot.OldGoodsReceiptLotId)
            {
                goodsReceipt.ModifyLogEntry(updatedLot.NewGoodsReceiptLotId, updatedLot.OldGoodsReceiptLotId, goodsReceiptLot.ItemId,
                    goodsReceipt.Timestamp);
            }
        }

        _goodsReceiptRepository.Update(goodsReceipt);
        return await _goodsReceiptRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}