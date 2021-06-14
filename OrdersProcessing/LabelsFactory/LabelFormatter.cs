using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderProcessing.Orders;
using OrderProcessing.Shipment;

namespace OrderProcessing.LabelsFactory
{

    class LocalPostLabel : ILabelFormatter
    {
        public string GenerateLabelForOrder(string providerName, IAddress address)
        {
            return $"Shipment provider: {providerName}\n{address.Name}\n{address.Line1}\n{address.Line2}\n{address.PostalCode}\n";
        }
    }

    class GlobalLabel : ILabelFormatter
    {
        public string GenerateLabelForOrder(string providerName, IAddress address)
        {
            return $"Shipment provider: {providerName}\n{address.Name}\n{address.Line1}\n{address.Line2}\n{address.PostalCode}\n{address.Country}\n";
        }
    }


}
