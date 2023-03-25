using ThaiDuongWarehouse.Api.Applications.Queries.InventoryLogEntries;
using ThaiDuongWarehouse.Domain.AggregateModels.LogAggregate;

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
		CreateMap<LotAdjustment, LotAdjustmentViewModel>()
			.ForMember(dest => dest.Employee, opt => opt.MapFrom(src => src.Employee))
			.ForMember(dest => dest.Unit, opt => opt.MapFrom(src => src.Item.Unit));
        CreateMap<GoodsReceiptLot, GoodsReceiptLotViewModel>();
		CreateMap<GoodsReceipt, GoodsReceiptViewModel>();
		CreateMap<InventoryLogEntry, InventoryLogEntryViewModel>();
	}
}
