namespace ThaiDuongWarehouse.Api.Applications.Queries.Department;

public class DepartmentViewModel
{
    public string Name { get; private set; }

    public DepartmentViewModel(string name)
    {
        Name = name;
    }
}
