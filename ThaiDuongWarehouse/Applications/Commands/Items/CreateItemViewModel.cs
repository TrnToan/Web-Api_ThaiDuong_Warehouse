namespace ThaiDuongWarehouse.Api.Applications.Commands.Items;

public class CreateItemViewModel
{
    public string ItemId { get; private set; }
    public string ItemName { get; private set;}
    public string ItemClassId { get; private set; }
    public string Unit { get; private set; }

    public CreateItemViewModel(string itemId, string itemName, string itemClassId, string unit)
    {
        ItemId = itemId;
        ItemName = itemName;
        ItemClassId = itemClassId;
        Unit = unit;
    }
}
