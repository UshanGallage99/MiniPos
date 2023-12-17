using Microsoft.EntityFrameworkCore;
using MiniPos.Models;

namespace MiniPos.Data
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Town> Town { get; set; }
        public DbSet<ContactPerson> ContactPerson { get; set; }

        public DbSet<Products> Products { get; set; }
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<InvoiceLine> InvoiceLine { get; set; }
    }
}
