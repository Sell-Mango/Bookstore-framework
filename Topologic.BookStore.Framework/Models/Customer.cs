
using System.Collections.ObjectModel;
using Topologic.BookStore.Framework.Utilities;

namespace Topologic.BookStore.Framework.Models
{
    public class Customer
    {
        private readonly string _customerId;
        private string _email;
        private string _firstName;
        private string _lastName;

        public Customer(string customerId, string email)
        {
            _customerId = customerId;
            Email = email;
            FirstName = string.Empty;
            LastName = string.Empty;

        }

        public Customer(string customerId, string email, string firstName, string lastName)
        {
            _customerId = customerId;
            Email = email;

            _firstName = firstName;
            _lastName = lastName;
        }


        public string CustomerId { get => _customerId; }
        public string Email { 
            get => _email;
            set
            {
                if (!CustomerValidator.IsEmailValid(value)) throw new ArgumentException("Invalid email format", nameof(value));
                _email = value;
            }
        }
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentException("First name cannot be empty or null", nameof(value));
                _firstName = value;
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Last name cannot be empty or null", nameof(value));
                _lastName = value;
            }
        }
        public Collection<Order> Orders { get; private set; } = new Collection<Order>();

        public bool AddOrder(Order order)
        {
            if (order == null) return false;
            Orders.Add(order);
            return true;
        }

    }
}
