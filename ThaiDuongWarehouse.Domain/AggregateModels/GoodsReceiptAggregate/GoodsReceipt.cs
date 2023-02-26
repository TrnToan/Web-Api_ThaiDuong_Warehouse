namespace ThaiDuongWarehouse.Domain.AggregateModels.GoodsReceiptAggregate
{
    public class GoodsReceipt : Entity, IAggregateRoot
    {
        public string GoodsReceiptId { get; private set; }
        public string? PurchaseOrderNumber { get; private set; }
        public DateTime Timestamp { get; private set; }
        public bool IsConfirmed { get; private set; }
        public List<GoodsReceiptLot> Lots { get; private set; }

        public GoodsReceipt(string goodsReceiptId, string? purchaseOrderNumber, DateTime timestamp, bool isConfirmed)
        {
            GoodsReceiptId = goodsReceiptId;
            PurchaseOrderNumber = purchaseOrderNumber;
            Timestamp = timestamp;
            IsConfirmed = isConfirmed;
            Lots = new List<GoodsReceiptLot>();
        }

        public void Update(string lotId, double quantity, double sublotSize, string purchaseOrderNumber, Location location, 
            DateTime productionDate, DateTime expirationDate)
        {
            var lot = Lots.FirstOrDefault(e => e.GoodsReceiptLotId == lotId);
            if (lot == null)
            {
                throw new Exception("Lot doesn't exist in the current GoodsReceipt");
            }
            lot.Update(quantity, sublotSize, purchaseOrderNumber, location, productionDate, expirationDate);
        }
        public void AddLot(GoodsReceiptLot goodsReceiptLot)
        {
            if(goodsReceiptLot is null)
                throw new ArgumentNullException(nameof(goodsReceiptLot));

            Lots.Add(goodsReceiptLot);
        }
        public void RemoveLot(string goodsReceiptLotId)
        {
            var lot = Lots.FirstOrDefault(e => e.GoodsReceiptLotId == goodsReceiptLotId);
            if (lot == null)
            {
                throw new WarehouseDomainException("Lot doesn't exist in the current GoodsReceipt");
            }
            Lots.Remove(lot);
        }
        public void Confirm()
        {
            
        }
    }
}
