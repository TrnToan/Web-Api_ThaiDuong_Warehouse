namespace ThaiDuongWarehouse.Api.Applications.Queries.Employees;

public class EmployeeViewModel
{
    public string EmployeeId { get; private set; }
    public string EmployeeName { get; private set; }

    public EmployeeViewModel(string employeeId, string employeeName)
    {
        EmployeeId = employeeId;
        EmployeeName = employeeName;
    }
}
