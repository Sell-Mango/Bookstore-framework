
using Topologic.BookStore.Framework.Managers;
using Topologic.BookStore.Framework.Models;

namespace Topologic.BookStore.ClientTestCLI;
class Program
{
    static void Main(string[] args)
    {
        // Instansiates some Books
        PhysicalBook book1 = new PhysicalBook();
        PhysicalBook book2 = new PhysicalBook();
        PhysicalBook book3 = new PhysicalBook();
        // Instansiates Managers
        InventoryManager inventoryManager = new InventoryManager();

        // var book = new PhysicalBook("snømannen", "9788203450815", 249, "Kjell-Magne Larsen");

        // Console.WriteLine(book);
    }
}

