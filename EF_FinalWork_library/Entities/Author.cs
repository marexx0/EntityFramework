using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_FinalWork_library.Entities;

public class Author
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public ICollection<Book> Books { get; set; } = new List<Book>();
}