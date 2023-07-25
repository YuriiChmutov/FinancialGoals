namespace FinancialGoals.Core.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public decimal Limit { get; set; }
        public string? ImageName { get; set; }
        public List<Transaction>? Transactions { get; set; }
        public List<User> Users { get; set; }

        public Category()
        {
            Users = new List<User>();
        }
    }
}
