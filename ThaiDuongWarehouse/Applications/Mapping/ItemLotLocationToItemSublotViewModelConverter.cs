using ThaiDuongWarehouse.Domain.AggregateModels;

namespace ThaiDuongWarehouse.Api.Applications.Mapping;

public class ItemLotLocationToItemSublotViewModelConverter : ITypeConverter<ItemLotLocation, ItemSublotViewModel>
{
    public ItemSublotViewModel Convert(ItemLotLocation src, ItemSublotViewModel dest, ResolutionContext context)
    {
        return new ItemSublotViewModel(src.Location.LocationId, src.QuantityPerLocation);
    }
}
