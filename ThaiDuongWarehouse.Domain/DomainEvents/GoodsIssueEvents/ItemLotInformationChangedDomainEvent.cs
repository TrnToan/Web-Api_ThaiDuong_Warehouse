using ThaiDuongWarehouse.Domain.AggregateModels.GoodsIssueAggregate;

namespace ThaiDuongWarehouse.Domain.DomainEvents.GoodsIssueEvents;
public class ItemLotInformationChangedDomainEvent : INotification
{
    public ItemLot ItemLot { get; private set; }
    public GoodsIssueLot GoodsIssueLot { get; private set; }

    public ItemLotInformationChangedDomainEvent(ItemLot itemLot, GoodsIssueLot goodsIssueLot)
    {
        ItemLot = itemLot;
        GoodsIssueLot = goodsIssueLot;
    }
}
