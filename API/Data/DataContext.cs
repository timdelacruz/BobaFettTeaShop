using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customer { get; set; }
        public DbSet<Flavor> Flavor { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<ItemToppings> ItemToppings { get; set; }
        public DbSet<Size> Size { get; set; }
        public DbSet<Tea> Tea { get; set; }
        public DbSet<Toppings> Toppings { get; set; }
        public DbSet<Transaction> Transaction { get; set; }


    }
}