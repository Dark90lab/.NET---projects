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
    public class LocalPost : IShipmentProvider
    {
        public string Name { set; get; }
        Parcel parcel;
        List<IShippableOrder> orders;
        TaxRatesDB taxes;
        
        public LocalPost(TaxRatesDB tx)
        {
            Name = "LocalPost";
            orders = new List<IShippableOrder>();
            parcel = new Parcel();
            taxes = tx;
            parcel.ShipmentProviderName = Name;
           
        }
     
        public IEnumerable<IParcel> GetParcels()
        {
            int tax_in_percentage=0;
            if(taxes.TaxRates.ContainsKey("Polska"))
                tax_in_percentage = taxes.TaxRates["Polska"];

            LinearTaxCalculator lCalculator = new LinearTaxCalculator(tax_in_percentage);
            parcel.Summary = new SummaryFormatter(lCalculator).PrintOrdersSummary(orders);
            parcel.BundleHeader = new SummaryFormatter(lCalculator).PrintHeader("Polska");
            foreach(var order in orders)
            {
                parcel.BundlePrice += order.PaidAmount;
                parcel.BundleTax += lCalculator.CalculateTax(order.PaidAmount);
            }
            parcel.BundlePriceWithTax += parcel.BundlePrice + parcel.BundleTax;
            return new[] { parcel };
        }

        public void RegisterForShipment(IShippableOrder order)
        {
            orders.Add(order);
        }

        public string GetLabelForOrder(IShippableOrder order)
        {
            return new LocalPostLabel().GenerateLabelForOrder(Name, order.Recipient);
        }
    }
}
