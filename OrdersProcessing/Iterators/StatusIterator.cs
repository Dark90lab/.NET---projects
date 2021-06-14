//Orders Processig
//Mateusz Grzelak

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderProcessing.Databases;
using OrderProcessing.Orders;

namespace OrderProcessing.Iterators
{
    //Decorator
    class StatusIterator : IEnumerable<Order>
    {
        IFilter filter;
        IEnumerable<Order> Orders;
 
        public StatusIterator(IEnumerable<Order> od, IFilter f)
        {
            filter = f;
            Orders = od;
        }


        public IEnumerator<Order> GetEnumerator()
        {

           foreach(var order in Orders)
            {
                if (filter.FilterValue(order))
                    yield return order;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
