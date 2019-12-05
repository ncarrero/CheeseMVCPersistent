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
        public DbSet<Menu> Menus { get; set; }
        public DbSet<CheeseMenu> CheeseMenus { get; set; }

        //method to set primary key of CheeseMenu class and table to be a composite key consisting of both CheeseID and MenuID
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CheeseMenu>()
                .HasKey(c => new { c.CheeseID, c.MenuID });
        }

        public CheeseDbContext(DbContextOptions<CheeseDbContext> options)
            : base(options)
        { }

    }
}
