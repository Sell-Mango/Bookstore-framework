using System.Collections.ObjectModel;
using Topologic.BookStoreFramework.Advanced;

namespace Topologic.BookStoreFramework
{
    /// <summary>
    /// Represents a customer that can buy books and keep track of orders history. 
    /// An unique identifier is generated for each customer.
    /// </summary>
    public class Customer
    {
        private string _email;
        private string _firstName;
        private string _lastName;
        private readonly List<Order> _ordersHistory;

        /// <summary>
        /// Creates a new instance of a Customer class with email only (minimal constructor).
        /// </summary>
        /// <param name="email">Email address for the customer.</param>
        public Customer(string email)
        {
            CustomerId = Guid.NewGuid().ToString();
            Email = email;
            _firstName = string.Empty;
            _lastName = string.Empty;
            Wallet = 0;
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
            CustomerId = Guid.NewGuid().ToString();
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            Wallet = 0;
            _ordersHistory = [];
        }

        /// <summary>
        /// Gets or sets the email address for the customer.
        /// </summary>
        /// <value>Customer email address.</value>
        /// <exception cref="ArgumentNullException">Thrown when email is null or empty.</exception>
        /// <exception cref="ArgumentException">Thrown when email is invalid format, see <see cref="CustomerValidator"/> for specifications.</exception>
        public string Email { 
            get => _email;
            set
            {
                if(string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value), "Email cannot be null or empty.");
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
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value), "First name cannot be null or empty.");
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
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value), "Last name cannot be null or empty.");
                _lastName = value;
            }
        }

        /// <summary>
        /// Gets the unique id for the customer. 
        /// </summary>
        /// <value>Customer id that is automatically generated.</value>
        public string CustomerId { get; }

        /// <summary>
        /// Gets the wallet balance of the customer. Use <see cref="AddFundsToWallet"/> to add and <see cref="DecreaseFundsFromWallet"/> to decrease funds.
        /// </summary>
        /// <value>Current wallet balance of customer.</value>
        public double Wallet { get; private set; }

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
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="amountToAdd"/> is 0 or negative.</exception>
        public bool AddFundsToWallet(double amountToAdd)
        {
            if (amountToAdd <= 0) throw new ArgumentOutOfRangeException(nameof(amountToAdd), "Amount to add to balance must be greater than 0.");

            Wallet += amountToAdd;
            return true;
        }

        /// <summary>
        /// Decreases funds from the customer's wallet.
        /// </summary>
        /// <param name="amountToDecrease">Amount to decrease.</param>
        /// <returns>True if funds is successfully decreased from the <see cref="Wallet"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if <paramref name="amountToDecrease"/> is 0 or negative.</exception>
        /// <exception cref="InvalidOperationException">Thrown if insuficcent amount is being decreased from <see cref="Wallet"/>.</exception>
        public bool DecreaseFundsFromWallet(double amountToDecrease)
        {
            if (amountToDecrease <= 0) throw new ArgumentOutOfRangeException(nameof(amountToDecrease), "Amount to decrease from balance must be greater than 0.");

            if (amountToDecrease > Wallet)
            {
                throw new InvalidOperationException("Cannot decrease amount of money. Are you sure there are sufficcent balance in your wallet?");
            }
            Wallet -= amountToDecrease;
            return true;
        }

        /// <summary>
        /// Adding a newly created <see cref="Order"/> to the customer's order history."/>
        /// Should be called after the order is created from a manager class like <see cref="PaymentManager"/>.
        /// </summary>
        /// <param name="order">Order to be added to <see cref="OrdersHistory"/>.</param>
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
