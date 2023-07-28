namespace FinancialGoals.Services;

public class CurrencyInfo
{
    public int CurrencyId { get; set; }
    public string Currency { get; set; }
    public string CurrencySymbol { get; set; }
    
    public CurrencyInfo(CurrencyType currencyType)
    {
        switch (currencyType)
        {
            case CurrencyType.UAH:
                CurrencyId = (int) CurrencyType.UAH;
                Currency = ((CurrencyType) CurrencyType.UAH).ToString();
                CurrencySymbol = "₴";
                break;
            case CurrencyType.USD:
                CurrencyId = (int) CurrencyType.USD;
                Currency = ((CurrencyType) CurrencyType.USD).ToString();
                CurrencySymbol = "$";
                break;
            case CurrencyType.EUR:
                CurrencyId = (int) CurrencyType.EUR;
                Currency = ((CurrencyType) CurrencyType.EUR).ToString();
                CurrencySymbol = "€";
                break;
        }
    }
}