namespace FinancialGoals.Core.DTOs.Transaction;

public class ExpensesPerMonthByCategoryDTO
{
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public decimal Amount { get; set; }
    public string Color { get; set; }
}