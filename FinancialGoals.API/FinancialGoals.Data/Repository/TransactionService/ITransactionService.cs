﻿using FinancialGoals.Core.Models;

namespace FinancialGoals.Data.Repository.TransactionService;

public interface ITransactionService
{
    Task<List<Transaction>> GetTransactionsByAccountIdAsync(int accountId);
    Task<List<Transaction>> GetTransactionsByDateAsync(int accountId, DateTime dateStart, DateTime? dateEnd = null);
    Task<bool> TransactionExistsAsync(int id);
    Task<Transaction?> GetTransactionAsync(int id);
    Task AddTransactionAsync(Transaction transaction);
    Task UpdateTransactionAsync(int id, Transaction transaction);
    Task DeleteTransactionAsync(int id);
    decimal GetSpendAmountByCategory(int categoryId);
}