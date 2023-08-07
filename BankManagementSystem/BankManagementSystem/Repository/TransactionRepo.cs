using BankManagementSystem.DAL;
using BankManagementSystem.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace BankManagementSystem.Repository
{
    public class TransactionRepo : ITransactionRepo
    {

        private readonly BankDbContext _dbContext;
        
        

        public TransactionRepo(BankDbContext dbContext)
        {
           
            _dbContext = dbContext;
        }
        public int GetNextTransactionID()
        {
            var lastTransaction = _dbContext.Transactions
                    .OrderByDescending(t => t.Transaction_Id)
                    .FirstOrDefault();

            int transactionID = lastTransaction != null ? lastTransaction.Transaction_Id : 0;

            return transactionID + 1;
        }
        public void AddTransaction(long accountnumber,float amount,string remark,TransactionType t,bool isTransfer = false  )
        { 
            Transaction tr = new Transaction();
            if (isTransfer)
            {
                tr.Transaction_Id = GetNextTransactionID() - 1;  
            }
            else
            {
                tr.Transaction_Id = GetNextTransactionID();
            }
            tr.Account_number = accountnumber;
            tr.Amount = amount;
            tr.Remark = remark;
            tr.Type = t;
            _dbContext.Transactions.Add(tr);
            _dbContext.SaveChanges();
        }
       

        public List<Transaction> TransactionSummary(long accountId,DateTime sd,DateTime ed)
        {
          
            return _dbContext.Transactions
                .Where(t => t.Account_number == accountId 
                                    && t.CreatedOn >= sd && t.CreatedOn <= ed).ToList();
;
        }

        public void TransferMoney(long sourceAccountId, long destinationAccountId, float transferAmount,
           IAccountRepo _accountRepo )
        {
            var sourceAccount = _dbContext.Accounts.Find(sourceAccountId);
            var destinationAccount = _dbContext.Accounts.Find(destinationAccountId);

            if (sourceAccount != null && destinationAccount != null)
            {
                if (sourceAccount.Balance >= transferAmount)
                {
                    //AccountType act,long accountId, float withdrawalAmount, ITransactionRepo transactionRepo, bool isTransfer
                    _accountRepo.WithdrawFromAccount(sourceAccount.Account_Type, sourceAccountId,transferAmount,true);
                    _accountRepo.DepositInAccount(destinationAccount.Account_Type, destinationAccountId, transferAmount, true);
                    _dbContext.Accounts.Update(sourceAccount);
                    _dbContext.Accounts.Update(destinationAccount);

                    AddTransaction(sourceAccountId, transferAmount, "RECIVED", TransactionType.RECIVED,true);
                    AddTransaction(destinationAccountId, transferAmount, "SENT", TransactionType.SENT,false);

                    _dbContext.SaveChanges();

                }
                else
                {
                    throw new Exception("Not enough balance available in your account...");
                }
               
            }
            else if(sourceAccount == null)
            {
                throw new Exception("Source account does not exist...");
            }
            else if(destinationAccount == null)
            {
                throw new Exception("Destination account does not exist...");
            }
        }

        public void UpdateAccountType(long accountNumber, AccountType accountType)
        {
           var account = _dbContext.Accounts.ToList().Find(o => o.Account_Number == accountNumber);
            if (account != null)
            {
                account.Account_Type = accountType;
                _dbContext.Accounts.Update(account);
                
                _dbContext.SaveChanges();
            }
            else
            {
                throw new Exception("Account is not found...");
            }
        }

        public void UpdateTransactionAmount(int transactionId, float amount)
        {
          
            var tr = _dbContext.Transactions.ToList().Find(o => o.Transaction_Id == transactionId);
            
            var acc = _dbContext.Accounts.ToList().Find(o => o.Account_Number == tr.Account_number);
            if(acc != null && tr!=null)
            {
                if (tr.Type == TransactionType.CREDIT)
                {
                    if (tr.Amount < amount)
                    {
                        acc.Balance += amount - tr.Amount;
                        tr.Amount = amount;
                    }
                    else
                    {
                        acc.Balance -= tr.Amount - amount;
                        tr.Amount = amount;
                    }
                }
                if (tr.Type == TransactionType.DEBITED)
                {
                    if (tr.Amount < amount)
                    {
                        acc.Balance -= amount - tr.Amount;
                        tr.Amount = amount;
                    }
                    else
                    {
                        acc.Balance += tr.Amount - amount;
                        tr.Amount = amount;
                    }
                }

                _dbContext.Transactions.Update(tr);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new Exception("Please enter valid transaction ID");
            }
           
        }

        public void UpdateTransactionType(int transactionId, TransactionType transactionTypeToUpdate)
        {
            
            var tr = _dbContext.Transactions.ToList().Find(o => o.Transaction_Id == transactionId);
            TimeSpan ts = DateTime.Now - tr.CreatedOn;
            var acc = _dbContext.Accounts.ToList().Find(o => o.Account_Number == tr.Account_number);
            if (tr != null && acc != null && ts.TotalHours<24 ) {
                
                if (transactionTypeToUpdate == TransactionType.CREDIT && 
                    tr.Type == TransactionType.DEBITED)
                {
                    acc.Balance += tr.Amount;
                    tr.Type = TransactionType.CREDIT;
                    _dbContext.Accounts.Update(acc);
                    _dbContext.Transactions.Update(tr);
                    _dbContext.SaveChanges(true);
                }
                else if(transactionTypeToUpdate == TransactionType.DEBITED &&
                    tr.Type == TransactionType.CREDIT)
                {
                    acc.Balance -= tr.Amount;
                    tr.Type = TransactionType.DEBITED;
                    _dbContext.Accounts.Update(acc);
                    _dbContext.Transactions.Update(tr);
                    _dbContext.SaveChanges(true);
                }
                else
                {
                    throw new Exception("Transaction type not supported...");
                }
            }
            else
            {
                throw new Exception("No details found or transaction can't be updated after 24 hours");
            }
        }

        

        public void DeleteTransaction(int transactionId)
        {
            var tr = _dbContext.Transactions.ToList().Find(o => o.Transaction_Id == transactionId);
            if (tr != null)
            {
                _dbContext.Transactions.Remove(tr);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new Exception("Please enter valide transaction ID...");
            }
           
        }
    }
}
