namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsIssues;

public class CreateGoodsIssueCommandHandler : IRequestHandler<CreateGoodsIssueCommand, bool>
{
    private readonly IGoodsIssueRepository _goodsIssueRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IEmployeeRepository _employeeRepository;
    public CreateGoodsIssueCommandHandler(IGoodsIssueRepository goodsIssueRepository, IItemRepository itemRepository, 
        IEmployeeRepository employeeRepository)
    {
        _goodsIssueRepository = goodsIssueRepository;
        _itemRepository = itemRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<bool> Handle(CreateGoodsIssueCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetEmployeeById(request.EmployeeId);
        if (employee is null)
        {
            throw new EntityNotFoundException($"{employee} doesn't exist.");
        }
        var goodsIssue = new GoodsIssue(request.GoodsIssueId, request.PurchaseOrderNumber, request.Timestamp, 
            request.Receiver, employee.Id);

        foreach (var entry in request.Entries)
        {
            var item = await _itemRepository.GetItemById(entry.Item.ItemId);
            if (item is null)
            {
                throw new EntityNotFoundException($"{item} doesn't exist.");
            }

            goodsIssue.AddEntry(item, entry.Unit, entry.RequestedSublotSize, entry.RequestedQuantity);
        }
        _goodsIssueRepository.Add(goodsIssue);

        return await _goodsIssueRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
