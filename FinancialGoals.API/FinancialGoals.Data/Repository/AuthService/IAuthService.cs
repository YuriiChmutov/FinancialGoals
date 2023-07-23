using FinancialGoals.Core.Models;
using FinancialGoals.Services;

namespace FinancialGoals.Data.Repository.AuthService;

public interface IAuthService
{
    Task<ServiceResponse<int>> Register(User user, string password);
    Task<bool> UserExists(string email);
}