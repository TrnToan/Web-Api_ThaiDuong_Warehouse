namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsIssues;

public class AddLotsToGoodsIssueCommandHandler : IRequestHandler<AddLotsToGoodsIssueCommand, bool>
{
    private readonly IGoodsIssueRepository _goodsIssueRepository;
    private readonly IEmployeeRepository _employeeRepository;
    public AddLotsToGoodsIssueCommandHandler(IGoodsIssueRepository goodsIssueRepository, IEmployeeRepository employeeRepository)
    {
        _goodsIssueRepository = goodsIssueRepository;
        _employeeRepository = employeeRepository;
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

            GoodsIssueLot goodsIssueLot = new (lotViewmodel.GoodsIssueLotId, lotViewmodel.Quantity, lotViewmodel.SublotSize,
                lotViewmodel.Note, employee.Id);

            goodsIssue.Addlot(lotViewmodel.ItemId, goodsIssueLot);
        }

        _goodsIssueRepository.Update(goodsIssue);

        return await _goodsIssueRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
