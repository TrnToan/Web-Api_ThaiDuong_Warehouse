using System.Runtime.Serialization;
using Unit = ThaiDuongWarehouse.Domain.AggregateModels.ItemAggregate.Unit;

namespace ThaiDuongWarehouse.Api.Applications.Commands.Items;

[DataContract]
public class UpdateItemCommand : IRequest<Item>
{
    [DataMember]
    public string ItemId { get; private set; }
    [DataMember]
    public double MinimumStockLevel { get; private set; }
    [DataMember]
    public decimal Price { get; private set; }
    [DataMember]
    public Unit Unit { get; private set; }
    public UpdateItemCommand(string itemId, Unit unit, double minimumStockLevel, decimal price)
    {
        ItemId = itemId;
        Unit = unit;
        MinimumStockLevel = minimumStockLevel;
        Price = price;
    }
}
