namespace ThaiDuongWarehouse.Domain.AggregateModels.DepartmentAggregate;

public class Department : Entity, IAggregateRoot
{
    public string Name { get; private set; }

    public Department(string name)
    {
        Name = name;
    }
}
