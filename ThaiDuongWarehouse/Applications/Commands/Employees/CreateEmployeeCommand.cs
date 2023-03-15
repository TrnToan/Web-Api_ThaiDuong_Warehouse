using System.Runtime.Serialization;

namespace ThaiDuongWarehouse.Api.Applications.Commands.Employees;

[DataContract]
public class CreateEmployeeCommand : IRequest<bool>
{
    [DataMember]
    public string EmployeeId { get; private set; }
    [DataMember] 
    public string EmployeeName { get;}

    public CreateEmployeeCommand(string employeeId, string employeeName)
    {
        EmployeeId = employeeId;
        EmployeeName = employeeName;
    }
}
