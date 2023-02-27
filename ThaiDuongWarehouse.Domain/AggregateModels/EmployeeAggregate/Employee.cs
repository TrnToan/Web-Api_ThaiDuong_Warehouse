namespace ThaiDuongWarehouse.Domain.AggregateModels.EmployeeAggregate;
public class Employee : Entity, IAggregateRoot
{
    public string EmployeeId { get; private set; }
    public string EmployeeName { get; private set;}

    public Employee(string employeeId, string employeeName)
    {
        EmployeeId = employeeId;
        EmployeeName = employeeName;
    }
}
