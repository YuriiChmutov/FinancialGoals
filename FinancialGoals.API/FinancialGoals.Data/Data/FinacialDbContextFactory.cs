using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FinancialGoals.Data.Data
{
    internal class FinacialDbContextFactory : IDesignTimeDbContextFactory<FinancialDbContext>
    {
        public FinancialDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<FinancialDbContext>();
            optionsBuilder.UseSqlServer(
                "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = FinancialGoals");

            return new FinancialDbContext(optionsBuilder.Options);
        }
    }
}
