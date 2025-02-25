using System.Collections.ObjectModel;
using Topologic.BookStoreFramework.Advanced;

namespace Topologic.BookStoreFramework
{
    /// <summary>
    /// Represents a customer that can buy books. 
    /// An unique identifier is generated for each customer.
    /// </summary>
    public class Customer
    {
        private readonly string _customerId;
        private string _email;
        private string _firstName;
        private string _lastName;
        private double _wallet;
        private readonly Collection<Order> _ordersHistory;

        /// <summary>
        /// Creates a new instance of a Customer class with email only (minimal constructor).
        /// </summary>
        /// <param name="email">Email address for the customer.</param>
        public Customer(string email)
        {
            _customerId = Guid.NewGuid().ToString();
            Email = email;
            _firstName = string.Empty;
            _lastName = string.Empty;
            _wallet = 0;
            _ordersHistory = [];
        }

        /// <summary>
        /// Creates a new instance of a Customer class with basic information.
        /// </summary>
        /// <param name="email">Email address for the customer.</param>
        /// <param name="firstName">First name of the customer.</param>
        /// <param name="lastName">Last name of the customer.</param>
        public Customer(string email, string firstName, string lastName)
        {
            _customerId = Guid.NewGuid().ToString();
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            _wallet = 0;
            _ordersHistory = [];
        }

        /// <summary>
        /// Gets the unique id for the customer. 
        /// </summary>
        /// <value>Customer id that is automatically generated.</value>
        public string CustomerId { get => _customerId; }

        /// <summary>
        /// Gets or sets the email address for the customer.
        /// </summary>
        /// <value>Customer email address.</value>
        /// <exception cref="ArgumentNullException">Thrown when email is null or empty.</exception>
        /// <exception cref="ArgumentException">Thrown when email is invalid format, see <see cref="CustomerValidator"/> for specifications.</exception>"
        public string Email { 
            get => _email;
            set
            {
                if(string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value), "Email cannot be null or empty");
                if (!CustomerValidator.IsEmailValid(value)) throw new ArgumentException("Invalid email format.", nameof(value));
                _email = value;
            }
        }
        /// <summary>
        /// Gets or sets the first name of the customer.
        /// </summary>
        /// <value>Customer first name</value>
        /// <exception cref="ArgumentNullException">Thrown when first name is null or empty.</exception>
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value), "First name cannot be empty or null.");
                _firstName = value;
            }
        }

        /// <summary>
        /// Gets or sets the last name of the customer.
        /// </summary>
        /// <value>Customer last name</value>
        /// <exception cref="ArgumentNullException">Thrown when last name is null or empty.</exception>
        public string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value), "Last name cannot be empty or null");
                _lastName = value;
            }
        }

        /// <summary>
        /// Gets the wallet balance of the customer.
        /// </summary>
        /// <value>Current wallet balance of customer.</value>
        public double Wallet { get => _wallet; }

        /// <summary>
        /// Gets the order history of the customer.
        /// </summary>
        /// <value>Read-only collection of orders.</value>
        public ReadOnlyCollection<Order> OrdersHistory => _ordersHistory.AsReadOnly();

        /// <summary>
        /// Adds funds to the customer's wallet.
        /// </summary>
        /// <param name="amountToAdd">Amount of funds to add to <see cref="Wallet"/></param>
        /// <returns>True if a positive amount is successfully added, otherwise false.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if zero or negative amount is added.</exception>
        public bool AddFundsToWallet(double amountToAdd)
        {
            if (amountToAdd <= 0) throw new ArgumentOutOfRangeException(nameof(amountToAdd), "Cannot add zero or negative balanse");

            _wallet += amountToAdd;
            return true;
        }

        /// <summary>
        /// Adding a newly created <see cref="Order"/> to the customer's order history."/>
        /// Should be called after the order is created from a manager class like <see cref="PaymentManager"/>.
        /// </summary>
        /// <param name="order">Order to be added to <see cref="OrdersHistory"/></param>
        /// <returns>True if an order object is successfully added to <see cref="OrdersHistory"/>, otherwise false or an <see cref="ArgumentNullException"/>.</returns>
        /// <exception cref="ArgumentNullException">Thrown if order is null.</exception>"
        public bool AddToOrdersHistory(Order order)
        {
            ArgumentNullException.ThrowIfNull(order, nameof(order));

            _ordersHistory.Add(order);
            return true;
        }

    }
}
