namespace ThaiDuongWarehouse.Api.Applications.Commands.Employees;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand, bool>
{
    private readonly IEmployeeRepository _employeeRepository;
    public CreateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<bool> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = new Employee(request.EmployeeId, request.EmployeeName);

        _employeeRepository.Add(employee);
        return await _employeeRepository.UnitOfWork.SaveEntitiesAsync();
    }
}
