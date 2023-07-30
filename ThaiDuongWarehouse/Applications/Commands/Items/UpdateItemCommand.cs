namespace ThaiDuongWarehouse.Api.Applications.Commands.Items;

[DataContract]
public class UpdateItemCommand : IRequest<bool>
{
    [DataMember]
    public string ItemId { get; private set; }
    [DataMember]
    public string ItemName { get; private set; }
    [DataMember]
    public double MinimumStockLevel { get; private set; }
    [DataMember]
    public decimal Price { get; private set; }
    [DataMember]
    public string Unit { get; private set; }
    [DataMember]
    public string ItemClassId { get; private set; }
    [DataMember]
    public double? PacketSize { get; private set; }
    [DataMember]
    public string? PacketUnit { get; private set; }
    public UpdateItemCommand(string itemId, string itemName, string unit, double minimumStockLevel, decimal price, 
        string itemClassId, double? packetSize, string? packetUnit)
    {
        ItemId = itemId;
        ItemName = itemName;
        Unit = unit;
        MinimumStockLevel = minimumStockLevel;
        Price = price;
        ItemClassId = itemClassId;
        PacketSize = packetSize;
        PacketUnit = packetUnit;
    }
}
