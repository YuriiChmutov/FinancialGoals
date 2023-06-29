using System.Text.Json.Serialization;

namespace FinancialGoals.Core.DTOs
{
    public class LoginModel
    {
        [JsonIgnore]
        public string? FirstName { get; set; }
        [JsonIgnore]
        public string? SecondName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberLogin { get; set; }
    }
}
