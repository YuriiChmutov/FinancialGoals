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
            }
            //string endpointUri = "https://localhost:8081";
            //string primaryKey = "pkn9yTHdZ4TgatHoWlGj5c3HjVa7jirSVPfuMbF63e4HpR2HpTM3UmPu5TJ8ILJiUMLX4jdXGdtyACDb4CS5ww==";
            //string databaseName = "FinancialGoals";

            //optionsBuilder.UseCosmos(
            //    endpointUri,
            //    primaryKey,
            //    databaseName,
            //    cosmosOptionsAction: options =>
            //    {
            //        options.ConnectionMode(Microsoft.Azure.Cosmos.ConnectionMode.Direct);
            //        options.MaxRequestsPerTcpConnection(20);
            //        options.MaxTcpConnectionsPerEndpoint(32);
            //    }
            //);
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.HasDefaultContainer("AllInOne");
        //    modelBuilder.HasManualThroughput(600);

        //    modelBuilder.Entity<User>()
        //        .ToContainer(nameof(Users))
        //        .HasNoDiscriminator()
        //        .HasDefaultTimeToLive(60)
        //        .HasPartitionKey(x => x.Email)
        //        .HasKey(x => x.UserId);
        //}
    }
}
