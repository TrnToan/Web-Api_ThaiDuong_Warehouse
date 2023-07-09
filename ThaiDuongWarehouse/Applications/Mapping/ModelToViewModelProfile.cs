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
		CreateMap<LotAdjustment, LotAdjustmentViewModel>();
        CreateMap<GoodsReceiptLot, GoodsReceiptLotViewModel>();
		CreateMap<GoodsReceipt, GoodsReceiptViewModel>();
		CreateMap<GoodsIssue, GoodsIssueViewModel>();
		CreateMap<GoodsIssueEntry, GoodsIssueEntryViewModel>();
		CreateMap<GoodsIssueLot, GoodsIssueLotViewModel>();
		CreateMap<InventoryLogEntry, InventoryLogEntryViewModel>();
		CreateMap<GoodsReceipt, GoodsReceiptsHistoryViewModel>();
		CreateMap<GoodsReceiptLot, GoodsReceiptLotsHistoryViewModel>();
		CreateMap<GoodsIssue, GoodsIssuesHistoryViewModel>();
		CreateMap<GoodsIssueEntry, GoodsIssueEntryHistoryViewModel>();
		CreateMap<GoodsIssueLot, GoodsIssueLotsHistoryViewModel>();
		CreateMap<FinishedProductReceipt, FinishedProductReceiptViewModel>();
		CreateMap<FinishedProductReceiptEntry, FinishedProductReceiptEntryViewModel>();
	}
}
