namespace ThaiDuongWarehouse.Domain.AggregateModels.ItemAggregate;
public interface IItemClassRepository
{
    Task<ItemClass?> GetById(string id);
}

