namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

public class CreateGoodsReceiptCommandHandler : IRequestHandler<CreateGoodsReceiptCommand, bool>
{
    private readonly IGoodsReceiptRepository _goodsReceiptRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IEmployeeRepository _employeeRepository;
    public CreateGoodsReceiptCommandHandler(IGoodsReceiptRepository goodsReceiptRepository, IItemRepository itemRepository, 
        IEmployeeRepository employeeRepository)
    {
        _goodsReceiptRepository = goodsReceiptRepository;
        _itemRepository = itemRepository;
        _employeeRepository = employeeRepository; 
    }

    public async Task<bool> Handle(CreateGoodsReceiptCommand request, CancellationToken cancellationToken)
    {
        var goodsReceiptEmployee = await _employeeRepository.GetEmployeeById(request.EmployeeId);
        if (goodsReceiptEmployee is null)
        {
            throw new EntityNotFoundException($"Employee in the GoodsReceipt {request.GoodsReceiptId} doesn't exist in the context.");
        }

        var existedGoodsReceipt = await _goodsReceiptRepository.GetGoodsReceiptById(request.GoodsReceiptId);
        if (existedGoodsReceipt is not null)
        {
            throw new DuplicateRecordException(nameof(GoodsReceipt), existedGoodsReceipt.GoodsReceiptId);
        }
   
        var goodsReceipt = new GoodsReceipt(request.GoodsReceiptId, request.Supplier, DateTime.UtcNow.AddHours(7), goodsReceiptEmployee);
        var itemLots = new List<ItemLot>();
        
        foreach (var receiptLotViewModel in request.GoodsReceiptLots)
        {
            Item? item = await _itemRepository.GetItemById(receiptLotViewModel.ItemId, receiptLotViewModel.Unit);
            if (item is null)
            {
                throw new EntityNotFoundException($"Item with Id {receiptLotViewModel.ItemId} and unit {receiptLotViewModel.Unit} doesn't exist.");
            }

            Employee? employee = await _employeeRepository.GetEmployeeById(receiptLotViewModel.EmployeeId);
            if (employee is null)
            {
                throw new EntityNotFoundException($"Employee with Id {receiptLotViewModel.EmployeeId} doesn't exist.");
            }

            GoodsReceiptLot goodsReceiptLot = new (receiptLotViewModel.GoodsReceiptLotId, receiptLotViewModel.Quantity,
                employee, item, receiptLotViewModel.Note, goodsReceipt.Id);
            ItemLot itemLot = new (receiptLotViewModel.GoodsReceiptLotId, receiptLotViewModel.Quantity, goodsReceipt.Timestamp, item.Id);

            itemLots.Add(itemLot);
            goodsReceipt.AddLot(goodsReceiptLot);
        }
        goodsReceipt.Confirm(itemLots);

        _goodsReceiptRepository.Add(goodsReceipt);
        return await _goodsReceiptRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}