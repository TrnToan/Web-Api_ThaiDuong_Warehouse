namespace ThaiDuongWarehouse.Api.Applications.Commands.Items;

public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, bool>
{
    private readonly IItemRepository _itemRepository;
    public CreateItemCommandHandler(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task<bool> Handle(CreateItemCommand request, CancellationToken cancellationToken)
    {
        var isExistedItem = await _itemRepository.GetItemById(request.ItemId, request.Unit);
        if (isExistedItem is not null) 
        {
            throw new DuplicateRecordException($"Item with Id {request.ItemId} and Unit {request.Unit} already existed in the database.");
        }

        var item = new Item(request.ItemId, request.ItemClassId, request.Unit, request.ItemName, request.MinimumStockLevel, 
            request.Price, request.PacketSize, request.PacketUnit);

        _itemRepository.Add(item);
        return await _itemRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
