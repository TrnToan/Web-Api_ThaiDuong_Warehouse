using System.Runtime.Serialization;

namespace ThaiDuongWarehouse.Api.Applications.Commands.Items;

[DataContract]
public class CreateItemsCommand : IRequest<bool>
{
    [DataMember]
    public List<CreateItemViewModel> Items { get; set; }

    public CreateItemsCommand(List<CreateItemViewModel> items)
    {
        Items = items;
    }
}
