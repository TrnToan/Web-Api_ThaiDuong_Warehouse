namespace ThaiDuongWarehouse.Api.Applications.Queries.InventoryLogEntries;

public class ItemLotLogEntryViewModel
{
    public string LotId { get; private set; }
    public double Quantity { get; private set; }
    public double? NumOfPackets { get; private set; }

    public ItemLotLogEntryViewModel(string lotId, double quantity, double? numOfPackets)
    {
        LotId = lotId;
        Quantity = quantity;
        NumOfPackets = numOfPackets;
    }
}
