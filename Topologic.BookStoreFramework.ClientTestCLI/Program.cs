using Topologic.BookStoreFramework.Advanced;

namespace Topologic.BookStoreFramework.ClientTestCLI;
class Program
{
    static void Main(string[] args)
    {
        // 1. Setup
        // 1.1 innstaniate managers
        InventoryManager inventoryManager = new();
        PaymentManager paymentManager = new(inventoryManager);
        IPaymentProcessor paymentProcessor;


        // 1.2 Create a customer and add funds to waller
        Customer customer1 = new("test@hiof.no");
        customer1.AddFundsToWallet(5000);

        // 1.3 Create a new shopping cart for customer1
        ShoppingCart shoppingCartCustomer1 = new(inventoryManager, customer1.CustomerId);


        // 2. Create some different derived books
        // Valid ISBN numbers for testing can be generated here: https://generate.plus/en/number/isbn

        // 2.1 - Create two physical books, one with only ISBNm other with basic arguments.
        PhysicalBook physicalBook1 = new("978-0-1609-7689-6");
        PhysicalBook physicalBook2 = new("978-9-1816-1373-5", "The Hobbit", 299, 389, BookCoverType.Hardcover);

        // 2.2 - Create two audio books, one with only ISBN, other with basic arguments.
        AudioBook audioBook1 = new("0-8286-7143-5");
        AudioBook audioBook2 = new("0-7174-6601-9", "Heksene", 249, new TimeSpan(3, 25, 26), "Trond Teigen");

        // 2.3 - Setting properties for books created with minimalistic constructor
        physicalBook1.Title = "A journey to the west";
        physicalBook1.Price = 499;
        audioBook1.Title = "Eragon";
        audioBook1.Price = 329;


        // 3. Add books to bookstore inventory
        // Adds the same book twice
        inventoryManager.AddBook(physicalBook1);
        // Specify quantity
        inventoryManager.AddBook(physicalBook2, 5);
        inventoryManager.AddBook(audioBook1, 3);
        inventoryManager.AddBook(audioBook2, 20);


        // 4. Now that we have some books in the inventory, we can add them to the shopping cart
        // Saving the results of adding to cart in BookOperationResult

        // 4.1 - Option 1: Find book in inventory and add it to the users shopping cart by ISBN.
        if(inventoryManager.TryFindBookByIsbn("978-0-1609-7689-6", out var foundBook1))
        {
            try
            {
                shoppingCartCustomer1.AddToCart(foundBook1);
            }
            catch (OutOfStockException)
            {
                Console.WriteLine("Desired book is currently out of stock, try add another to cart.");
            }
        }

        // 4.2 - Option 2: Find book in inventory and add it to the users shopping cart by Title and specify quantity.
        if (inventoryManager.TryFindBookByTitle("The Hobbit", out var foundBook2))
        {
            try
            {
                shoppingCartCustomer1.AddToCart(foundBook2, 2);
            }
            catch (OutOfStockException)
            {
                Console.WriteLine("Desired book is currently out of stock, try add another to cart.");
            }
        }

        // 4.3 - Option 3: Add a given book by title using FindBookByTitle method
        try
        { 
            shoppingCartCustomer1.AddToCart(inventoryManager.FindBookByTitle("Heksene"), 3);
        }
        catch(KeyNotFoundException)
        {
            Console.WriteLine("Book not found in inventory, add it to inventory before adding to cart.");
        }
        catch (OutOfStockException)
        {
            Console.WriteLine("Desired book is currently out of stock, try add another to cart.");
        }


        // 5. Validate shopping cart and checkout
        try
        {
            paymentManager.ValdiateCorrectCustomer(customer1, shoppingCartCustomer1);

            // 5.1 User chooses payment method
            paymentProcessor = new PaypalPaymentMethod();
            paymentManager.PaymentProcessor = paymentProcessor;

            // 5.2 Checkout
            try
            {
                paymentManager.PurchaseOrder(customer1, shoppingCartCustomer1);
            }
            catch (PaymentProcessingException)
            {
                Console.WriteLine($"Something went wrong while processing order with {paymentManager.PaymentProcessor.PaymentMethodName}.");
            }
        }
        catch (ArgumentException)
        {
            Console.WriteLine("Invalid customer.");
        }


        // 6. Print the order made by customer1
        try
        {
            Console.WriteLine(customer1.OrdersHistory[0]);
        }
        catch (ArgumentOutOfRangeException)
        {
            Console.WriteLine("No orders found in order history.");
        }
    }
}

