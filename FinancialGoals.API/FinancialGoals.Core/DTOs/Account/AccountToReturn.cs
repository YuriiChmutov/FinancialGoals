namespace FinancialGoals.Core.DTOs.Account;

public class AccountToReturn
{
    public int FinancialAccountId { get; set; }
    public string Name { get; set; }
    public double Balance { get; set; }
    // public CurrencyType Currency { get; set; }
}