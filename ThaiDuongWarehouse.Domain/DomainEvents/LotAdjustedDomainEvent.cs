using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThaiDuongWarehouse.Domain.DomainEvents
{
    public class LotAdjustedDomainEvent : INotification
    {
        public bool IsConfirmed { get; private set; }
        public LotAdjustedDomainEvent(bool isConfirmed)
        {
            IsConfirmed = isConfirmed;
        }
    }
}
