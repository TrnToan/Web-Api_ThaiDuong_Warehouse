﻿namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsIssues;

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
        var existedGoodsIssue = await _goodsIssueRepository.GetGoodsIssueById(request.GoodsIssueId);
        if (existedGoodsIssue is not null)
        {
            throw new DuplicateRecordException($"GoodsIssue with Id {existedGoodsIssue.GoodsIssueId} already existed.");
        }

        var employee = await _employeeRepository.GetEmployeeById(request.EmployeeId);
        if (employee is null)
        {
            throw new EntityNotFoundException($"Employee with Id {request.EmployeeId} doesn't exist.");
        }
        var goodsIssue = new GoodsIssue(request.GoodsIssueId, DateTime.UtcNow.AddHours(7), request.Receiver, employee.Id);

        foreach (var entry in request.Entries)
        {
            var item = await _itemRepository.GetItemById(entry.ItemId, entry.Unit);
            if (item is null)
            {
                throw new EntityNotFoundException($"Item with Id {entry.ItemId} doesn't exist.");
            }

            goodsIssue.AddEntry(item, entry.RequestedQuantity);
        }
        _goodsIssueRepository.Add(goodsIssue);

        return await _goodsIssueRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
