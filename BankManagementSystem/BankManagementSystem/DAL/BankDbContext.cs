using Microsoft.EntityFrameworkCore;

namespace BankManagementSystem.DAL
{
    public class BankDbContext : DbContext
    {
        public BankDbContext(DbContextOptions<BankDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Transaction> Transactions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base.OnModelCreating(modelBuilder);
            //ADD DEFAULLT VALUE TO COLUMN
            /* modelBuilder.Entity<Transaction>().Property(t => t.CreatedOn).HasDefaultValueSql("getdate()");
             modelBuilder.Entity<Account>()
           .HasOne<Customer>()
           .WithMany().HasForeignKey(a => a.Customer_id)
           .OnDelete(DeleteBehavior.Cascade);*/

            modelBuilder.Entity<Account>().Property(A => A.CreatedOn)
        .HasDefaultValueSql("GETDATE()");


            modelBuilder.Entity<Transaction>()
              .Property(T => T.CreatedOn)
              .HasDefaultValueSql("format(GETDATE(),'dd/MM/yyyy hh:mm:ss')");

            modelBuilder.Entity<Account>()
              .HasOne<Customer>()
              .WithMany().HasForeignKey(a => a.Customer_id)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Transaction>().HasOne<Account>()
              .WithMany().HasForeignKey(t => t.Account_number)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Transaction>().HasKey(t => new { t.Transaction_Id, t.Account_number });
        }


    }
}
