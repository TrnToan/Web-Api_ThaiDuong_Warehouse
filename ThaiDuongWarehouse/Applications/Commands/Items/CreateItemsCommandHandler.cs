namespace ThaiDuongWarehouse.Api.Applications.Commands.Items;

public class CreateItemsCommandHandler : IRequestHandler<CreateItemsCommand, bool>
{
    private readonly IItemRepository _itemRepository;

    public CreateItemsCommandHandler(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task<bool> Handle(CreateItemsCommand request, CancellationToken cancellationToken)
    {
        foreach (var item in request.Items)
        {
            var isExistedItem = await _itemRepository.GetItemById(item.ItemId, item.Unit);
            if (isExistedItem is not null)
            {
                continue;
            }

            var newItem = new Item(item.ItemId, item.ItemName, item.Unit, item.ItemClassId);
            _itemRepository.Add(newItem);
        }
        return await _itemRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
