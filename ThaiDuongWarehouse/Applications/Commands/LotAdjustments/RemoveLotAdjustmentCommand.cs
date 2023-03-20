using System.Runtime.Serialization;

namespace ThaiDuongWarehouse.Api.Applications.Commands.LotAdjustments;

[DataContract]
public class RemoveLotAdjustmentCommand : IRequest<bool>
{
    [DataMember]
    public string LotId { get; private set; }

    public RemoveLotAdjustmentCommand(string lotId)
    {
        LotId = lotId;
    }
}
