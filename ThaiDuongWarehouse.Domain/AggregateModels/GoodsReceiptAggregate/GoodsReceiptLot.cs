namespace ThaiDuongWarehouse.Domain.AggregateModels.GoodsReceiptAggregate;
public class GoodsReceiptLot 
{
    public int Id { get; set; }         
    public string GoodsReceiptLotId { get; private set; }
    public double Quantity { get; private set; }
    public DateTime? ProductionDate { get; private set; }
    public DateTime? ExpirationDate { get; private set; }
    public string? Note { get; private set; }
    // Foreign Key
    public int ItemId { get; private set; }                      
    public int GoodsReceiptId { get; private set; }
    public Item Item { get; private set; }
    public Employee Employee { get; private set; }
    public List<GoodsReceiptSublot> Sublots { get; private set; }

    private GoodsReceiptLot() { }
    public GoodsReceiptLot(string goodsReceiptLotId, double quantity, DateTime? productionDate, DateTime? expirationDate, 
        int itemId, int goodsReceiptId)
    {
        GoodsReceiptLotId = goodsReceiptLotId;
        Quantity = quantity;
        ProductionDate = productionDate;
        ExpirationDate = expirationDate;
        ItemId = itemId;
        GoodsReceiptId = goodsReceiptId;
    }
    public GoodsReceiptLot(string goodsReceiptLotId, double quantity, Employee employee, Item item, string? note, int goodsReceiptId)
    {
        GoodsReceiptLotId = goodsReceiptLotId;
        Quantity = quantity;
        Employee = employee;
        Item = item;
        Note = note;
        GoodsReceiptId = goodsReceiptId;
    }

    public void Update(string newLotId, double quantity, DateTime? productionDate, DateTime? expirationDate, string? note,
        List<GoodsReceiptSublot> receiptSublots)
    {
        GoodsReceiptLotId = newLotId;
        Quantity = quantity;

        if (productionDate != null) 
            ProductionDate = productionDate;

        if (expirationDate != null) 
            ExpirationDate = expirationDate;

        if (note != null)
            Note = note;

        if (receiptSublots != null)
            Sublots = receiptSublots;
    }

    public void UpdateConfirmedLot(double quantity, DateTime? productionDate, DateTime? expirationDate)
    {
        Quantity = quantity;
        ProductionDate = productionDate;
        ExpirationDate = expirationDate;
    }

    public void SetQuantity(double quantity)
    {
        Quantity = quantity;
    }
}
