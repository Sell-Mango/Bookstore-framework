using System.Collections.ObjectModel;
using Topologic.BookStoreFramework.Utilities;

namespace Topologic.BookStoreFramework
{
    public class Customer
    {
        private readonly string _customerId;
        private string _email;
        private string _firstName;
        private string _lastName;
        private double _wallet;

        public Customer(string email)
        {
            _customerId = Guid.NewGuid().ToString();
            Email = email;
            _firstName = string.Empty;
            _lastName = string.Empty;
            _wallet = 0;
        }

        public Customer(string email, string firstName, string lastName)
        {
            _customerId = Guid.NewGuid().ToString();
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            _wallet = 0;
        }


        public string CustomerId { get => _customerId; }
        public string Email { 
            get => _email;
            set
            {
                if(string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value), "Email cannot be null or empty");
                if (!CustomerValidator.IsEmailValid(value)) throw new ArgumentException("Invalid email format.", nameof(value));
                _email = value;
            }
        }
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value), "First name cannot be empty or null.");
                _firstName = value;
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value), "Last name cannot be empty or null");
                _lastName = value;
            }
        }
        public double Wallet { get => _wallet; }
        public Collection<Order> OrderHistory { get; } = [];

        public bool AddFundsToWallet(double amountToAdd)
        {
            if (amountToAdd <= 0) throw new ArgumentException("Cannot add zero or negative balanse", nameof(amountToAdd));

            _wallet += amountToAdd;
            return true;
        }

        public bool AddToOrderHistory(Order order)
        {
            ArgumentNullException.ThrowIfNull(order, nameof(order));

            OrderHistory.Add(order);
            return true;
        }

    }
}
