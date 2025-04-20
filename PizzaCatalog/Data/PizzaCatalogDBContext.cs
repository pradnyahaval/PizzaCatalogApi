using Microsoft.EntityFrameworkCore;
using PizzaCatalog.WebApi.Model.Domain;

namespace PizzaCatalog.WebApi.Data
{
    public class PizzaCatalogDBContext : DbContext
    {
        public PizzaCatalogDBContext(DbContextOptions<PizzaCatalogDBContext> dbContextOptions) : base(dbContextOptions) { }
   

        public DbSet<Pizzas> Pizzas { get; set; }
        public DbSet<Toppings> Toppings { get; set; }
        public DbSet<Sizes> Sizes { get; set; }
        public DbSet<PizzaToppings> PizzaToppings { get; set; }
        public DbSet<PizzaImages> PizzaImages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Pizzas>()
            .Property(t => t.BasePrice)
            .HasPrecision(10, 2);

            modelBuilder.Entity<Toppings>()
            .Property(t => t.Price)
            .HasPrecision(10, 2);

            modelBuilder.Entity<Sizes>()
            .Property(t => t.PriceMultiplier)
            .HasPrecision(10, 2);


            modelBuilder.Entity<PizzaToppings>()
                .HasOne(p => p.Pizzas)
                .WithMany(pt => pt.PizzaToppings)
                .HasForeignKey(p => p.PizzId);

            modelBuilder.Entity<PizzaToppings>()
                .HasOne(t => t.Toppings)
                .WithMany(pt => pt.PizzaToppings)
                .HasForeignKey(t => t.ToppingId);

            modelBuilder.Entity<Pizzas>()
                .HasOne(p => p.PizzaImages)
                .WithOne(pi => pi.Pizzas)
                .HasForeignKey<PizzaImages>(pi => pi.PizzaId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
