using System.Runtime.Serialization;

namespace ThaiDuongWarehouse.Api.Applications.Commands.Department;

[DataContract]
public class CreateDepartmentCommand : IRequest<bool>
{
    [DataMember]
    public string Name { get; private set; }

    public CreateDepartmentCommand(string name)
    {
        Name = name;
    }
}
