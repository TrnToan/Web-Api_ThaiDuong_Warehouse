namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

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
        var goodsReceiptEmployee = await _employeeRepository.GetEmployeeById(request.EmployeeId); 

        List<GoodsReceiptLot> goodsReceiptLots = new();
        
        foreach (var receiptLotViewModel in request.GoodsReceiptLots)
        {
            var item = await _itemRepository.GetItemById(receiptLotViewModel.ItemId, receiptLotViewModel.Unit);
            if (item is null)
            {
                throw new EntityNotFoundException($"{item}");
            }
            
            var employee = await _employeeRepository.GetEmployeeById(receiptLotViewModel.EmployeeId);
            if (employee is null)
            {
                throw new EntityNotFoundException($"{employee}");
            }

            var goodsReceiptLot = new GoodsReceiptLot(receiptLotViewModel.GoodsReceiptLotId, receiptLotViewModel.Quantity, 
                receiptLotViewModel.Unit, receiptLotViewModel.PurchaseOrderNumber, employee, item, receiptLotViewModel.Note);

            goodsReceiptLots.Add(goodsReceiptLot);
            //goodsReceipt.AddLot(goodsReceiptLot);
        }
        var goodsReceipt = new GoodsReceipt(request.GoodsReceiptId, request.Supplier, request.Timestamp, false, 
            goodsReceiptEmployee, goodsReceiptLots);

        _goodsReceiptRepository.Add(goodsReceipt);

        if (goodsReceipt.Lots.Count < 2)
            throw new Exception("Missing goodsReceiptLots on trying to add 2 lots");

        return await _goodsReceiptRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}