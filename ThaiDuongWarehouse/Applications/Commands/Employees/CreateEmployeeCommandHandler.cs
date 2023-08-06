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
        var employee = await _employeeRepository.GetEmployeeById(request.EmployeeId);
        if (employee is not null)
        {
            throw new DuplicateRecordException(nameof(Employee), employee.EmployeeId);
        }

        var newEmployee = new Employee(request.EmployeeId, request.EmployeeName);

        _employeeRepository.Add(newEmployee);
        return await _employeeRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}
