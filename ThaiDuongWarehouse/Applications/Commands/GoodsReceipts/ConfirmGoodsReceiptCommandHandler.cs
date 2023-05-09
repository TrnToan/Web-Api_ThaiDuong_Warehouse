﻿using ThaiDuongWarehouse.Domain.AggregateModels.LogAggregate;

namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

public class ConfirmGoodsReceiptCommandHandler : IRequestHandler<ConfirmGoodsReceiptCommand, bool>
{
    private readonly IGoodsReceiptRepository _goodsReceiptRepository;
    private readonly IStorageRepository _storageRepository;
    public ConfirmGoodsReceiptCommandHandler(IGoodsReceiptRepository goodsReceiptRepository, IStorageRepository storageRepository)
    {
        _goodsReceiptRepository = goodsReceiptRepository;
        _storageRepository = storageRepository;
    }

    public async Task<bool> Handle(ConfirmGoodsReceiptCommand request, CancellationToken cancellationToken)
    {
        var goodsReceipt = await _goodsReceiptRepository.GetGoodsReceiptById(request.GoodsReceiptId);

        if (goodsReceipt is null)
        {
            throw new EntityNotFoundException($"GoodsReceipt with Id {request.GoodsReceiptId} doesn't exist.");
        }

        List<ItemLot> itemLots = new();
        foreach (GoodsReceiptLot lot in goodsReceipt.Lots)
        {
#pragma warning disable CS8604 // Possible null reference argument.
            Location? location = await _storageRepository.GetLocationById(lot.LocationId);
#pragma warning restore CS8604 // Possible null reference argument.
            if (location is null)
            {
                throw new EntityNotFoundException($"Location doesn't exist. Create new location.");
            }

            ItemLot itemLot = new(lot.GoodsReceiptLotId, location.Id, lot.ItemId, lot.Quantity, lot.Unit, lot.SublotSize,
                lot.SublotUnit, lot.PurchaseOrderNumber, lot.ProductionDate, lot.ExpirationDate);

            itemLots.Add(itemLot);
        }

        goodsReceipt.Confirm(itemLots);
        _goodsReceiptRepository.Update(goodsReceipt);

        return await _goodsReceiptRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
