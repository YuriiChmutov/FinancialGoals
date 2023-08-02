using AutoMapper;
using FinancialGoals.Core.DTOs.Transaction;
using FinancialGoals.Core.Models;

namespace FinancialGoals.Data.Profiles;

public class TransactionProfile : Profile
{
    public TransactionProfile()
    {
        CreateMap<Transaction, Core.DTOs.Transaction.TransactionToReturn>().ReverseMap();
        CreateMap<TransactionToCreate, Transaction>();
        CreateMap<TransactionToReturn, TransactionsDataDTO>()
            .ForMember(dest => dest.Transactions, opt => opt.MapFrom(x => x));
    }
}