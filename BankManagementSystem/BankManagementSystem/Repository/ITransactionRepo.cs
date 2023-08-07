

using BankManagementSystem.DAL;
using BankManagementSystem.Model;

namespace BankManagementSystem.Repository
{
    public interface ITransactionRepo
    {

        int GetNextTransactionID();
         void TransferMoney(long sourceAccountId, long destinationAccountId, float transferAmount,IAccountRepo accountRepo);

         List<Transaction> TransactionSummary(long accountId, DateTime sd, DateTime ed);
         void AddTransaction(long accountnumber, float amount, string remark, TransactionType t,bool isTransfer = false);

         void UpdateAccountType(long accountNumber, AccountType accountType);

         void UpdateTransactionType(int transactionId, TransactionType transactionType);
         void UpdateTransactionAmount(int transactionId, float amount);

         void DeleteTransaction(int transactionId);

    }
}
