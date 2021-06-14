using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderProcessing.Orders;

namespace OrderProcessing.Iterators
{
    public class ReadyShipmentFilter : IFilter
    {
        public bool FilterValue(Order order)
        {
            if (order.Status == OrderStatus.ReadyForShipment)
                return true;
            else
                return false;
        }
    }
}
