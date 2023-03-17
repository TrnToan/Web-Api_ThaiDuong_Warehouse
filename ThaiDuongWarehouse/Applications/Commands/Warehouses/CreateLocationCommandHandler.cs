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
        var location = new Location(request.LocationId);
        _storageRepository.Add(location);
        return await _storageRepository.UnitOfWork.SaveEntitiesAsync();        
    }
}
