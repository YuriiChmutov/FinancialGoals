namespace FinancialGoals.Services;

public class ServiceResponse<T>
{
    public bool Success { get; set; } = false;
    public string Message { get; set; }
    public T Data { get; set; }
}