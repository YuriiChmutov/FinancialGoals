using FinancialGoals.Core.DTOs.Category;
using FinancialGoals.Core.Models;
using FinancialGoals.Services;

namespace FinancialGoals.Core.DTOs.Transaction;

public class TransactionToReturn
{
    public int TransactionId { get; set; }
    public double Amount { get; set; }
    public TransactionType Type { get; set; }
    public DateTime Date { get; set; }
    public string? Description { get; set; }
    public int FinancialAccountId { get; set; }
    public CategoryToReturn CategoryToReturn { get; set; } = new CategoryToReturn();
    public int CategoryId { get; set; }
}