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
    public string Unit{ get; private set; }
    [DataMember]
    public double? PacketSize { get; private set; }
    [DataMember]
    public string? PacketUnit { get; private set; }

    public CreateItemCommand(string itemId, string itemClassId, string unit, string itemName, double minimumStockLevel, 
        decimal? price, double? packetSize, string? packetUnit)
    {
        ItemId = itemId;
        ItemClassId = itemClassId;
        Unit = unit;
        ItemName = itemName;
        MinimumStockLevel = minimumStockLevel;
        Price = price;
        PacketSize = packetSize;
        PacketUnit = packetUnit;
    }
}
