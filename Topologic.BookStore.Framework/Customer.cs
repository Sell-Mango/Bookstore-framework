
using System.Collections.ObjectModel;
using Topologic.BookStore.Framework.Utilities;

namespace Topologic.BookStore.Framework.Models
{
    public class Customer
    {
        private readonly string _customerId;
        private string _email = string.Empty;
        private string _firstName = string.Empty;
        private string _lastName = string.Empty;
        private double _wallet = 0;

        public Customer(string email)
        {
            _customerId = Guid.NewGuid().ToString();
            Email = email;
        }

        public Customer(string email, string firstName, string lastName)
        {
            _customerId = Guid.NewGuid().ToString();
            Email = email;

            FirstName = firstName;
            LastName = lastName;
        }


        public string CustomerId { get => _customerId; }
        public string Email { 
            get => _email;
            set
            {
                if (!CustomerValidator.IsEmailValid(value)) throw new ArgumentException("Invalid email format.", nameof(value));
                _email = value;
            }
        }
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentException("First name cannot be empty or null.", nameof(value));
                _firstName = value;
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentException("Last name cannot be empty or null", nameof(value));
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
            if(order is null) throw new ArgumentNullException(nameof(order), "Order cannot be null");
            OrderHistory.Add(order);
            return true;
        }

    }
}
