using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThaiDuongWarehouse.Domain.Exceptions
{
    public class WarehouseDomainException : Exception
    {
        public WarehouseDomainException() { }
        public WarehouseDomainException(string message) : base(message)
        {

        }

    }
}
