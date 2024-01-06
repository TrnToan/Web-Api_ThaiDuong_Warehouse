using System.Runtime.Serialization;

namespace ThaiDuongWarehouse.Api.Applications.Commands.ItemLots;

[DataContract]
public class IsolateItemLotCommand : IRequest<bool>
{
    [DataMember]
    public string ItemLotId { get; private set; }
    [DataMember]
    public List<IsolatedItemSublotViewModel> IsolatedItemSublots { get; private set; }

    public IsolateItemLotCommand(string itemLotId, List<IsolatedItemSublotViewModel> isolatedItemSublots)
    {
        ItemLotId = itemLotId;
        IsolatedItemSublots = isolatedItemSublots;
    }
}
