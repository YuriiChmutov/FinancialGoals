using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FinancialGoals.Data.Data
{
    internal class FinancialDbContextFactory : IDesignTimeDbContextFactory<FinancialDbContext>
    {
        //private readonly string _endpointUri;
        //private readonly string _primaryKey;
        //private readonly string _databaseName;

        //public FinancialDbContextFactory(string endpointUri, string primaryKey, string databaseName)
        //{
        //    _endpointUri = endpointUri;
        //    _primaryKey = primaryKey;
        //    _databaseName = databaseName;
        //}

        public FinancialDbContext CreateDbContext(string[] args)
        {
            string endpointUri = "https://localhost:8081";
            string primaryKey = "pkn9yTHdZ4TgatHoWlGj5c3HjVa7jirSVPfuMbF63e4HpR2HpTM3UmPu5TJ8ILJiUMLX4jdXGdtyACDb4CS5ww==";
            string databaseName = "FinancialGoals";

            var optionsBuilder = new DbContextOptionsBuilder<FinancialDbContext>();
            optionsBuilder.UseSqlServer(
                "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = FinancialGoals");

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

            return new FinancialDbContext(optionsBuilder.Options);
        }
    }


    internal class CosmosDbContextFactory : IDesignTimeDbContextFactory<CosmosDbContext>
    {
        //private readonly string _endpointUri;
        //private readonly string _primaryKey;
        //private readonly string _databaseName;

        //public FinancialDbContextFactory(string endpointUri, string primaryKey, string databaseName)
        //{
        //    _endpointUri = endpointUri;
        //    _primaryKey = primaryKey;
        //    _databaseName = databaseName;
        //}

        public CosmosDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CosmosDbContext>();
            optionsBuilder.UseCosmos(
                "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
                "Goals"
            );

            return new CosmosDbContext(optionsBuilder.Options);
        }
    }
}
