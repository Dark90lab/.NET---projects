using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderProcessing.Orders;
using OrderProcessing.Shipment;

namespace OrderProcessing.Provider
{
    //FACADE
    class ShipmentRegister
    {
        public List<IShipmentProvider> providers = new List<IShipmentProvider>();
        public IShipmentProvider ShipmentProceeding(Order order,TaxRatesDB taxDB)
        {
            if(order.Recipient.Country == "Polska")
            {
                IShipmentProvider local = providers.Find(p => p.Name == "LocalPost");
                if (local == null)
                {
                    LocalPost lp = new LocalPost(taxDB);
                    providers.Add(lp);
                    lp.RegisterForShipment(order);
                    return lp;
                }
                else
                {
                    local.RegisterForShipment(order);
                    return local;
                }
            }
            else
            {
                IShipmentProvider glob = providers.Find(p => p.Name == "Global");
                if (glob == null)
                {
                    Global gb = new Global(taxDB);
                    gb.RegisterForShipment(order);
                    providers.Add(gb);
                    return gb;
                }
                else
                {
                    glob.RegisterForShipment(order);
                    return glob;
                }

            }
        }
    }
}
