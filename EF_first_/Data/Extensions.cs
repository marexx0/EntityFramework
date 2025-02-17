using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_first_.Data;
public static class Extensions
{
    public static void SeedData(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>().HasData(new Country[]
        {
            new() { Id = 1, Name = "USA" },
            new() { Id = 2, Name = "UK" },
            new() { Id = 3, Name = "Ukraine" },
        });
        modelBuilder.Entity<Genre>().HasData(new Genre[]
        {
            new() { Id = 1, Name = "Pop" },
            new() { Id = 2, Name = "Rock" },
            new() { Id = 3, Name = "Rap" },
        });
        modelBuilder.Entity<Album>().HasData(new Album[]
        {
            new() { Id = 1, Name = "Album 1", Year = 2022, GenreId = 1 },
            new() { Id = 2, Name = "Album 2", Year = 2023, GenreId = 2 },
        });

    }


}
