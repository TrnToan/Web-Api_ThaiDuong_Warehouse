using ThaiDuongWarehouse.Api.Applications.Queries.FinishedProductInventories;
using ThaiDuongWarehouse.Domain.AggregateModels;
using ThaiDuongWarehouse.Domain.AggregateModels.ProductInventoryAggregate;

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
		CreateMap<ItemLot, ItemLotLogEntryViewModel>();
		CreateMap<ItemLotLocation, ItemSublotViewModel>()
			.ConvertUsing<ItemLotLocationToItemSublotViewModelConverter>();
		CreateMap<LotAdjustment, LotAdjustmentViewModel>();
		CreateMap<SublotAdjustment, SublotAdjustmentViewModel>();
        CreateMap<GoodsReceiptLot, GoodsReceiptLotViewModel>();
		CreateMap<GoodsReceiptSublot, GoodsReceiptSublotViewModel>();
		CreateMap<GoodsReceipt, GoodsReceiptViewModel>();
		CreateMap<GoodsIssue, GoodsIssueViewModel>();
		CreateMap<GoodsIssueEntry, GoodsIssueEntryViewModel>();
		CreateMap<GoodsIssueLot, GoodsIssueLotViewModel>();
		CreateMap<GoodsIssueSublot, GoodsIssueSublotViewModel>();
		CreateMap<InventoryLogEntry, InventoryLogEntryViewModel>();
		CreateMap<GoodsReceipt, GoodsReceiptHistoryViewModel>();
		CreateMap<GoodsReceiptLot, GoodsReceiptLotHistoryViewModel>();
		CreateMap<GoodsIssue, GoodsIssueHistoryViewModel>();
		CreateMap<GoodsIssueEntry, GoodsIssueEntryHistoryViewModel>();
		CreateMap<GoodsIssueLot, GoodsIssueLotHistoryViewModel>();
		CreateMap<FinishedProductReceipt, FinishedProductReceiptViewModel>();
		CreateMap<FinishedProductReceiptEntry, FinishedProductReceiptEntryViewModel>();
		CreateMap<FinishedProductIssue, FinishedProductIssueViewModel>();
		CreateMap<FinishedProductIssueEntry, FinishedProductIssueEntryViewModel>();
		CreateMap<FinishedProductInventory, FinishedProductInventoryViewModel>();
	}
}
