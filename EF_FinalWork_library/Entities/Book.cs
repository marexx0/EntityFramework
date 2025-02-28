using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_FinalWork_library.Entities;
public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int PageCount { get; set; }
    public string Genre { get; set; }
    public int Year { get; set; }
    public int AuthorId { get; set; }
    public Author Author { get; set; } = null!;
    public int PublisherId { get; set; }
    public Publisher Publisher { get; set; } = null!;
    public double CostPrice { get; set; }
    public double SalePrice { get; set; }
    public int StockCount { get; set; }
    public int? PreviousBookId { get; set; }
    public Book? PreviousBook { get; set; }
    public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}

