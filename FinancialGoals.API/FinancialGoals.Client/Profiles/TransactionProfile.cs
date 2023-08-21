using AutoMapper;
using FinancialGoals.Core.DTOs.Transaction;

namespace FinancialGoals.Client.Profiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<TransactionToReturn, TransactionToReturn>();

            CreateMap<TransactionToUpdate, TransactionToReturn>();
        }
    }
}
