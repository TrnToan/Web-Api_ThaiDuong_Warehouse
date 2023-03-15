using System.Runtime.Serialization;
using Unit = ThaiDuongWarehouse.Domain.AggregateModels.ItemAggregate.Unit;

namespace ThaiDuongWarehouse.Api.Applications.Commands.Items;

[DataContract]
public class CreateItemCommand : IRequest<bool>
{
    [DataMember]
    public string ItemId { get; private set; }
    [DataMember]
    public string ItemName { get; private set; }
    [DataMember]
    public double MinimumStockLevel { get; private set; }
    [DataMember]
    public decimal? Price { get; private set; }
    [DataMember]
    public string ItemClassId { get; private set; }
    [DataMember]
    public string UnitName { get; private set; }

    public CreateItemCommand(string itemId, string itemClassId, string unitName, string itemName, 
        double minimumStockLevel, decimal? price)
    {
        ItemId = itemId;
        ItemClassId = itemClassId;
        UnitName = unitName;
        ItemName = itemName;
        MinimumStockLevel = minimumStockLevel;
        Price = price;
    }
}
