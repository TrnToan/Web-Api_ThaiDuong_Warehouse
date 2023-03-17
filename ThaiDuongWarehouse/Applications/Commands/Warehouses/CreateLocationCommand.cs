using System.Runtime.Serialization;
using ThaiDuongWarehouse.Domain.AggregateModels.LotAggregate;

namespace ThaiDuongWarehouse.Api.Applications.Commands.Warehouses;

[DataContract]
public class CreateLocationCommand : IRequest<bool>
{
    [DataMember]
    public string LocationId { get; private set; }

    public CreateLocationCommand(string locationId)
    {
        LocationId = locationId;
    }
}
