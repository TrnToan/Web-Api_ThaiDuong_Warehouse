using ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsIssues;

[DataContract]
public class CreateGoodsIssueLotViewModel
{
    [DataMember]
    public string GoodsIssueLotId { get; private set; }
    [DataMember]
    public string ItemId { get; private set; }
    [DataMember]
    public string Unit { get; private set; }
    [DataMember]
    public List<ItemLotLocationViewModel> ItemLotLocations { get; private set; }
    [DataMember]
    public string? Note { get; private set; }
    [DataMember]
    public string EmployeeId { get; private set; }

    public CreateGoodsIssueLotViewModel(string goodsIssueLotId, string itemId, string unit,
        List<ItemLotLocationViewModel> itemLotLocations, string? note, string employeeId)
    {
        GoodsIssueLotId = goodsIssueLotId;
        ItemId = itemId;
        Unit = unit;
        ItemLotLocations = itemLotLocations;
        Note = note;
        EmployeeId = employeeId;
    }
}
