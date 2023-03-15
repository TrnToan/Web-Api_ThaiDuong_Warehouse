namespace ThaiDuongWarehouse.Api.Applications.Commands.Department;

public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, bool>
{
    private readonly IDepartmentRepository _departmentRepository;

    public CreateDepartmentCommandHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<bool> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var department = new Domain.AggregateModels.DepartmentAggregate.Department(request.Name);
        _departmentRepository.Add(department);

        return await _departmentRepository.UnitOfWork.SaveEntitiesAsync();
    }
}
