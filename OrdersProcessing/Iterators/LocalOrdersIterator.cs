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
 
    class LocalOrdersIterator : IEnumerable<Order>
    {
        private LocalOrdersDB localOrders { set; get; }
        public LocalOrdersIterator(LocalOrdersDB locOrdDB)
        {
            localOrders = locOrdDB;
        }

        public IEnumerator<Order> GetEnumerator()
        {
            foreach (var order in localOrders.Orders)
                yield return order;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
