using FinancialGoals.Core.Models;
using FinancialGoals.Services;
using Microsoft.EntityFrameworkCore;

namespace FinancialGoals.Data.Data
{
    public class FinancialDbContext : DbContext
    {
        public FinancialDbContext(
            DbContextOptions<FinancialDbContext> options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<FinancialAccount> FinancialAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Goal> Goals { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = FinancialGoals");
                // optionsBuilder.UseSqlServer("Data Source = DESKTOP-9N3RA63\\SQLEXPRESS; Initial Catalog = FinancialGoalsDB; User Id = Test; Password=1234");
            }
        }
    }
}
