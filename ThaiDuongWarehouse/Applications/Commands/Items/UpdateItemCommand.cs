namespace ThaiDuongWarehouse.Api.Applications.Commands.Items;

[DataContract]
public class UpdateItemCommand : IRequest<bool>
{
    [DataMember]
    public string ItemId { get; private set; }
    [DataMember]
    public double MinimumStockLevel { get; private set; }
    [DataMember]
    public decimal Price { get; private set; }
    [DataMember]
    public string Unit { get; private set; }
    public UpdateItemCommand(string itemId, string unit, double minimumStockLevel, decimal price)
    {
        ItemId = itemId;
        Unit = unit;
        MinimumStockLevel = minimumStockLevel;
        Price = price;
    }
}
