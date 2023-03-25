﻿namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

public class CreateGoodsReceiptCommandHandler : IRequestHandler<CreateGoodsReceiptCommand, bool>
{
    private readonly IGoodsReceiptRepository _goodsReceiptRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IEmployeeRepository _employeeRepository;
    public CreateGoodsReceiptCommandHandler(IGoodsReceiptRepository goodsReceiptRepository,
        IItemRepository itemRepository, IEmployeeRepository employeeRepository)
    {
        _goodsReceiptRepository = goodsReceiptRepository;
        _itemRepository = itemRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<bool> Handle(CreateGoodsReceiptCommand request, CancellationToken cancellationToken)
    {
        var goodsReceipt = new GoodsReceipt(request.GoodsReceiptId, request.Supplier,
            request.Timestamp, false, request.Employee);
        
        foreach (var receiptLotViewModel in request.GoodsReceiptLots)
        {
            var item = await _itemRepository.GetItemById(receiptLotViewModel.ItemId);
            if (item is null)
            {
                throw new EntityNotFoundException($"{item}");
            }
            
            var employee = await _employeeRepository.GetEmployeeById(receiptLotViewModel.EmployeeId);
            if (employee is null)
            {
                throw new EntityNotFoundException($"{employee}");
            }

            var goodsReceiptLot = new GoodsReceiptLot(receiptLotViewModel.GoodsReceiptLotId,
                receiptLotViewModel.Quantity, employee, item);
            goodsReceipt.AddLot(goodsReceiptLot);
        }
        _goodsReceiptRepository.Add(goodsReceipt);

        return await _goodsReceiptRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}