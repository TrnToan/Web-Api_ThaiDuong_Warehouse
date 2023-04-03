using ThaiDuongWarehouse.Domain.AggregateModels.GoodsIssueAggregate;

namespace ThaiDuongWarehouse.Domain.DomainEvents;
public class GoodsIssueLotLogEntryDomainEvent : INotification
{
    public DateTime Timestamp { get; private set; }
    public GoodsIssueLot GoodsIssueLot { get; private set; }
	public GoodsIssueLotLogEntryDomainEvent(DateTime timestamp, GoodsIssueLot goodsIssueLot)
    {
        Timestamp = timestamp;
        GoodsIssueLot = goodsIssueLot;
    }
}
