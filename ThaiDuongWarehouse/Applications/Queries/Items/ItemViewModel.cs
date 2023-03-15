namespace ThaiDuongWarehouse.Api.Applications.Queries.Items;

public class ItemViewModel
{
    public string ItemId { get; private set; }
    public string ItemClassId { get; private set; }             
    public string UnitName { get; private set; }                
    public string ItemName { get; private set; }
    public double MinimumStockLevel { get; private set; }
    public double Price { get; private set; }

    public ItemViewModel(string itemId, string itemClassId, string unitName, string itemName,
        double minimumStockLevel, double price)
    {
        ItemId = itemId;
        ItemClassId = itemClassId;
        UnitName = unitName;
        ItemName = itemName;
        MinimumStockLevel = minimumStockLevel;
        Price = price;
    }
}
