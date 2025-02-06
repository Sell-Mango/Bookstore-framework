
using Topologic.BookStore.Framework.Models;

namespace Topologic.BookStore.ClientTestCLI;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var book = new PhysicalBook("snømannen", "9788203450815", 249, "Kjell-Magne Larsen");

        Console.WriteLine(book);
    }
}

