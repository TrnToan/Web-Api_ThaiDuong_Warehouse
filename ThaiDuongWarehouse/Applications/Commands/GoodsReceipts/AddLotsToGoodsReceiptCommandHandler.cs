namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

public class AddLotsToGoodsReceiptCommandHandler : IRequestHandler<AddLotsToGoodsReceiptCommand, bool>
{
    private readonly IGoodsReceiptRepository _goodsReceiptRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public AddLotsToGoodsReceiptCommandHandler(IGoodsReceiptRepository goodsReceiptRepository, IItemRepository itemRepository,
        IEmployeeRepository employeeRepository)
    {
        _goodsReceiptRepository = goodsReceiptRepository;
        _itemRepository = itemRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<bool> Handle(AddLotsToGoodsReceiptCommand request, CancellationToken cancellationToken)
    {
        var goodsReceipt = await _goodsReceiptRepository.GetGoodsReceiptByGoodsReceiptId(request.GoodsReceiptId);
        if (goodsReceipt is null)
        {
            throw new EntityNotFoundException(nameof(GoodsReceipt), request.GoodsReceiptId);
        }

        List<ItemLot> itemLots = new List<ItemLot>();
        foreach (var lot in request.GoodsReceiptLots)
        {
            Employee? employee = await _employeeRepository.GetEmployeeById(lot.EmployeeId);
            if (employee is null)
            {
                throw new EntityNotFoundException($"Employee, {lot.EmployeeId}");
            }

            Item? item = await _itemRepository.GetItemById(lot.ItemId, lot.Unit);
            if (item is null)
            {
                throw new EntityNotFoundException($"Item, {lot.ItemId}");
            }

            var goodsReceiptLot = new GoodsReceiptLot(lot.GoodsReceiptLotId, lot.Quantity, employee, item, lot.Note,
                goodsReceipt.Id);
            ItemLot itemLot = new (lot.GoodsReceiptLotId, lot.Quantity, goodsReceipt.Timestamp, item.Id);

            itemLots.Add(itemLot);
            goodsReceipt.AddLot(goodsReceiptLot);
        }
        goodsReceipt.Confirm(itemLots);
        _goodsReceiptRepository.Update(goodsReceipt);

        return await _goodsReceiptRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
