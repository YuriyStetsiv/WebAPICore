using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Domain.Models.Enums
{
    public enum OrderStatus
    {
        None = 0,
        Processing = 1,
        NotPaid = 2,
        Paid = 3,
        InTheWay = 4,
        Delivered = 5,
        Received = 6
    }
}
