using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topologic.BookStoreFramework
{
    public interface IPaymentProcessor
    {
        bool ProcessPayment(Customer customer, double amountToPay);

        string PaymentMethodName { get; }
    }
}
