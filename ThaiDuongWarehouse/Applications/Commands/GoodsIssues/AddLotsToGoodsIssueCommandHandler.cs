namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsIssues;

public class AddLotsToGoodsIssueCommandHandler : IRequestHandler<AddLotsToGoodsIssueCommand, bool>
{
    private readonly IGoodsIssueRepository _goodsIssueRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IItemLotRepository _itemLotRepository;
    public AddLotsToGoodsIssueCommandHandler(IGoodsIssueRepository goodsIssueRepository, 
        IEmployeeRepository employeeRepository, IItemLotRepository itemLotRepository)
    {
        _goodsIssueRepository = goodsIssueRepository;
        _employeeRepository = employeeRepository;
        _itemLotRepository = itemLotRepository;
    }

    public async Task<bool> Handle(AddLotsToGoodsIssueCommand request, CancellationToken cancellationToken)
    {
        var goodsIssue = await _goodsIssueRepository.GetGoodsIssueById(request.GoodsIssueId);

        if (goodsIssue is null)
        {
            throw new EntityNotFoundException(nameof(goodsIssue));
        }
        
        foreach(var lotViewmodel in request.GoodsIssueLots)
        {
            var employee = await _employeeRepository.GetEmployeeById(lotViewmodel.EmployeeId);
            if (employee is null)
            {
                throw new EntityNotFoundException(nameof(employee));
            }

            var lot = await _itemLotRepository.GetLotByLotId(lotViewmodel.GoodsIssueLotId);
            if (lot is null)
            {
                throw new EntityNotFoundException($"Itemlot with id {lotViewmodel.GoodsIssueLotId} doesn't exist.");
            }
            if (lot.IsIsolated == true)
            {
                throw new Exception($"Itemlot with id {lotViewmodel.GoodsIssueLotId} is isolated.");
            }

            GoodsIssueLot goodsIssueLot = new (lotViewmodel.GoodsIssueLotId, lotViewmodel.Quantity, lotViewmodel.SublotSize,
                lotViewmodel.Note, employee.Id);

            goodsIssue.Addlot(lotViewmodel.ItemId, goodsIssueLot);
        }

        _goodsIssueRepository.Update(goodsIssue);

        return await _goodsIssueRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
