using ThaiDuongWarehouse.Domain.AggregateModels.ItemLotLocationAggregate;

namespace ThaiDuongWarehouse.Domain.DomainEvents.IsolatedItemLotEvents;
public class BackToItemLotDomainEvent : INotification
{
    public ItemLot ItemLot { get; private set; }
    public List<ItemLotLocation> UnisolatedItemLotLocations { get; private set; }
    public double UnisolatedQuantity { get; private set; }

    public BackToItemLotDomainEvent(ItemLot itemLot, List<ItemLotLocation> unisolatedItemLotLocations, double unisolatedQuantity)
    {
        ItemLot = itemLot;
        UnisolatedItemLotLocations = unisolatedItemLotLocations;
        UnisolatedQuantity = unisolatedQuantity;
    }
}
