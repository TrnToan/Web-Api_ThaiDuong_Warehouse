﻿using ThaiDuongWarehouse.Domain.AggregateModels.LogAggregate;

namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

public class ConfirmGoodsReceiptCommandHandler : IRequestHandler<ConfirmGoodsReceiptCommand, bool>
{
    private readonly IGoodsReceiptRepository _goodsReceiptRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IStorageRepository _storageRepository;
    public ConfirmGoodsReceiptCommandHandler(IGoodsReceiptRepository goodsReceiptRepository, IItemRepository itemRepository, 
        IStorageRepository storageRepository)
    {
        _goodsReceiptRepository = goodsReceiptRepository;
        _itemRepository = itemRepository;
        _storageRepository = storageRepository;
    }

    public async Task<bool> Handle(ConfirmGoodsReceiptCommand request, CancellationToken cancellationToken)
    {
        var goodsReceipt = await _goodsReceiptRepository.GetGoodsReceiptById(request.GoodsReceiptId);

        if (goodsReceipt is null)
        {
            throw new EntityNotFoundException(nameof(goodsReceipt));
        }

        List<ItemLot> itemLots = new();
        foreach (GoodsReceiptLot lot in goodsReceipt.Lots)
        {
            var items = await _itemRepository.GetItemsByItemId(lot.Item.ItemId);

            bool hasDuplicateUnit = false;
            foreach (Item item in items)
            {
                if (lot.Unit == item.Unit)
                {
                    hasDuplicateUnit = true;
                    break;                   
                }
            }

            if (hasDuplicateUnit == false)
            {
                Item item = new();
                item.CreateItemWithNewUnit(lot.Item.ItemId, lot.Item.ItemClassId, lot.Item.ItemName, lot.Unit);
            }

#pragma warning disable CS8604 // Possible null reference argument.
            Location? location = await _storageRepository.GetLocationById(lot.LocationId);
#pragma warning restore CS8604 // Possible null reference argument.
            if (location is null)
            {
                throw new EntityNotFoundException($"{lot.LocationId} doesn't exist. Create new location.");
            }

            ItemLot itemLot = new(lot.GoodsReceiptLotId, location.Id, lot.ItemId, lot.Quantity, lot.Unit, lot.SublotSize,
                lot.PurchaseOrderNumber, lot.ProductionDate, lot.ExpirationDate);

            // Do đã truyền vào ItemId là ForeignKey của bảng Item, cân nhắc việc bỏ đi đối số lot.Unit

            itemLots.Add(itemLot);
        }

        goodsReceipt.Confirm(DateTime.Now, itemLots);
        _goodsReceiptRepository.Update(goodsReceipt);

        return await _goodsReceiptRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
