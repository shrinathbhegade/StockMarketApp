using Microsoft.EntityFrameworkCore;

namespace InventoryApp.Models
{
    public class StockDbContext : DbContext
    {
        public StockDbContext(DbContextOptions<StockDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        //write onmodelcreating method to set up relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.UserId);
                entity.Property(u => u.UserName).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
                entity.Property(u => u.PasswordHash).IsRequired();
                entity.Property(u => u.Balance).HasColumnType("decimal(18,2)");
            });

            //add model builder for Stock
            modelBuilder.Entity<Stock>(entity =>
            {
                entity.HasKey(s => s.StockId);
                entity.Property(s => s.StockName).IsRequired().HasMaxLength(50);
                entity.Property(s => s.Category).IsRequired().HasMaxLength(100);
                entity.Property(s => s.CurrentPrice).HasColumnType("decimal(18,2)");
                entity.Property(s => s.Actualprice).HasColumnType("decimal(18,2)");
            });

            //add model builder for Transaction
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(t => t.TransactionId);
                entity.Property(t => t.Quantity).HasColumnType("decimal(18,2)");
                entity.Property(t => t.PriceAtTransaction).HasColumnType("decimal(18,2)");

                //User to Transaction WithMany relationship
                entity.HasOne(t => t.User)
                .WithMany(u => u.Transactions)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

                //Stock to Transaction WithMany relationship
                entity.HasOne(t => t.Stock).WithMany(s => s.Transactions).HasForeignKey(t => t.StockId).OnDelete(DeleteBehavior.Cascade);

                //ToDo: Add sample data in User with HasData
                modelBuilder.Entity<User>().HasData(new User { Balance= 5000, Email= "johndoe@gmail.com", PasswordHash= "123456", UserId= 1, UserName= "John Doe" });
                modelBuilder.Entity<User>().HasData(new User { Balance = 25000, Email = "janedoe@gmailc.om", PasswordHash = "abcdef", UserId = 2, UserName = "Jane Doe" });

                modelBuilder.Entity<Stock>().HasData(new Stock { Actualprice = 100, Category = "Tech", CurrentPrice = 150, StockId = 1, StockName = "Apple Inc." });
                modelBuilder.Entity<Stock>().HasData(new Stock { Actualprice = 200, Category = "Tech", CurrentPrice = 250, StockId = 2, StockName = "Microsoft Corporation" });
                modelBuilder.Entity<Stock>().HasData(new Stock { Actualprice = 300, Category = "Tech", CurrentPrice = 350, StockId = 3, StockName = "Amazon.com Inc." });
                modelBuilder.Entity<Transaction>().HasData(new Transaction { PriceAtTransaction = 150, Quantity = 10, StockId = 1, UserId = 1, TransactionId = 1, BuyDate = DateTime.UtcNow });

            });

        }
    }
}
