using FinancialGoals.Core.DTOs.Category;
using FinancialGoals.Services;

namespace FinancialGoals.Core.DTOs.Account;

public class AccountToReturn
{
    public int FinancialAccountId { get; set; }
    public string Name { get; set; }
    public double Balance { get; set; }
    public CurrencyInfo CurrencyInfo { get; set; }
    public List<CategoryToReturn> Categories { get; set; }
}