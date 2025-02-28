using EF_FinalWork_library.Entities;
using Library.Persistance;
using System;

class Program
{
    static void Main()
    {
        using var context = new LibraryDbContext();
        var bookstore = new Bookstore(context);

        while (true)
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Add book");
            Console.WriteLine("2. Edit book");
            Console.WriteLine("3. Remove book");
            Console.WriteLine("4. Reserve book");
            Console.WriteLine("5. Show all books");
            Console.WriteLine("0. Exit");

            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    bookstore.AddBook();
                    break;
                case "2":
                    bookstore.EditBook();
                    break;
                case "3":
                    bookstore.RemoveBook();
                    break;
                case "4":
                    bookstore.ReserveBook();
                    break;
                case "5":
                    bookstore.DisplayAllBooks();
                    break;
                case "0":
                    Console.WriteLine("Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }
}
