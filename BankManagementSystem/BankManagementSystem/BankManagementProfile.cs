using AutoMapper;
using BankManagementSystem.DAL;
using BankManagementSystem.Model;

namespace BankManagementSystem
{
    public class BankManagementProfile : Profile
    {
        public BankManagementProfile() {

            CreateMap<AccountModel, Account>().ReverseMap();
            CreateMap<CustomerModel, Customer>().ReverseMap();
            CreateMap<TransactionModel, Transaction>().ReverseMap();


            CreateMap<Account, GetAllAccModel>().ForMember(dest => dest.InterestRate,
                src => src.MapFrom(s => s.Account_Type == AccountType.Savings ? 5 : 2));

            CreateMap<Customer, GetAllCustomerModel>().ReverseMap();
            CreateMap<Transaction,GetTransactionSummary>().ReverseMap();
        }

       
    }
}
