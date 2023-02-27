namespace ThaiDuongWarehouse.Domain.Seedwork;
public interface IRepository<T> where T : IAggregateRoot
{
    IUnitOfWork unitOfWork { get; }
}
