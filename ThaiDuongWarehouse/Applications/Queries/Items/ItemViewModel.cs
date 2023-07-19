namespace ThaiDuongWarehouse.Api.Applications.Queries.Items;

public class ItemViewModel
{
    public string ItemId { get; private set; }
    public string ItemClassId { get; private set; }             
    public string Unit{ get; private set; }                
    public string ItemName { get; private set; }
    public double MinimumStockLevel { get; private set; }
    public decimal? Price { get; private set; }

    public ItemViewModel(string itemId, string itemClassId, string unit, string itemName,
        double minimumStockLevel, decimal price)
    {
        ItemId = itemId;
        ItemClassId = itemClassId;
        Unit = unit;
        ItemName = itemName;
        MinimumStockLevel = minimumStockLevel;
        Price = price;
    }
}
