
using Topologic.BookStore.Framework.Managers;
using Topologic.BookStore.Framework.Models;

namespace Topologic.BookStore.ClientTestCLI;
class Program
{
    static void Main(string[] args)
    {
        // Instansiates some Books with each overload
        // Physical books
        PhysicalBook book1 = new PhysicalBook("Lord of the Rings: Two Towers", "978-3-8747-4427-0", 299, 3.25, 322);
        PhysicalBook book2 = new PhysicalBook("Witcher", "978-0-7330-7673-2", 370, 3.5, 456, BookCoverType.Hardcover, ["Navn navnesen", "Ola Olasen"]);
        PhysicalBook book3 = new PhysicalBook("Snømannen", "0-3599-3099-9", 599, 4.345, 588, BookCoverType.Paperback, ["Lars Fjell"], "Some description", "Nb-No", "Egmont", new DateTime(2011, 05, 12));

        // Audio books
        AudioBook audioBook1 = new AudioBook("Heksene", "978-0-4980-8315-0", 199, new TimeSpan(5, 6, 22), "Navn Navnesen");
        AudioBook audioBook2 = new AudioBook("A journey to the west", "978-9-0154-2640-1", 329, new TimeSpan(8, 33, 12), "Ola Normann");
        AudioBook audioBook3 = new AudioBook("Dune", "", 349, new TimeSpan(12, 59, 45), "Kari Karisen");
        
        // Instansiates Managers
        InventoryManager inventoryManager = new InventoryManager();


    }
}

