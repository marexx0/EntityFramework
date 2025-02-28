using EF_FinalWork_library.Entities;
using Library.Persistance;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

public class Bookstore
{
    private readonly LibraryDbContext _context;
    public Bookstore(LibraryDbContext context)
    {
        _context = context;
    }

    public void AddBook()
    {
        Console.Write("Enter book title: ");
        string title = Console.ReadLine();

        Console.Write("Enter page count: ");
        int pageCount = int.Parse(Console.ReadLine());

        Console.Write("Enter genre: ");
        string genre = Console.ReadLine();

        Console.Write("Enter year of publication: ");
        int year = int.Parse(Console.ReadLine());

        Console.Write("Enter cost price: ");
        double costPrice = double.Parse(Console.ReadLine());

        Console.Write("Enter sale price: ");
        double salePrice = double.Parse(Console.ReadLine());

        Console.Write("Enter stock count: ");
        int stockCount = int.Parse(Console.ReadLine());

        Console.Write("Enter author ID: ");
        int authorId = int.Parse(Console.ReadLine());

        Console.Write("Enter publisher ID: ");
        int publisherId = int.Parse(Console.ReadLine());

        var newBook = new Book
        {
            Title = title,
            PageCount = pageCount,
            Genre = genre,
            Year = year,
            CostPrice = costPrice,
            SalePrice = salePrice,
            StockCount = stockCount,
            AuthorId = authorId,
            PublisherId = publisherId
        };

        _context.Books.Add(newBook);
        _context.SaveChanges();

        Console.WriteLine("Book added successfully!");
    }

    public void EditBook()
    {
        Console.Write("Enter book ID to edit: ");
        int bookId = int.Parse(Console.ReadLine());

        var bookToEdit = _context.Books.FirstOrDefault(b => b.Id == bookId);
        if (bookToEdit == null)
        {
            Console.WriteLine("Book not found.");
            return;
        }

        Console.Write("Enter new title (leave empty to keep current): ");
        string title = Console.ReadLine();
        if (!string.IsNullOrEmpty(title)) bookToEdit.Title = title;

        Console.Write("Enter new page count (leave empty to keep current): ");
        string pageCount = Console.ReadLine();
        if (!string.IsNullOrEmpty(pageCount)) bookToEdit.PageCount = int.Parse(pageCount);

        Console.Write("Enter new genre (leave empty to keep current): ");
        string genre = Console.ReadLine();
        if (!string.IsNullOrEmpty(genre)) bookToEdit.Genre = genre;

        Console.Write("Enter new year (leave empty to keep current): ");
        string year = Console.ReadLine();
        if (!string.IsNullOrEmpty(year)) bookToEdit.Year = int.Parse(year);

        Console.Write("Enter new cost price (leave empty to keep current): ");
        string costPrice = Console.ReadLine();
        if (!string.IsNullOrEmpty(costPrice)) bookToEdit.CostPrice = double.Parse(costPrice);

        Console.Write("Enter new sale price (leave empty to keep current): ");
        string salePrice = Console.ReadLine();
        if (!string.IsNullOrEmpty(salePrice)) bookToEdit.SalePrice = double.Parse(salePrice);

        Console.Write("Enter new stock count (leave empty to keep current): ");
        string stockCount = Console.ReadLine();
        if (!string.IsNullOrEmpty(stockCount)) bookToEdit.StockCount = int.Parse(stockCount);

        _context.SaveChanges();
        Console.WriteLine("Book updated successfully!");
    }

    public void RemoveBook()
    {
        Console.Write("Enter book ID to remove: ");
        int bookId = int.Parse(Console.ReadLine());

        var bookToRemove = _context.Books.FirstOrDefault(b => b.Id == bookId);
        if (bookToRemove != null)
        {
            _context.Books.Remove(bookToRemove);
            _context.SaveChanges();
            Console.WriteLine("Book removed successfully!");
        }
        else
        {
            Console.WriteLine("Book not found.");
        }
    }

    public void ReserveBook()
    {
        Console.Write("Enter book ID to reserve: ");
        int bookId = int.Parse(Console.ReadLine());

        Console.Write("Enter customer ID: ");
        int customerId = int.Parse(Console.ReadLine());

        var bookToReserve = _context.Books.FirstOrDefault(b => b.Id == bookId);
        if (bookToReserve != null && bookToReserve.StockCount > 0)
        {
            var reservation = new Reservation
            {
                BookId = bookToReserve.Id,
                CustomerId = customerId,
                ReservationDate = DateTime.Now
            };
            _context.Reservations.Add(reservation);
            bookToReserve.StockCount--;
            _context.SaveChanges();
            Console.WriteLine("Book reserved successfully!");
        }
        else
        {
            Console.WriteLine("Book not available for reservation.");
        }
    }
    public void RemoveReservation()
    {
        Console.Write("Enter reservation ID to remove: ");
        if (!int.TryParse(Console.ReadLine(), out int reservationId))
        {
            Console.WriteLine("Invalid input.");
            return;
        }

        var reservation = _context.Reservations
            .Include(r => r.Book)
            .FirstOrDefault(r => r.Id == reservationId);

        if (reservation == null)
        {
            Console.WriteLine("Reservation not found.");
            return;
        }

        _context.Reservations.Remove(reservation);

        if (reservation.Book != null)
        {
            reservation.Book.StockCount++;
        }

        _context.SaveChanges();
        Console.WriteLine("Reservation removed successfully.");
    }

    public void ShowReservations()
    {
        Console.Write("Enter customer ID: ");
        int customerId = int.Parse(Console.ReadLine());

        var customer = _context.Customers
            .Include(c => c.Reservations)
            .ThenInclude(r => r.Book)
            .FirstOrDefault(c => c.Id == customerId);

        if (customer == null)
        {
            Console.WriteLine("Customer not found.");
            return;
        }

        Console.WriteLine($"Customer: {customer.Name}");
        if (customer.Reservations.Any())
        {
            Console.WriteLine("Reservations:");
            foreach (var reservation in customer.Reservations)
            {
                Console.WriteLine($"- Reservation ID: {reservation.Id}, Book: {reservation.Book.Title}, Date: {reservation.ReservationDate}");
            }
        }
        else
        {
            Console.WriteLine("No reservations found.");
        }
    }


    public void DisplayAllBooks()
    {
        var books = _context.Books.Include(b => b.Author).Include(b => b.Publisher).ToList();

        if (books.Count == 0)
        {
            Console.WriteLine("No books available.");
            return;
        }

        foreach (var book in books)
        {
            if (book.PreviousBook != null)
            {
                Console.WriteLine($"ID: {book.Id}, Title: {book.Title}, Author: {book.Author.FullName}, Genre: {book.Genre}, Year: {book.Year}, Pages: {book.PageCount}, Price for sale: {book.SalePrice}, Price for buy: {book.CostPrice}, Publisher: {book.Publisher.Name}, Stock: {book.StockCount}, Previous book: {book.PreviousBook}");
            }
            else 
            { 
                Console.WriteLine($"ID: {book.Id}, Title: {book.Title}, Author: {book.Author.FullName}, Genre: {book.Genre}, Year: {book.Year}, Pages: {book.PageCount}, Price for sale: {book.SalePrice}, Price for buy: {book.CostPrice}, Publisher: {book.Publisher.Name}, Stock: {book.StockCount}");
            }
        }
    }
}