namespace ThaiDuongWarehouse.Api.Applications.Commands.Items;

public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, ItemViewModel>
{
    private readonly IItemRepository _itemRepository;
    private readonly IMapper _mapper;
    public UpdateItemCommandHandler(IItemRepository itemRepository, IMapper mapper)
    {
        _itemRepository = itemRepository;
        _mapper = mapper;
    }

    public async Task<ItemViewModel> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _itemRepository.GetItemById(request.ItemId, request.Unit);

        if(item is null)
        {
            throw new EntityNotFoundException("This Item doesn't exist in current context");
        }

        item.Update(request.Unit, request.MinimumStockLevel, request.Price);
        _itemRepository.Update(item);
        await _itemRepository.UnitOfWork.SaveEntitiesAsync();

        return _mapper.Map<ItemViewModel>(item);
    }
}
