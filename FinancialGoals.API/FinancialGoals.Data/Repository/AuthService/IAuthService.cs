using FinancialGoals.Core.Models;
using FinancialGoals.Services;

namespace FinancialGoals.Data.Repository.AuthService;

public interface IAuthService
{
    Task<ServiceResponse<string>> Register(User user, string password);
    Task<bool> UserExists(string email);
    Task<ServiceResponse<string>> Login(string email, string password);
    Task<ServiceResponse<bool>> ChangePassword(int userId, string newPassword);
}