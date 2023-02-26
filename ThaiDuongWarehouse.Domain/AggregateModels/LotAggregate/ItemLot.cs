using ThaiDuongWarehouse.Domain.AggregateModels.LogAggregate;

namespace ThaiDuongWarehouse.Domain.AggregateModels.LotAggregate
{
    public class ItemLot : Entity, IAggregateRoot
    {
        public string LotId { get; private set; }
        public double Quantity { get; private set; }
        public double? SublotSize { get; private set; }
        public string PurchaseOrderNumber { get; private set; }
        public DateTime ProductionDate { get; private set; }
        public DateTime ExpirationDate { get; private set; }
        public Location Location { get; private set; }
        public List<InventoryLogEntry> InventoryLogEntries { get; private set; }
        public Item Item { get; private set; }

        public ItemLot(string lotId, double quantity, double? sublotSize, string purchaseOrderNumber, 
            DateTime productionDate, DateTime expirationDate, Location location, Item item)
        {
            LotId = lotId;
            Quantity = quantity;
            SublotSize = sublotSize;
            PurchaseOrderNumber = purchaseOrderNumber;
            ProductionDate = productionDate;
            ExpirationDate = expirationDate;
            Location = location;
            Item = item;
            InventoryLogEntries = new List<InventoryLogEntry>();
        }
    }
}
