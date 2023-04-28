using System.Runtime.Serialization;

namespace ThaiDuongWarehouse.Api.Applications.Commands.ItemLots;

[DataContract]
public class RemoveItemLotsCommand : IRequest<bool>
{
    [DataMember]
    public List<string> ItemLotIds { get; set; }

    public RemoveItemLotsCommand(List<string> itemLotIds)
    {
        ItemLotIds = itemLotIds;
    }
}
