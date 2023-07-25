using FinancialGoals.Core.DTOs.User;
using FinancialGoals.Services;

namespace FinancialGoals.Client.Services.AuthService;

public interface IAuthService
{
    Task<ServiceResponse<string>> Register(UserRegister request);
    Task<ServiceResponse<string>> Login(UserLogin request);
    Task<ServiceResponse<bool>> ChangePassword(UserChangePassword request);
}