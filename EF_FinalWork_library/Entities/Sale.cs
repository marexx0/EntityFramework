﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_FinalWork_library.Entities;
public class Sale
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public Book Book { get; set; } = null!;
    public DateTime SaleDate { get; set; }
    public int Quantity { get; set; }
    public double TotalPrice { get; set; }
}
