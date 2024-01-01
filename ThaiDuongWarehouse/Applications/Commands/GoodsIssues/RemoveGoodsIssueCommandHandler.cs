namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsIssues;

public class RemoveGoodsIssueCommandHandler : IRequestHandler<RemoveGoodsIssueCommand, bool>
{
    private readonly IGoodsIssueRepository _goodsIssueRepository;

    public RemoveGoodsIssueCommandHandler(IGoodsIssueRepository goodsIssueRepository)
    {
        _goodsIssueRepository = goodsIssueRepository;
    }

    public async Task<bool> Handle(RemoveGoodsIssueCommand request, CancellationToken cancellationToken)
    {
        var goodsIssue = await _goodsIssueRepository.GetGoodsIssueById(request.GoodsIssueId);
        if (goodsIssue is null)
        {
            throw new EntityNotFoundException(nameof(GoodsIssue), request.GoodsIssueId);
        }

        if (goodsIssue.Entries.Count == 0) 
            _goodsIssueRepository.Remove(goodsIssue);

        return await _goodsIssueRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}