namespace Topologic.BookStoreFramework.ClientTestCLI;
class Program
{
    static void Main(string[] args)
    {
        // 1. Setup
        // innstaniate managers
        InventoryManager inventoryManager = new();
        PaymentManager paymentManager = new(inventoryManager);

        // Create a customer and add funds to waller
        Customer customer1 = new("test@hiof.no");
        customer1.AddFundsToWallet(5000);

        // Create a new shopping cart for customer1
        ShoppingCart shoppingCartCustomer1 = new(inventoryManager, customer1.CustomerId);


        // 2. Create some different derived books
        // Valid ISBN numbers for testing can be generated here: https://generate.plus/en/number/isbn
        PhysicalBook physicalBook1 = new("978-0-1609-7689-6");
        PhysicalBook physicalBook2 = new("978-9-1816-1373-5", "The Hobbit", 299, 389, BookCoverType.Hardcover);
        AudioBook audioBook1 = new("0-8286-7143-5");
        AudioBook audioBook2 = new("0-7174-6601-9", "Heksene", 249, new TimeSpan(3, 25, 26), "Trond Teigen");
        EBook eBook1 = new("0-4334-8984-7", "The Witcher", 199, 24);

        physicalBook1.Title = "A journey to the west";
        physicalBook1.Price = 499;
        audioBook1.Title = "Eragon";
        audioBook1.Price = 329;


        // 3. Add books to inventory
        // Adds the same book twice
        inventoryManager.AddBook(physicalBook1);
        // Specify quantity
        inventoryManager.AddBook(physicalBook2, 5);
        inventoryManager.AddBook(audioBook1, 3);
        inventoryManager.AddBook(audioBook2, 20);
        inventoryManager.AddBook(eBook1);


        // 4. Try retrieving books from inventory and add them to shopping cart
        // Saving the results of adding to cart in BookOperationResult

        // Finding and adding book by ISBN
        BookOperationResult addedToCart1 = shoppingCartCustomer1.AddToCart(inventoryManager.FindBookByIsbn("978-0-1609-7689-6"));
        BookOperationResult addedToCart2 = shoppingCartCustomer1.AddToCart(inventoryManager.FindBookByIsbn("978-9-1816-1373-5"), 2);

        // Finding and adding book by title
        BookOperationResult addedToCart3 = shoppingCartCustomer1.AddToCart(inventoryManager.FindBookByTitle("Eragon"));

        // Finding book by title and by explicit typecasting to AudioBook before adding to cart
        AudioBook tempAudioBook = (AudioBook)inventoryManager.FindBookByTitle("Heksene");
        BookOperationResult addedToCart4 = shoppingCartCustomer1.AddToCart(tempAudioBook, 3);

        BookOperationResult addedToCart5 = shoppingCartCustomer1.AddToCart(inventoryManager.FindBookByTitle("The Witcher"));

        // Writing results to console
        Console.WriteLine(addedToCart1);
        Console.WriteLine(addedToCart2);
        Console.WriteLine(addedToCart3);
        Console.WriteLine(addedToCart4);
        Console.WriteLine(addedToCart5);


        // 5. Validate shopping cart and checkout
        if(paymentManager.ValdiateCorrectCustomer(customer1, shoppingCartCustomer1))
        {
            // Checkout
            paymentManager.PurchaseOrder(customer1, shoppingCartCustomer1);
 
        }

        // 7. Print the order made by customer1
        Console.WriteLine(customer1.OrdersHistory[0]);

    }
}

