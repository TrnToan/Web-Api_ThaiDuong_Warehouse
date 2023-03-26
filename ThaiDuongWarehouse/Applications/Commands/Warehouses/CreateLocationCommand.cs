using System.Runtime.Serialization;
using ThaiDuongWarehouse.Domain.AggregateModels.LotAggregate;

namespace ThaiDuongWarehouse.Api.Applications.Commands.Warehouses;

[DataContract]
public class CreateLocationCommand : IRequest<bool>
{
    [DataMember]
    public string WarehouseId { get; private set; }
    [DataMember]
    public string LocationId { get; private set; }

    public CreateLocationCommand(string locationId, string warehouseId)
    {
        LocationId = locationId;
        WarehouseId = warehouseId;
    }
}
