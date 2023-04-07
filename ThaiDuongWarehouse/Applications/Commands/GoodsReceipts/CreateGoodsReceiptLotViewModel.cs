using System.Runtime.Serialization;

namespace ThaiDuongWarehouse.Api.Applications.Commands.GoodsReceipts;

[DataContract]
public class CreateGoodsReceiptLotViewModel
{
    [DataMember]
    public string GoodsReceiptLotId { get; private set; }
    [DataMember]
    public double Quantity { get; private set; }
    [DataMember]
    public string Unit { get; private set; }
    [DataMember]
    public string ItemId { get; private set; }
    [DataMember]
    public string EmployeeId { get; private set; }
    [DataMember]
    public string? Note { get; private set; }
    public CreateGoodsReceiptLotViewModel(string goodsReceiptLotId, double quantity, string unit,
        string itemId, string employeeId, string? note)
    {
        GoodsReceiptLotId = goodsReceiptLotId;
        Quantity = quantity;
        Unit = unit;
        ItemId = itemId;
        EmployeeId = employeeId;
        Note = note;
    }
}
