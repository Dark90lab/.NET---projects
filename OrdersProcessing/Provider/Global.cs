using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderProcessing.Orders;
using OrderProcessing.Shipment;
using OrderProcessing.LabelsFactory;

namespace OrderProcessing.Provider
{
    struct CountryOrder
    {
        public string Country;
        public Parcel parcel;
        public List<IShippableOrder> orders;
    }

    public class Global : IShipmentProvider
    {

        public string Name { get; set; }
        List<CountryOrder> shipmentOrders;
        TaxRatesDB taxes;

        public Global(TaxRatesDB taxDB)
        {
            taxes = taxDB;
            Name = "Global";
            shipmentOrders = new List<CountryOrder>();
        }
        public string GetLabelForOrder(IShippableOrder order)
        {
            return new GlobalLabel().GenerateLabelForOrder(Name, order.Recipient);
        }

        public IEnumerable<IParcel> GetParcels()
        {
            List<IParcel> parcels = new List<IParcel>();
            foreach (var sOrder in shipmentOrders)
            {
                var parcel = sOrder.parcel;
                var orders = sOrder.orders;
                int tax_in_percentage = 0;
                if (taxes.TaxRates.ContainsKey(sOrder.Country))
                    tax_in_percentage = taxes.TaxRates[sOrder.Country];

                LinearTaxCalculator lCalculator = new LinearTaxCalculator(tax_in_percentage);
                parcel.Summary = new SummaryFormatter(lCalculator).PrintOrdersSummary(orders);
                parcel.BundleHeader = new SummaryFormatter(lCalculator).PrintHeader(sOrder.Country);
                parcel.ShipmentProviderName = Name;
                foreach (var order in orders)
                {
                    parcel.BundlePrice += order.PaidAmount;
                    parcel.BundleTax += lCalculator.CalculateTax(order.PaidAmount);
                }
                parcel.BundlePriceWithTax += parcel.BundlePrice + parcel.BundleTax;
                parcels.Add(parcel);
            }
            return parcels;
        }

        public void RegisterForShipment(IShippableOrder order)
        {
            List<IShippableOrder> orders = shipmentOrders.Find(p => p.Country == order.Recipient.Country).orders;
            if (orders == null)
            {
                CountryOrder cd = new CountryOrder();
                cd.Country = order.Recipient.Country;
                cd.orders = new List<IShippableOrder>();
                cd.orders.Add(order);
                cd.parcel = new Parcel();
                shipmentOrders.Add(cd);
            }
            else
            {
                orders.Add(order);
            }
        }
    }
}
