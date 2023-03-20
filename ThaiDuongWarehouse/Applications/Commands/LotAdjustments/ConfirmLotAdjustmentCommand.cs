using System.Runtime.Serialization;

namespace ThaiDuongWarehouse.Api.Applications.Commands.LotAdjustments;

[DataContract]
public class ConfirmLotAdjustmentCommand : IRequest<bool>
{
    [DataMember]
    public string LotId { get; private set; }
    [DataMember]
    public string ItemId { get; private set; }

    public ConfirmLotAdjustmentCommand(string lotId, string itemId)
    {
        LotId = lotId;
        ItemId = itemId;
    }
}
