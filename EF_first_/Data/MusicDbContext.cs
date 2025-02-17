using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_first_.Data;

public class MusicDbContext : DbContext 
{
    public MusicDbContext() {}
    public DbSet<Actor> Actors { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Album> Albums { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Track> Tracks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=StoreDb_2022;Integrated Security=True;Connect Timeout=30");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.SeedData();
    }


}

public class Actor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public int CountryId { get; set; }
    public Country Country { get; set; }

}
public class Country
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Actor> Actors { get; set;}
}

public class Album
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Year { get; set;}
    public int GenreId { get; set; }
    public Genre Genre { get; set; }
    public ICollection<Track> Tracks { get; set;}
}
public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Album> Albums { get; set; }
}
public class Track
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int AlbumId { get; set; }
    public Album Album { get; set; }
    public double Duration { get; set; }
}

