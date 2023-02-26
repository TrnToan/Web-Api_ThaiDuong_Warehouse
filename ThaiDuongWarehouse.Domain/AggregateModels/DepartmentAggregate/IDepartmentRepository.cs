namespace ThaiDuongWarehouse.Domain.AggregateModels.DepartmentAggregate
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> GetAll();
        void Add(Department department);
    }
}
