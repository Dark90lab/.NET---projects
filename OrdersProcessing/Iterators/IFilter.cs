using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderProcessing.Databases;
using OrderProcessing.Orders;

namespace OrderProcessing.Iterators
{
    public interface IFilter
    {
        bool FilterValue(Order order);

    }
}
