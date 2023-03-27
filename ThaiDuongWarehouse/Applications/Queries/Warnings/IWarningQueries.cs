namespace ThaiDuongWarehouse.Api.Applications.Queries.Warnings;

public interface IWarningQueries
{
    Task<IEnumerable<ItemLotViewModel>> StockLevelWarnings(string itemClassId);
    Task<IEnumerable<ItemLotViewModel>> ExpirationWarnings(int months);
}
