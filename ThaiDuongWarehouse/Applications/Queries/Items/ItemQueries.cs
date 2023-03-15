using ThaiDuongWarehouse.Domain.AggregateModels.ItemAggregate;

namespace ThaiDuongWarehouse.Api.Applications.Queries.Items;

public class ItemQueries : IItemQueries
{
    private readonly IItemRepository _itemRepository;
    private readonly IMapper _mapper;
    public ItemQueries(IItemRepository itemRepository, IMapper mapper)
    {
        _itemRepository = itemRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ItemViewModel>> GetAllItemsAsync()
    {
        var items = await _itemRepository.GetAllAsync();
        var viewModels = _mapper.Map<IEnumerable<Item>, IEnumerable<ItemViewModel>>(items);

        return viewModels;
    }

    public async Task<ItemViewModel> GetItemByIdAsync(string itemId)
    {
        var item = await _itemRepository.GetItemById(itemId);
        return _mapper.Map<Item?, ItemViewModel>(item);
    }
}
