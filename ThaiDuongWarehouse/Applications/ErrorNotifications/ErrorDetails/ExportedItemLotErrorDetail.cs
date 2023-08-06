namespace ThaiDuongWarehouse.Api.Applications.ErrorNotifications.ErrorDetails;

public class ExportedItemLotErrorDetail
{
    public string ItemLotId { get; set; }

    public ExportedItemLotErrorDetail(string itemLotId)
    {
        ItemLotId = itemLotId;
    }
}
