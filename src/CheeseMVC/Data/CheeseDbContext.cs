using CheeseMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CheeseMVC.Data
{
    public class CheeseDbContext : DbContext
    {
        public DbSet<Cheese> Cheeses { get; set; }
        //do not need to create a new dbContext because they are both related to each other
        //creates a separate table to reference categories
        public DbSet<CheeseCategory> Categories { get; set; }

        public CheeseDbContext(DbContextOptions<CheeseDbContext> options)
            : base(options)
        { }

    }
}
