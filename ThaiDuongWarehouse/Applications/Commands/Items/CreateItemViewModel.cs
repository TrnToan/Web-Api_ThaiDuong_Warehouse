namespace ThaiDuongWarehouse.Api.Applications.Commands.Items;

public class CreateItemViewModel
{
    public string ItemId { get; private set; }
    public string ItemName { get; private set; }
    public double MinimumStockLevel { get; private set; }
    public decimal? Price { get; private set; }
    public string ItemClassId { get; private set; }
    public string Unit { get; private set; }

    public CreateItemViewModel(string itemId, string itemName, double minimumStockLevel, decimal? price, 
        string itemClassId, string unit)
    {
        ItemId = itemId;
        ItemName = itemName;
        MinimumStockLevel = minimumStockLevel;
        Price = price;
        ItemClassId = itemClassId;
        Unit = unit;
    }
}
