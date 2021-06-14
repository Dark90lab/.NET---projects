using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderProcessing.Payments;
using OrderProcessing.Orders;

namespace OrderProcessing.PymentProcessing
{
    //CHAIN OF RESPONSIBILITY PATTER
    abstract class PaymentService : IHandler
    {
        private IHandler _nextService;
        public virtual Payment Service(Payment request,Order order)
        {
            if (this._nextService != null)           
                return this._nextService.Service(request,order);
            else
                return null;
        }
        public IHandler SetNext(IHandler service)
        {
            this._nextService = service;
            return service;
        }
    }

    class PayPalPayment : PaymentService
    {
        private Random randomElement = new Random(1234);
        public override Payment Service(Payment request, Order order)
        {
            if (request.PaymentType == PaymentMethod.PayPal && order.Status != OrderStatus.ReadyForShipment)
            {
                if (randomElement.NextDouble() <= 0.3)
                {
                    Console.WriteLine($"Order {order.OrderId} payment {request.PaymentType} has failed");
                    return base.Service(request, order);
                }

                if (request.Amount >= order.AmountToBePaid)
                    request.Amount = order.AmountToBePaid;

                order.FinalizedPayments.Add(request);
                order.Status = OrderStatus.PaymentProcessing;
                Console.WriteLine($"Order {order.OrderId} paid {request.Amount} via {request.PaymentType}");

                if (order.DueAmount <= 0)
                {
                    order.Status = OrderStatus.ReadyForShipment;
                    Console.WriteLine($"Order {order.OrderId} is ready for shipment");
                }

                return request;
            }
            else
                return base.Service(request, order);
        }
    }


    class InvoicePayment : PaymentService
    {
        private int count = 0;
        public override Payment Service(Payment request, Order order)
        {
            
            if (request.PaymentType == PaymentMethod.Invoice && order.Status!=OrderStatus.ReadyForShipment)
            {
                count++;
                if (count%3 == 0)
                {
                    Console.WriteLine($"Order {order.OrderId} payment {request.PaymentType} has failed");
                    return base.Service(request, order);
                }

                if (request.Amount >= order.AmountToBePaid)
                    request.Amount = order.AmountToBePaid;

                order.FinalizedPayments.Add(request);
                order.Status = OrderStatus.PaymentProcessing;
                Console.WriteLine($"Order {order.OrderId} paid {request.Amount} via {request.PaymentType}");

                if (order.DueAmount <= 0)
                {
                    order.Status = OrderStatus.ReadyForShipment;
                    Console.WriteLine($"Order {order.OrderId} is ready for shipment");
                }
                return request;
            }
            else
                return base.Service(request, order);
        }
    }

    class CreditCardPayment : PaymentService
    {
        public override Payment Service(Payment request, Order order)
        {
            if (request.PaymentType == PaymentMethod.CreditCard && order.Status != OrderStatus.ReadyForShipment)
            {
                if (request.Amount >= order.AmountToBePaid)
                    request.Amount = order.AmountToBePaid;

                order.FinalizedPayments.Add(request);
                order.Status = OrderStatus.PaymentProcessing;
                Console.WriteLine($"Order {order.OrderId} paid {request.Amount} via {request.PaymentType}");
              
                if (order.DueAmount <= 0)
                {
                    order.Status = OrderStatus.ReadyForShipment;
                    Console.WriteLine($"Order {order.OrderId} is ready for shipment");
                }
                return request;
            }
            else
                return base.Service(request, order);
        }
    }



}
