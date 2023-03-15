using System.Diagnostics.Tracing;
using ThaiDuongWarehouse.Api.Applications.Exceptions;

namespace ThaiDuongWarehouse.Api.Applications.Commands.Employees;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Employee>
{
    private readonly IEmployeeRepository _employeeRepository;
	public UpdateEmployeeCommandHandler(IEmployeeRepository employeeRepository)
	{
		_employeeRepository = employeeRepository;
	}

    public async Task<Employee> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetEmployeeById(request.EmployeeId);

        if (employee is null)
        {
            throw new EntityNotFoundException("Employee doesn't exist in the context");
        }

        employee.Update(request.EmployeeName);
        _employeeRepository.Update(employee);
        await _employeeRepository.UnitOfWork.SaveEntitiesAsync();

        return employee;
    }
}
