using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThaiDuongWarehouse.Domain.Seedwork
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork unitOfWork { get; }
    }
}
