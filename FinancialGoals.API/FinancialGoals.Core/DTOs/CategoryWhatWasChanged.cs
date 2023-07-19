namespace FinancialGoals.Core.DTOs;

public class CategoryWhatWasChanged
{
    public bool Name { get; set; } = false;
    public bool Limit { get; set; } = false;
    public bool Image { get; set; } = false;
}