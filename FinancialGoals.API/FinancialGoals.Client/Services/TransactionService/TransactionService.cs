using System.Net;
using System.Net.Http.Json;
using FinancialGoals.Core.DTOs.Transaction;

namespace FinancialGoals.Client.Services.TransactionService;

public class TransactionService : ITransactionService
{
    private readonly HttpClient _http;

    public TransactionService(HttpClient http)
    {
        _http = http;
    }

    public event Action? OnChange;
    public int CurrentPage { get; set; } = 1;
    public int PageCount { get; set; } = 0;

    public List<TransactionToReturn> Transactions { get; set; } = new List<TransactionToReturn>();
    
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

    public Task<TransactionToReturn> GetTransaction(int transactionId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> AddTransaction(TransactionToCreate transaction)
    {
        var response = await _http.PostAsJsonAsync($"https://localhost:7128/api/Transactions", transaction);
        return response.StatusCode == HttpStatusCode.OK ? true : false;
    }
}