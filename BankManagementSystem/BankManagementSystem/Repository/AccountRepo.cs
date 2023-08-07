using AutoMapper;
using BankManagementSystem.DAL;
using BankManagementSystem.Model;
using BankManagementSystem.Utils;

namespace BankManagementSystem.Repository
{
    public class AccountRepo : IAccountRepo
    {
        private BankDbContext _dbcontext;
        private Func<AccountType, IAccountUtils> _serviceResolver;
        private IAccountUtils _accountUtils;
        private IMapper _mapper;
        private ITransactionRepo _transactionRepo;

        public AccountRepo(ITransactionRepo transactionRepo,BankDbContext bankContextDB, Func<AccountType, IAccountUtils> serviceResolver, IMapper mapper)
        {
            this._dbcontext = bankContextDB;
            this._serviceResolver = serviceResolver;
            this._mapper = mapper;
            this._transactionRepo = transactionRepo;

        }
        public void AddAccount(int cmid, AccountModel acm, ITransactionRepo transactionRepo)
        {
            
            Customer customers = _dbcontext.Customers.Find(cmid);
            if (customers != null)
            {
                _accountUtils = _serviceResolver(acm.Account_Type);//DI
                
                acm.Account_Type = Enum.IsDefined(acm.Account_Type) ? acm.Account_Type : AccountType.Current;
 
                if (acm.Balance < _accountUtils.GetMinBalanceToBeMaintained())
                {
                    throw new Exception("min balance to create the account is " +
                        _accountUtils.GetMinBalanceToBeMaintained());

                }
                if (acm.Balance > _accountUtils.GetMaxBalanceToDeposit())
                {
                    throw new Exception("MAXLimit to Deposit is" + _accountUtils.GetMaxBalanceToDeposit);

                }

                Account account = _mapper.Map<Account>(acm);
       
                account.Customer_id = cmid;
                _dbcontext.Accounts.Add(account);
                _dbcontext.SaveChanges();
                transactionRepo.AddTransaction(account.Account_Number, acm.Balance, "NEW ACCOUNT CREATED", TransactionType.CREDIT);
            }
            else
            {
                throw new Exception("User does not exist");
            }
        }

        public void DepositInAccount(AccountType act,long accountId, float depositAmount,  bool isTransfer)
        {
            Account account = _dbcontext.Accounts.ToList().Find(a => a.Account_Number == accountId && a.Account_Type == act);
            if (account != null)
            {
                _accountUtils = _serviceResolver(act);

                if (depositAmount == 0)
                {
                    throw new Exception("Amount Can't be Zero for Deposit");
                }
                if (depositAmount > _accountUtils.GetMaxBalanceToDeposit())
                {
                    throw new Exception("MAXLimit to Deposit is" + _accountUtils.GetMaxBalanceToDeposit());
                }
                else
                {
                    account.Balance = account.Balance + depositAmount;
                    _dbcontext.Accounts.Update(account);
                    _dbcontext.SaveChanges();
                    if (!isTransfer)
                    {
                        _transactionRepo.AddTransaction(account.Account_Number, depositAmount, "DEPOSIT", TransactionType.CREDIT);
                    } 
                    

                }
            }
            else
            {
                throw new Exception("NO Account found with the given details");

            }
        }

        public List<GetAllAccModel> GetAllAccounts()
        {

            MapperConfiguration mapperConfiguration=new MapperConfiguration(cfg=>cfg.CreateMap<Account,GetAllAccModel>().ForMember(src=>src.InterestRate,
                opt=>opt.MapFrom(dest=>_serviceResolver(dest.Account_Type).GetInterestRate())));

            var mapper=mapperConfiguration.CreateMapper();
            return mapper.Map<List<GetAllAccModel>>(_dbcontext.Accounts.ToList());
        }

        public void RemoveAccount(long accountNumber, AccountType act,ITransactionRepo transactionRepo)
        {
            Account thisAccount = _dbcontext.Accounts.ToList().Find(a => a.Account_Number == accountNumber && a.Account_Type == act);
            if (thisAccount != null)
            {
                _accountUtils = _serviceResolver(act);
                transactionRepo.AddTransaction(thisAccount.Account_Number, thisAccount.Balance,"ACCOUNT REMOVED", TransactionType.DEBITED);
              

                _dbcontext.Accounts.Remove(thisAccount);

                _dbcontext.SaveChanges();

                if (_dbcontext.Accounts.ToList().FindAll(a => a.Customer_id == thisAccount.Customer_id).Count() == 0)
                {
                    Customer customers = _dbcontext.Customers.Find(thisAccount.Customer_id);
                    _dbcontext.Customers.Remove(customers);
                    _dbcontext.SaveChanges();
                }

            }
            else
            {
                throw new Exception("NO Account found with the given details");
            }
        }

        public void WithdrawFromAccount(AccountType act,long accountId, float withdrawalAmount, bool isTransfer)
        {
            Account account = _dbcontext.Accounts.ToList().Find(a => a.Account_Number == accountId && a.Account_Type == act);
            if (account != null)
            {
                _accountUtils = _serviceResolver(act);//DI

                if (withdrawalAmount > _accountUtils.GetMaxWithDrawalLimit())
                {

                    throw new Exception("MAX Limit to WithDrawal is " +
                        _accountUtils.GetMaxWithDrawalLimit());
                }
                List<Transaction> trListForToday = _dbcontext.Transactions.ToList().FindAll(t => t.Account_number == accountId &&
                                  t.CreatedOn.Date.ToString("dd/MM/yyyy") == DateTime.Today.Date.ToString("dd/MM/yyyy")
                                  && t.Type == TransactionType.DEBITED);
                if (trListForToday.Count() > 2)
                {
                    throw new Exception("Withdrawl  Limit is"
                        + _accountUtils.GetMaxNumberOfWithdrwalPerDay() + "per Day");
                }
                else
                {
                    if ((account.Balance - withdrawalAmount) >= _accountUtils.GetMinBalanceToBeMaintained())
                    {
                        account.Balance -= withdrawalAmount;
                        _dbcontext.Accounts.Update(account);
                        _dbcontext.SaveChanges();

                        if (!isTransfer)
                        {
                            _transactionRepo.AddTransaction(account.Account_Number, withdrawalAmount, "WITHDRAW", TransactionType.DEBITED);
                        }
                        
                    }
                    else
                    {
                        throw new Exception("Amount must maintain min limit of the account");
                    }
                }
            }
            else
            {
                throw new Exception("Account with given details does not exist");
            }
        
    }
    }
}
