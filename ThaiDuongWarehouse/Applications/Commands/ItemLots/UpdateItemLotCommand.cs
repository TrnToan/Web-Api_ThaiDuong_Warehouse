using System.Runtime.Serialization;

namespace ThaiDuongWarehouse.Api.Applications.Commands.ItemLots;

[DataContract]
public class UpdateItemLotCommand : IRequest<bool>
{
    [DataMember]
    public string ItemLotId { get; private set; }
    [DataMember]
    public bool IsIsolated { get; private set; }

    public UpdateItemLotCommand(string itemLotId, bool isIsolated)
    {
        ItemLotId = itemLotId;
        IsIsolated = isIsolated;
    }
}
