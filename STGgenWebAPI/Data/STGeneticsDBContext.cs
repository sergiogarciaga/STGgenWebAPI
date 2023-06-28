using System.Configuration;
using Microsoft.EntityFrameworkCore;
using STGgenWebAPI.Model;

namespace STGgenWebAPI.Data
{
    public class STGeneticsDBContext : DbContext
    {
        public STGeneticsDBContext()
        {
        }

        public STGeneticsDBContext(DbContextOptions<STGeneticsDBContext> options)
                   : base(options)
        {
        }

        public DbSet<Animal> Animals { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDetail> OrderDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the Animal entity
            modelBuilder.Entity<Animal>(entity =>
            {
                entity.ToTable("Animal");

                // Configure the primary key
                entity.HasKey(e => e.AnimalId);
                // Configure other properties
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Breed).HasMaxLength(100);
                entity.Property(e => e.BirthDate).IsRequired();
                entity.Property(e => e.Sex).IsRequired().HasMaxLength(10);
                entity.Property(e => e.Price).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.Status).IsRequired().HasMaxLength(10);
            });

            // Configure the Order entity
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                // Configure the primary key
                entity.HasKey(e => e.OrderId);

                // Configure other properties
                entity.Property(e => e.TotalPurchaseAmount).IsRequired().HasColumnType("decimal(18,2)");
                entity.Property(e => e.TotalQuantity).IsRequired();
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.ToTable("OrderDetail");

                // Configure the composite primary key
                entity.HasKey(e => new { e.OrderId, e.AnimalId });

                 // Configure relationships if any
                // entity.HasOne(e => e.Animal)
                //       .WithMany(e => e.OrderDetails)
                //       .HasForeignKey(e => e.AnimalId);
                // entity.HasOne(e => e.Order)
                //       .WithMany(e => e.OrderDetails)
                //       .HasForeignKey(e => e.OrderId);
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();
                 
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("STGgenWebAPIContext"));
            }
        }

    }
}
