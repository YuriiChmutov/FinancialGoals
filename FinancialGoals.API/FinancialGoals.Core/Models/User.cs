using System.Text.Json.Serialization;
using FinancialGoals.Services;

namespace FinancialGoals.Core.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public List<FinancialAccount>? FinancialAccounts { get; set;}
        public UserRole Role { get; set; }
        public DateTime? BirthDate { get; set; }
        public UserGender? Gender { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
