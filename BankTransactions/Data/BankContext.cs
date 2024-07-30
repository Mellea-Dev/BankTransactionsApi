using BankTransactions.Models;
using Microsoft.EntityFrameworkCore;

namespace BankTransactions.Data
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options) : base(options)
        {
            
        }
        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<AccountModel> Accounts { get; set; }
        public DbSet<TransactionModel> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Define composite primary key for Customer
            modelBuilder.Entity<CustomerModel>()
                .HasKey(a => a.CustomerID);

            //Setting Customer to auto increment
            modelBuilder.Entity<CustomerModel>()
               .Property(a => a.CustomerID)
               .ValueGeneratedOnAdd();

            // Define composite primary key for AccountModel
            modelBuilder.Entity<AccountModel>()
                .HasKey(a => a.AccountID);

            //Setting AccountID to auto increment
            modelBuilder.Entity<AccountModel>()
               .Property(a => a.AccountID)
               .ValueGeneratedOnAdd();

            // Configure the one-to-many relationship
            modelBuilder.Entity<AccountModel>()
                .HasOne(a => a.Customer)
                .WithMany(c => c.Accounts)
                .HasForeignKey(a => a.CustomerID);
        }
    }
}
