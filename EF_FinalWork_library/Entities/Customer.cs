using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_FinalWork_library.Entities;
public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
