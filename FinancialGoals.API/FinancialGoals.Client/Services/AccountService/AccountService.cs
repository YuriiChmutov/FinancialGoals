using System.Net.Http.Json;
using FinancialGoals.Core.DTOs.Account;

namespace FinancialGoals.Client.Services.AccountService;

public class AccountService : IAccountService
{
    private readonly HttpClient _http;

    public AccountService(HttpClient http)
    {
        _http = http;
    }

    public event Action OnChange;
    public int CurrentAccountId { get; set; }
    public List<AccountToReturn> Accounts { get; set; }
    
    public async Task GetAccounts()
    {
        Accounts = await _http.GetFromJsonAsync<List<AccountToReturn>>("https://localhost:7128/api/Accounts");
    }
}