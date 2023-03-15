using System.Runtime.Serialization;

namespace ThaiDuongWarehouse.Api.Applications.Commands.Employees;

[DataContract]
public class UpdateEmployeeCommand : IRequest<Employee>
{
    [DataMember]
    public string EmployeeId { get; private set; }
    [DataMember]
    public string EmployeeName { get; private set; }

    public UpdateEmployeeCommand(string employeeId, string employeeName)
    {
        EmployeeId = employeeId;
        EmployeeName = employeeName;
    }
}
