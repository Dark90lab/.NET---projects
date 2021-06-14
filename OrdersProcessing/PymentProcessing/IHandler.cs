using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderProcessing.Payments;
using OrderProcessing.Orders;

namespace OrderProcessing.PymentProcessing
{
    public interface IHandler
    {
        IHandler SetNext(IHandler handler);
        Payment Service(Payment request,Order order);

    }
}
