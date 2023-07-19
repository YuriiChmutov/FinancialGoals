﻿using System.Net.Http.Json;
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
    public List<TransactionToReturn> Transactions { get; set; }
    
    public async Task GetTransactions(int currentUserId)
    {
        Transactions = await _http.GetFromJsonAsync<List<TransactionToReturn>>("https://localhost:7128/api/Transactions");
    }

    public Task<TransactionToReturn> GetTransaction(int transactionId)
    {
        throw new NotImplementedException();
    }
}