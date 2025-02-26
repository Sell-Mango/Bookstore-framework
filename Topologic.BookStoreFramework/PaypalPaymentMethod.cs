using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Topologic.BookStoreFramework.Advanced;
namespace Topologic.BookStoreFramework
{
    public class PaypalPaymentMethod : IPaymentProcessor
    {
        public string PaymentMethodName => "Paypal";

        public bool ProcessPayment(Customer customer, double amountToPay)
        {
            if (customer.Wallet >= amountToPay)
            {
                customer.DecreaseFundsFromWallet(amountToPay);
                return true;
            }

            throw new PaymentProcessingException("Not enough funds in wallet.");
        }
    }
}
