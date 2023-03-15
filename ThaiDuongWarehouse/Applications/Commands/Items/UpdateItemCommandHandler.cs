using ThaiDuongWarehouse.Api.Applications.Exceptions;
using Unit = ThaiDuongWarehouse.Domain.AggregateModels.ItemAggregate.Unit;
namespace ThaiDuongWarehouse.Api.Applications.Commands.Items;

public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, Item>
{
    private readonly IItemRepository _itemRepository;
    public UpdateItemCommandHandler(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task<Item> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _itemRepository.GetItemById(request.ItemId);

        if(item is null)
        {
            throw new EntityNotFoundException("This Item doesn't exist in current context");
        }

        item.Update(request.Unit, request.MinimumStockLevel, request.Price);
        _itemRepository.Update(item);
        await _itemRepository.UnitOfWork.SaveEntitiesAsync();

        return item;
    }
}
