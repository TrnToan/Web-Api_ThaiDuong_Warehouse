namespace ThaiDuongWarehouse.Api.Applications.Commands.Warehouses;

public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, bool>
{
    private readonly IStorageRepository _storageRepository;
    public CreateLocationCommandHandler(IStorageRepository storageRepository)
    {
        _storageRepository = storageRepository;
    }

    public async Task<bool> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
    {
        var warehouse = await _storageRepository.GetWarehouseById(request.WarehouseId);
        if (warehouse is null)
        {
            throw new EntityNotFoundException($"Warehouse with Id {request.WarehouseId} not found.");
        }

        var location = new Location(request.LocationId, warehouse.Id);
        warehouse.Locations.Add(location);
        _storageRepository.Add(location);

        return await _storageRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);        
    }
}
