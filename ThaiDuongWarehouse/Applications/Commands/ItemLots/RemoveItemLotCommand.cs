namespace ThaiDuongWarehouse.Api.Applications.Commands.ItemLots;

[DataContract]
public class RemoveItemLotCommand : IRequest<bool>
{
    [DataMember]
    public string ItemLotId { get; set; }

    public RemoveItemLotCommand(string itemLotId)
    {
        ItemLotId = itemLotId;
    }
}
