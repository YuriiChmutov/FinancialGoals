using FinancialGoals.Services;

namespace FinancialGoals.Core.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Password { get; set; }
        public List<FinancialAccount>? FinancialAccounts { get; set;}
        public UserRole Role { get; set; }
    }
}
