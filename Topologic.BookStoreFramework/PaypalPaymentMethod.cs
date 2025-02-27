using Topologic.BookStoreFramework.Advanced;
namespace Topologic.BookStoreFramework
{
    /// <summary>
    /// Represents a concrete payment processor that can simulate the process for paying for an order using Paypal.
    /// </summary>
    public class PaypalPaymentMethod : IPaymentProcessor
    {
        /// <summary>
        /// Gets the name of the payment method.
        /// </summary>
        public string PaymentMethodName => "Paypal";

        /// <summary>
        /// Processes a payment for a customer.
        /// </summary>
        /// <inheritdoc/>
        /// <exception cref="PaymentProcessingException">Thrown when the customer does not have enough funds in their wallet.</exception>
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
