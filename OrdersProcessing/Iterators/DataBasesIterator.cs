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
    class DataBasesIterator : IEnumerable<Order>
    {
        private IEnumerable<Order> local { set; get; }
        private IEnumerable<Order> global { set; get; }
        public DataBasesIterator(IEnumerable<Order> loc, IEnumerable<Order> glob)
        {
            local = loc;
            global = glob;
        }
        public IEnumerator<Order> GetEnumerator()
        {
            foreach(var order in local)
                yield return order;
            foreach (var order in global)
                yield return order;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
