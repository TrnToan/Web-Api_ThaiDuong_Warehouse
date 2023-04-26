namespace ThaiDuongWarehouse.Api.Applications.Commands.Items;

public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, bool>
{
    private readonly IItemRepository _itemRepository;
    public UpdateItemCommandHandler(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task<bool> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _itemRepository.GetItemById(request.ItemId, request.Unit);

        if (item is null)
        {
            throw new EntityNotFoundException("This Item doesn't exist in current context");
        }

        item.Update(request.Unit, request.MinimumStockLevel, request.Price);
        _itemRepository.Update(item);

        return await _itemRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
