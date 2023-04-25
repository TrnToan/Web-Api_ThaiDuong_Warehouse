namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsIssues;

public class ConfirmExportingGoodsIssueLotsCommandHandler : IRequestHandler<ConfirmExportingGoodsIssueLotsCommand, bool>
{
    private readonly IGoodsIssueRepository _goodsIssueRepository;
    private readonly IItemLotRepository _itemLotRepository;

    public ConfirmExportingGoodsIssueLotsCommandHandler(IGoodsIssueRepository goodsIssueRepository, IItemLotRepository itemLotRepository)
    {
        _goodsIssueRepository = goodsIssueRepository;
        _itemLotRepository = itemLotRepository;
    }

    public async Task<bool> Handle(ConfirmExportingGoodsIssueLotsCommand request, CancellationToken cancellationToken)
    {
        GoodsIssue? goodsIssue = await _goodsIssueRepository.GetGoodsIssueById(request.GoodsIssueId);
        if (goodsIssue is null)
        {
            throw new EntityNotFoundException($"Goodsissue with Id {request.GoodsIssueId} doesn't exist.");
        }

        List<ItemLot> itemLots = new();
        foreach (GoodsIssueEntry entry in goodsIssue.Entries)
        {
            foreach (GoodsIssueLot lot in entry.Lots)
            {
                ItemLot? itemLot = await _itemLotRepository.GetLotByLotId(lot.GoodsIssueLotId);
                if (itemLot is null)
                {
                    throw new EntityNotFoundException($"Itemlot with {lot.GoodsIssueLotId} does not exist");
                }
                itemLots.Add(itemLot);
            }
        }
        goodsIssue.Confirm(goodsIssue.Timestamp, itemLots);
        _goodsIssueRepository.Update(goodsIssue);

        return await _goodsIssueRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
