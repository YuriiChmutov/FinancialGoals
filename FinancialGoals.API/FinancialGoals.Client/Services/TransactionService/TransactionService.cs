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
    public TransactionsDataDTO TransactionsData { get; set; } = new TransactionsDataDTO();
    
    public async Task GetTransactions(int page)
    {
        TransactionsData = await _http.GetFromJsonAsync<TransactionsDataDTO>($"https://localhost:7128/api/Transactions/user-transactions/{page}");
    }

    public Task<TransactionToReturn> GetTransaction(int transactionId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> AddTransaction(TransactionToCreate transaction)
    {
        var response = await _http.PostAsJsonAsync($"https://localhost:7128/api/Transactions", transaction);
        // OnChange.Invoke();
        return response.StatusCode == HttpStatusCode.OK ? true : false;
    }
}