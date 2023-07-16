using AutoMapper;
using FinancialGoals.Core.DTOs.Category;
using FinancialGoals.Core.Models;
using FinancialGoals.Data.Repository.TransactionService;

namespace FinancialGoals.Data.Resolvers;

public class CategoryAmountResolver : IValueResolver<Category, CategoryToReturn, decimal>
{
    private readonly ITransactionService _transactionService;

    public CategoryAmountResolver(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }
    
    public decimal Resolve(Category source, CategoryToReturn destination, decimal destMember, ResolutionContext context)
    {
        return _transactionService.GetSpendAmountByCategory(source.CategoryId);
    }
}
