namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

public class UpdateGoodsReceiptCommandHandler : IRequestHandler<UpdateGoodsReceiptCommand, bool>
{
    private readonly IGoodsReceiptRepository _goodsReceiptRepository;

    public UpdateGoodsReceiptCommandHandler(IGoodsReceiptRepository goodsReceiptRepository)
    {
        _goodsReceiptRepository = goodsReceiptRepository;
    }

    public async Task<bool> Handle(UpdateGoodsReceiptCommand request, CancellationToken cancellationToken)
    {
        var goodsReceipt = await _goodsReceiptRepository.GetGoodsReceiptById(request.GoodsReceiptId);
        if (goodsReceipt is null)
        {
            throw new EntityNotFoundException($"GoodsReceipt with Id {request.GoodsReceiptId} doesn't exist.");
        }
        //if (goodsReceipt.IsConfirmed)
        //{
        //    throw new Exception("Confirmed goodsreceipt is not allowed to modify.");
        //}
        foreach (var lot in request.GoodsReceiptLots)
        {
            goodsReceipt.UpdateLot(lot.GoodsReceiptLotId, lot.Quantity, lot.LocationId, lot.ProductionDate, 
                lot.ExpirationDate, lot.Note);
        }

        _goodsReceiptRepository.Update(goodsReceipt);
        return await _goodsReceiptRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
