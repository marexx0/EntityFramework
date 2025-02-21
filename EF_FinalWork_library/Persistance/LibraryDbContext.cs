using EF_FinalWork_library.Entities;
using Microsoft.EntityFrameworkCore;
using System.Configuration;


namespace Library.Persistance
{
    internal class LibraryDbContext : DbContext
    {

        public DbSet<Book> Books { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var cs = ConfigurationManager.ConnectionStrings["LocalDb"].ConnectionString;
                optionsBuilder.UseSqlServer(cs);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibraryDbContext).Assembly);
            modelBuilder.SeedInitialData();
        }
    }
}
