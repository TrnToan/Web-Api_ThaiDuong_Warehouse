﻿namespace ThaiDuongWarehouse.Domain.AggregateModels.GoodsReceiptAggregate;
public class GoodsReceiptLot
{
    public string GoodsReceiptLotId { get; private set; }
    public string? LocationId { get; private set; }
    public double Quantity { get; private set; }
    public string Unit { get; private set; }
    public double? SublotSize { get; private set; }
    public string? PurchaseOrderNumber { get; private set; }
    public DateTime? ProductionDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public string? Note { get; private set; }
    public int ItemId { get; private set; }                      // Foreign Key
    public int GoodsReceiptId { get; private set; }

    public Item Item { get; private set; }
    public Employee Employee { get; private set; }

    private GoodsReceiptLot() { }
    public GoodsReceiptLot(string goodsReceiptLotId, string? locationId, double quantity, string unit, double? sublotSize, 
        string? purchaseOrderNumber, DateTime? productionDate, DateTime? expirationDate, int itemId, int goodsReceiptId)
    {
        GoodsReceiptLotId = goodsReceiptLotId;
        LocationId = locationId;
        Quantity = quantity;
        Unit = unit;
        SublotSize = sublotSize;
        PurchaseOrderNumber = purchaseOrderNumber;
        ProductionDate = productionDate;
        ExpirationDate = expirationDate;
        ItemId = itemId;
        GoodsReceiptId = goodsReceiptId;
    }

    public GoodsReceiptLot(string goodsReceiptLotId, double quantity, string unit, string purchaseOrderNumber, Employee employee, 
        Item item, string? note)
    {
        GoodsReceiptLotId = goodsReceiptLotId;
        Quantity = quantity;
        Unit = unit;
        PurchaseOrderNumber = purchaseOrderNumber;
        Employee = employee;
        Item = item;
        Note = note;
    }

    public void Update(double quantity, double? sublotSize, string? purchaseOrderNumber, string locationId, 
        DateTime? productionDate, DateTime? expirationDate)
    {
        Quantity = quantity;
        SublotSize = sublotSize;
        PurchaseOrderNumber = purchaseOrderNumber;
        LocationId = locationId;
        ProductionDate = productionDate;
        ExpirationDate = expirationDate;
    }
}
