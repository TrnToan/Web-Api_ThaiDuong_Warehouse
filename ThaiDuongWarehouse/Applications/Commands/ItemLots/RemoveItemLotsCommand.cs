using System.Runtime.Serialization;

namespace ThaiDuongWarehouse.Api.Applications.Commands.ItemLots;

[DataContract]
public class RemoveItemLotsCommand : IRequest<bool>
{
    [DataMember]
    public string ItemLotId { get; set; }

    public RemoveItemLotsCommand(string itemLotId)
    {
        ItemLotId = itemLotId;
    }
}
