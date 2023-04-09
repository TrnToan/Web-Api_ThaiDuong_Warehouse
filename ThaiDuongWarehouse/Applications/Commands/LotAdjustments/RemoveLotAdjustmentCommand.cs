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

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public RemoveLotAdjustmentCommand()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {    }
}
