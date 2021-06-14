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
    class GlobalOrdersIterator : IEnumerable<Order>
    {
        private Order Root { set; get; }
        private GlobalOrdersDB gd { set; get; }
        private List<Order> Orders = new List<Order>();

        public GlobalOrdersIterator(GlobalOrdersDB gdDB)
        {
            gd = gdDB;
            BinarySearch(gd.Root);
        }

        //Level Order Binary Tree Traversal
        public int height(OrderNode node)
        {
            if (node == null)
                return 0;
            else
            {
                int lheight = height(node.Left);
                int rheight = height(node.Right);

                if (lheight > rheight)
                {
                    return (lheight + 1);
                }
                else
                {
                    return (rheight + 1);
                }
            }
        }

        void BinarySearchCurrent(OrderNode node, int lvl)
        {
            if (node == null)
                return;
            if (lvl == 1)
                Orders.Add(node.Order);
            else if (lvl > 1)
            {
                BinarySearchCurrent(node.Left, lvl - 1);
                BinarySearchCurrent(node.Right, lvl - 1);
            }
        }

        void BinarySearch(OrderNode node)
        {
            int h = height(node);
            for (int i = 1; i <= h; i++)
                BinarySearchCurrent(node, i);

        }

        public IEnumerator<Order> GetEnumerator()
        {
            foreach (var order in Orders)
                yield return order;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
