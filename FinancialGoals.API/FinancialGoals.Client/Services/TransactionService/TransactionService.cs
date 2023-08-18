using System.Net;
using System.Net.Http.Json;
using FinancialGoals.Client.Services.AccountService;
using FinancialGoals.Core.DTOs.Transaction;

namespace FinancialGoals.Client.Services.TransactionService;

public class TransactionService : ITransactionService
{
    private readonly HttpClient _http;
    private readonly IAccountService _accountService;

    public TransactionService(HttpClient http, IAccountService accountService)
    {
        _http = http;
        _accountService = accountService;
    }

    public event Action? OnChange;
    public int CurrentPage { get; set; } = 1;
    public int PageCount { get; set; } = 0;

    public List<TransactionToReturn> Transactions { get; set; } = new List<TransactionToReturn>();

    public Dictionary<int, TransactionsDataDTO> TransactionsForAccounts { get; set; } =
        new Dictionary<int, TransactionsDataDTO>();
    
    public async Task GetTransactions(int page)
    {
        var result = await _http.GetFromJsonAsync<TransactionsDataDTO>($"https://localhost:7128/api/Transactions/user-transactions/{page}");
        if (result is {Transactions: not null})
        {
            Transactions = result.Transactions;
            CurrentPage = result.CurrentPage;
            PageCount = result.Pages;
        }
    }
    
    public async Task GetTransactionsForAccount(int accountId, int page)
    {
        if (_accountService.Accounts == null)
        {
            await _accountService.GetAccounts();
        }
        
        var result = await _http.GetFromJsonAsync<TransactionsDataDTO>($"https://localhost:7128/api/Transactions/user-transactions/{accountId}/{page}");

        if (result is {Transactions: not null})
        {
            Transactions = result.Transactions;
            CurrentPage = result.CurrentPage;
            PageCount = result.Pages;
        }
    }

    public Task<TransactionToReturn> GetTransaction(int transactionId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> AddTransaction(TransactionToCreate transaction)
    {
        var response = await _http.PostAsJsonAsync($"https://localhost:7128/api/Transactions", transaction);
        return response.StatusCode == HttpStatusCode.OK ? true : false;
    }

    public async Task<bool> UpdateTransaction(TransactionToUpdate transaction)
    {
        var response = await _http.PutAsJsonAsync($"https://localhost:7128/api/Transactions/{_accountService.CurrentAccountId}/{transaction.TransactionId}", transaction);
        return response.StatusCode == HttpStatusCode.OK ? true : false;
    }
}