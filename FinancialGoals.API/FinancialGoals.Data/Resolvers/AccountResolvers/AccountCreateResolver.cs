using AutoMapper;
using AutoMapper.Execution;
using FinancialGoals.Core.DTOs.Account;
using FinancialGoals.Core.Models;
using FinancialGoals.Data.Repository.AccountService;
using FinancialGoals.Data.Repository.CategoryService;
using FinancialGoals.Services;

namespace FinancialGoals.Data.Resolvers.AccountResolvers;

public class AccountCreateResolver : ITypeConverter<AccountToCreate, FinancialAccount>
{
    private readonly IAccountService _accountService;
    private readonly ICategoryService _categoryService;

    public AccountCreateResolver(IAccountService accountService, ICategoryService categoryService)
    {
        _accountService = accountService;
        _categoryService = categoryService;
    }

    public FinancialAccount Convert(AccountToCreate source, FinancialAccount destination, ResolutionContext context)
    {
        var nextNumberForAccountName = _accountService.GetAmountOfUsersCurrencyAccounts((int) source.UserId, source.CurrencyType).Result + 1;
        var defaultCategories = _categoryService.GetDefaultCategories().Result;
        
        return new FinancialAccount
        {
            Balance = 0,
            Currency = source.CurrencyType,
            Name = $"AccUser{source.UserId}_{((CurrencyType) source.CurrencyType).ToString()}_{nextNumberForAccountName}", //todo: make a limit-control for amount of accounts
            Number = nextNumberForAccountName,
            UserId = (int) source.UserId
        };
    }
}