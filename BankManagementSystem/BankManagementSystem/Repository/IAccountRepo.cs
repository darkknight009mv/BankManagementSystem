using Azure;
using BankManagementSystem.DAL;
using BankManagementSystem.Model;
using Microsoft.AspNetCore.JsonPatch;

namespace BankManagementSystem.Repository
{
    public interface IAccountRepo
    {
         void AddAccount(int cmid,AccountModel account, ITransactionRepo transactionRepo);
        void RemoveAccount(long accountNumber, AccountType type,ITransactionRepo transactionRepo);
         List<GetAllAccModel> GetAllAccounts();

         void DepositInAccount(AccountType act, long accountId, float depositAmount,  bool isTransfer);

         void WithdrawFromAccount(AccountType act,long accountId, float withdrawalAmount, bool isTransfer);





    }
}
