namespace ThaiDuongWarehouse.Domain.AggregateModels.FinishedProductReceiptAggregate;
public interface IFinishedProductReceiptRepository : IRepository<FinishedProductReceipt>
{
    Task<FinishedProductReceipt?> GetReceiptById(string finishedProductReceiptId);
    Task<FinishedProductReceipt> Add(FinishedProductReceipt finishedProductReceipt);
    void Update(FinishedProductReceipt finishedProductReceipt);
    void Remove(FinishedProductReceipt finishedProductReceipt);
}
