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
        var item = new Item(request.ItemId, request.ItemClassId, request.Unit,
            request.ItemName, request.MinimumStockLevel, request.Price);

        _itemRepository.Add(item);
        return await _itemRepository.UnitOfWork.SaveEntitiesAsync();
    }
}
