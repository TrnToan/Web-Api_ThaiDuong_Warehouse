namespace ThaiDuongWarehouse.Api.Applications.Mapping;

public class ModelToViewModelProfile : Profile
{
	public ModelToViewModelProfile()
	{
		CreateMap<Employee, EmployeeViewModel>();
		CreateMap<Item, ItemViewModel>();
		CreateMap<Department, DepartmentViewModel>();
		CreateMap<Warehouse, WarehouseViewModel>();
		CreateMap<Location, LocationViewModel>();
		CreateMap<ItemLot, ItemLotViewModel>();
		CreateMap<GoodsReceiptLot, GoodsReceiptLotViewModel>();
		CreateMap<GoodsReceipt, GoodsReceiptViewModel>();
	}
}
