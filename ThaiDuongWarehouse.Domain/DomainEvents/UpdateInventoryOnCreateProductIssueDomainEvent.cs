﻿namespace ThaiDuongWarehouse.Domain.DomainEvents;
public class UpdateInventoryOnCreateProductIssueDomainEvent : INotification
{
    public Item Item { get; private set; }
    public string PurchaseOrderNumber { get; private set; }
    public double Quantity { get; private set; }
    public DateTime Timestamp { get; private set; }

    public UpdateInventoryOnCreateProductIssueDomainEvent(Item item, string purchaseOrderNumber, double quantity, DateTime timestamp)
    {
        Item = item;
        PurchaseOrderNumber = purchaseOrderNumber;
        Quantity = quantity;
        Timestamp = timestamp;
    }
}
