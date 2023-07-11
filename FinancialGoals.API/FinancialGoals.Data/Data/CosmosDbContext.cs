using FinancialGoals.Core.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;

namespace FinancialGoals.Data.Data
{
    public class CosmosDbContext : DbContext
    {
        public CosmosDbContext(DbContextOptions<CosmosDbContext> options) : base(options)
        {
        }

        public DbSet<CosmosGoal> Goals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CosmosGoal>(entity =>
            {
                entity.HasKey(e => e.GoalId);
                entity.Property(e => e.GoalId).ValueGeneratedOnAdd();
                entity.Property(e => e.Name).IsRequired();
                
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
