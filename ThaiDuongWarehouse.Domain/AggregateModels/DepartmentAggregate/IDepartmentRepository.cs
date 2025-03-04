﻿namespace ThaiDuongWarehouse.Domain.AggregateModels.DepartmentAggregate;

public interface IDepartmentRepository : IRepository<Department>
{
    Task<IEnumerable<Department>> GetAllAsync();
    Department Add(Department department);
}
