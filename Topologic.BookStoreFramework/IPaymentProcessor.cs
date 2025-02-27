using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Topologic.BookStoreFramework
{
    /// <summary>
    /// Represents a payment processor that can process payments for customers. Should be implemented by payment method classes.
    /// </summary>
    public interface IPaymentProcessor
    {
        /// <summary>
        /// Processes a payment for a customer.
        /// </summary>
        /// <param name="customer">The customer to proccess the payment for.</param>
        /// <param name="amountToPay">Amount to pay.</param>
        /// <returns></returns>
        bool ProcessPayment(Customer customer, double amountToPay);

        /// <summary>
        /// Gets the name of the payment method.
        /// </summary>
        string PaymentMethodName { get; }
    }
}
