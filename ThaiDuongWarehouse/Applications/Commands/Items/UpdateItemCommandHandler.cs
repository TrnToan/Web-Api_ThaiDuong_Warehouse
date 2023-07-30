namespace ThaiDuongWarehouse.Api.Applications.Commands.Items;

public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, bool>
{
    private readonly IItemRepository _itemRepository;
    private readonly IItemClassRepository _itemClassRepository;
    public UpdateItemCommandHandler(IItemRepository itemRepository, IItemClassRepository itemClassRepository)
    {
        _itemRepository = itemRepository;
        _itemClassRepository = itemClassRepository;
    }

    public async Task<bool> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _itemRepository.GetItemById(request.ItemId, request.Unit);

        if (item is null)
        {
            throw new EntityNotFoundException("This Item doesn't exist in current context");
        }

        var itemClass = await _itemClassRepository.GetById(request.ItemClassId);
        if (itemClass is null)
        {
            throw new EntityNotFoundException($"This ItemClass {request.ItemClassId} doesn't exist in current context");
        }

        item.Update(request.ItemName, request.Unit, request.MinimumStockLevel, request.Price, request.ItemClassId, request.PacketSize,
            request.PacketUnit);
        _itemRepository.Update(item);

        return await _itemRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
