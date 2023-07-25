
namespace ThaiDuongWarehouse.Api.Applications.Commands.LotAdjustments;

public class CreateLotAdjustmentCommandHandler : IRequestHandler<CreateLotAdjustmentCommand, bool>
{
    private readonly ILotAdjustmentRepository _lotAdjustmentRepository;
    private readonly IItemLotRepository _itemLotRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IItemRepository _itemRepository;
    public CreateLotAdjustmentCommandHandler(ILotAdjustmentRepository lotAdjustmentRepository, IItemLotRepository itemLotRepository, 
        IEmployeeRepository employeeRepository, IItemRepository itemRepository)
    {
        _lotAdjustmentRepository = lotAdjustmentRepository;
        _itemLotRepository = itemLotRepository;
        _employeeRepository = employeeRepository;
        _itemRepository = itemRepository;
    }

    public async Task<bool> Handle(CreateLotAdjustmentCommand request, CancellationToken cancellationToken)
    {
        var lotAdjustment = await _lotAdjustmentRepository.GetAdjustmentByLotId(request.LotId);
        if (lotAdjustment is not null)
        {
            throw new DuplicateRecordException($"Previous lotAdjustment with LotId {request.LotId} hasn't been confirmed yet.");
        }

        Item? item = await _itemRepository.GetItemById(request.ItemId, request.Unit);
        if (item is null)
        {
            throw new EntityNotFoundException("Item does not exist.");
        }

        var itemLot = await _itemLotRepository.GetLotByLotId(request.LotId);
        if (itemLot is null)
        {
            throw new EntityNotFoundException($"Itemlot {request.LotId} does not exist");
        }

        var employee = await _employeeRepository.GetEmployeeByName(request.EmployeeName);
        if (employee is null)
        {
            throw new EntityNotFoundException($"Employee {request.EmployeeName} does not exist");
        }
        
        var newLotAdjustment = new LotAdjustment(request.LotId, itemLot.Quantity, request.Note, DateTime.UtcNow.AddHours(7), 
            item.Id, employee.Id);

        double totalQuantity = request.SublotAdjustments.Sum(sub => sub.NewQuantityPerLocation);
        if (totalQuantity != request.AfterQuantity)
        {
            throw new InvalidItemLotException($"Total itemlot quantity {request.AfterQuantity} is not equal to sum of " +
                $"quantityPerLocation {totalQuantity}");
        }

        newLotAdjustment.Update(request.AfterQuantity);

        foreach (var sublot in request.SublotAdjustments)
        {
            var existedSublot = itemLot.ItemLotLocations.Find(lot => lot.Location.LocationId == sublot.LocationId);
            if (existedSublot is null)
            {
                throw new EntityNotFoundException($"Itemlot with locationId {sublot.LocationId} not found.");
            }
            newLotAdjustment.AddSublot(sublot.LocationId, existedSublot.QuantityPerLocation, sublot.NewQuantityPerLocation);
        }

        _lotAdjustmentRepository.Add(newLotAdjustment);
        return await _lotAdjustmentRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
