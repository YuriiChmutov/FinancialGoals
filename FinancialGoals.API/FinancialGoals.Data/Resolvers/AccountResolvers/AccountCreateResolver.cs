using AutoMapper;
using AutoMapper.Execution;
using FinancialGoals.Core.DTOs.Account;
using FinancialGoals.Core.Models;
using FinancialGoals.Data.Repository.AccountService;
using FinancialGoals.Services;

namespace FinancialGoals.Data.Resolvers.AccountResolvers;

public class AccountCreateResolver : ITypeConverter<AccountToCreate, FinancialAccount>
{
    private readonly IAccountService _accountService;

    public AccountCreateResolver(IAccountService accountService)
    {
        _accountService = accountService;
    }

    public FinancialAccount Convert(AccountToCreate source, FinancialAccount destination, ResolutionContext context)
    {
        var nextNumberForAccountName = _accountService.GetAmountOfUsersCurrencyAccounts((int) source.UserId, source.CurrencyType).Result;
        return new FinancialAccount
        {
            Balance = 0,
            Currency = source.CurrencyType,
            Name = $"AccUser{source.UserId}_{((CurrencyType) source.CurrencyType).ToString()}_{nextNumberForAccountName}", //todo: make a limit-control for amount of accounts
            UserId = (int) source.UserId
        };
    }
}