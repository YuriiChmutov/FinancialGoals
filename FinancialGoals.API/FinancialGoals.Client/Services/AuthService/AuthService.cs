using System.Net.Http.Json;
using FinancialGoals.Core.DTOs.User;
using FinancialGoals.Services;

namespace FinancialGoals.Client.Services.AuthService;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ServiceResponse<int>> Register(UserRegister request)
    {
        var result = await _httpClient.PostAsJsonAsync("https://localhost:7128/api/users/register", request);
        return await result.Content.ReadFromJsonAsync<ServiceResponse<int>>();
    }
}