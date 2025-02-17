using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace AviationDb
{
    public class AviationDbContext : DbContext
    {
        public DbSet<Airplane> Airplanes { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Location> Locations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=AviationDb;Trusted_Connection=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airplane>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Model).IsRequired().HasMaxLength(100);
                entity.Property(a => a.Type).IsRequired().HasMaxLength(50);
                entity.Property(a => a.MaxPassengers).IsRequired();
                entity.Property(a => a.Country).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Flight>(entity =>
            {
                entity.HasKey(f => f.Id);
                entity.Property(f => f.FlightNumber).IsRequired().HasMaxLength(50);
                entity.HasOne(f => f.Airplane).WithMany().HasForeignKey(f => f.AirplaneId);
                entity.HasOne(f => f.DepartureLocation).WithMany().HasForeignKey(f => f.DepartureLocationId);
                entity.HasOne(f => f.ArrivalLocation).WithMany().HasForeignKey(f => f.ArrivalLocationId);
                entity.HasMany(f => f.Clients).WithMany(c => c.Flights);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(c => c.LastName).IsRequired().HasMaxLength(100);
                entity.Property(c => c.Email).IsRequired().HasMaxLength(200);
                entity.Property(c => c.Gender).IsRequired();
                entity.HasOne(c => c.Account).WithOne(a => a.Client).HasForeignKey<Account>(a => a.ClientId);
            });

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Username).IsRequired().HasMaxLength(100);
                entity.Property(a => a.Password).IsRequired().HasMaxLength(100);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasKey(l => l.Id);
                entity.Property(l => l.Name).IsRequired().HasMaxLength(200);
                entity.Property(l => l.Country).IsRequired().HasMaxLength(100);
            });
        }
    }

    public class Airplane
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public int MaxPassengers { get; set; }
        public string Country { get; set; }
    }

    public class Flight
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; }
        public int AirplaneId { get; set; }
        public Airplane Airplane { get; set; }
        public int DepartureLocationId { get; set; }
        public Location DepartureLocation { get; set; }
        public int ArrivalLocationId { get; set; }
        public Location ArrivalLocation { get; set; }
        public List<Client> Clients { get; set; } = new();
    }

    public class Client
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }
        public List<Flight> Flights { get; set; } = new();
    }

    public class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }

    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
    }
}
