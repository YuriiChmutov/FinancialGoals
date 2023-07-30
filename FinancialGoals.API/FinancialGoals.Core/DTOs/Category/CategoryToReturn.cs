namespace FinancialGoals.Core.DTOs.Category;

public class CategoryToReturn
{
    public int CategoryId { get; set; }
    public string Name { get; set; }
    public decimal Amount { get; set; } = 0;
    public decimal Limit { get; set; } = 0;
    public ImageData Image { get; set; }
    public List<int> FinancialAccountIds { get; set; }
}