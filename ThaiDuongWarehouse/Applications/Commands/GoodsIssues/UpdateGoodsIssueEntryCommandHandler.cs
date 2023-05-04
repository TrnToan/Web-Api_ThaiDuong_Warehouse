namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsIssues;

public class UpdateGoodsIssueEntryCommandHandler : IRequestHandler<UpdateGoodsIssueEntryCommand, bool>
{
    private readonly IGoodsIssueRepository _goodsIssueRepository;

    public UpdateGoodsIssueEntryCommandHandler(IGoodsIssueRepository goodsIssueRepository)
    {
        _goodsIssueRepository = goodsIssueRepository;
    }

    public async Task<bool> Handle(UpdateGoodsIssueEntryCommand request, CancellationToken cancellationToken)
    {
        var goodsIssue = await _goodsIssueRepository.GetGoodsIssueById(request.GoodsIssueId);

        if (goodsIssue is null)
        {
            throw new EntityNotFoundException($"GoodsIssue with Id {request.GoodsIssueId} doesn't exist.");
        }

        if (goodsIssue.IsConfirmed)
        {
            throw new Exception("GoodsIssueEntry contained in confirmed GoodsIssue can't be modified.");
        }
        goodsIssue.UpdateEntry(request.ItemId, request.Unit, request.RequestedSublotSize, request.RequestedQuantity);

        _goodsIssueRepository.Update(goodsIssue);
        return await _goodsIssueRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
