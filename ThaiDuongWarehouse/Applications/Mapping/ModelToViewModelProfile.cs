namespace ThaiDuongWarehouse.Api.Applications.Mapping;

public class ModelToViewModelProfile : Profile
{
	public ModelToViewModelProfile()
	{
		CreateMap<Employee, EmployeeViewModel>();
		CreateMap<Item, ItemViewModel>();
		CreateMap<Department, DepartmentViewModel>();
	}
}
