using EF_FinalWork_library.Entities;
using Microsoft.EntityFrameworkCore;

public static class ModelBuilderExtensions
{
    public static void SeedInitialData(this ModelBuilder modelBuilder)
    {

        var authors = new List<Author>
        {
            new Author { Id = 1, FullName = "J.K. Rowling" },
            new Author { Id = 2, FullName = "George Orwell" },
            new Author { Id = 3, FullName = "J.R.R. Tolkien" }
        };

        var publishers = new List<Publisher>
        {
            new Publisher { Id = 1, Name = "Scholastic" },
            new Publisher { Id = 2, Name = "Harcourt" },
            new Publisher { Id = 3, Name = "Penguin" }
        };


        var books = new List<Book>
        {
            new Book { Id = 1, Title = "Harry Potter and the Sorcerer's Stone", AuthorId = 1, PublisherId = 1, PageCount = 309, Genre = "Fantasy", Year = 1997, CostPrice = 10.5, SalePrice = 20.0 },
            new Book { Id = 2, Title = "1984", AuthorId = 2, PublisherId = 2, PageCount = 328, Genre = "Dystopian", Year = 1949, CostPrice = 5.0, SalePrice = 12.0 },
            new Book { Id = 3, Title = "The Hobbit", AuthorId = 3, PublisherId = 3, PageCount = 310, Genre = "Fantasy", Year = 1937, CostPrice = 8.0, SalePrice = 18.0 }
        };


        modelBuilder.Entity<Author>().HasData(authors);
        modelBuilder.Entity<Publisher>().HasData(publishers);
        modelBuilder.Entity<Book>().HasData(books);


        var reservations = new List<Reservation>
        {
            new Reservation { Id = 1, CustomerId = 1, BookId = 1, ReservedUntil = DateTime.UtcNow.AddDays(7) },
            new Reservation { Id = 2, CustomerId = 2, BookId = 2, ReservedUntil = DateTime.UtcNow.AddDays(10) }
        };

        modelBuilder.Entity<Reservation>().HasData(reservations);


        var sales = new List<Sale>
        {
            new Sale { Id = 1, BookId = 1, SaleDate = DateTime.UtcNow, Quantity = 2, TotalPrice = 40.0m },
            new Sale { Id = 2, BookId = 2, SaleDate = DateTime.UtcNow, Quantity = 1, TotalPrice = 12.0m }
        };

        modelBuilder.Entity<Sale>().HasData(sales);
    }
}
