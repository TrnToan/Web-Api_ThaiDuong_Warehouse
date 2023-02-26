namespace ThaiDuongWarehouse.Domain.AggregateModels.GoodsReceiptAggregate
{
    public class GoodsReceiptLot
    {
        public string GoodsReceiptLotId { get; private set; }
        public string ItemId { get; private set; }
        public string EmployeeId { get; private set; }
        public double Quantity { get; private set; }
        public double? SublotSize { get; private set; }
        public string? PurchaseOrderNumber { get; private set; }
        public DateTime? ProductionDate { get; private set; }
        public DateTime? ExpirationDate { get; private set; }
        public Item Item { get; private set; }
        public Employee Employee { get; private set; }
        public Location? Location { get; private set; }

        public GoodsReceiptLot(string goodsReceiptLotId, string itemId, string employeeId, double quantity, 
            double? sublotSize, string? purchaseOrderNumber, DateTime? productionDate, DateTime? expirationDate, 
            Item item, Employee employee, Location? location)
        {
            GoodsReceiptLotId = goodsReceiptLotId;
            ItemId = itemId;
            EmployeeId = employeeId;
            Quantity = quantity;
            SublotSize = sublotSize;
            PurchaseOrderNumber = purchaseOrderNumber;
            ProductionDate = productionDate;
            ExpirationDate = expirationDate;
            Item = item;
            Employee = employee;
            Location = location;
        }

        public void Update(double quantity, double? sublotSize, string? purchaseOrderNumber, Location? location, 
            DateTime? productionDate, DateTime? expirationDate)
        {
            Quantity = quantity;
            SublotSize = sublotSize;
            PurchaseOrderNumber = purchaseOrderNumber;
            Location = location;
            ProductionDate = productionDate;
            ExpirationDate = expirationDate;
        }
    }
}
