using FinancialGoals.Core.DTOs.User;
using FinancialGoals.Services;

namespace FinancialGoals.Client.Services.AuthService;

public interface IAuthService
{
    Task<ServiceResponse<int>> Register(UserRegister request);
}