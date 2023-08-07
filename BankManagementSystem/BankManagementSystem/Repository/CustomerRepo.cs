using AutoMapper;
using Azure;
using BankManagementSystem.DAL;
using BankManagementSystem.Model;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace BankManagementSystem.Repository
{
    public class CustomerRepo : ICustomerRepo
    {
        private BankDbContext _dbContext;
        private IMapper _mapper;
        private IAccountRepo _accountRepo;
        private ITransactionRepo _transactionRepo;
     
        
        
        public CustomerRepo(BankDbContext dbContext,IMapper mapper,ITransactionRepo transactionRepo,
            IAccountRepo accountRepo)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _transactionRepo = transactionRepo;
            _accountRepo = accountRepo;
           
        }

        public void AddCustomer(CustomerModel customer, IAccountRepo accountRepo, ITransactionRepo transactionRepo)
        { 
            Customer cs = _mapper.Map<Customer>(customer);
            var ac = customer.Account;
            Account acc = _mapper.Map<Account>(ac);
            _dbContext.Customers.Add(cs);
            _dbContext.SaveChanges();
            acc.Customer_id = cs.Customer_Id;
            try
            {
                accountRepo.AddAccount(cs.Customer_Id, ac, transactionRepo);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                DeleteCustomer(cs.Customer_Id,_transactionRepo);
                throw new Exception(ex.Message);
            }
            
        }

        public void DeleteCustomer(int customerId,ITransactionRepo _transactionrepo)
        {
           Customer cs = _dbContext.Customers.ToList().Find(o => o.Customer_Id == customerId);
            if(cs!= null)
            {
               
                _dbContext.Customers.Remove(cs);
               
                _dbContext.SaveChanges();
            }
            else
            {
                throw new Exception("Customer does not exist...");
            }
           


        }

        public List<GetAllCustomerModel> GetAllCustomers()
        {
            return _mapper.Map<List<GetAllCustomerModel>>(_dbContext.Customers.ToList());
            
        }

        public void UpdateCustomer(int customerID, JsonPatchDocument<Customer> csjson)
        {
            
            Customer cs = _dbContext.Customers.ToList().Find(o => o.Customer_Id==customerID);
            if(cs!=null)
            {
                if(csjson.Operations.Any(o => o.path.ToLower()== "/Customer_Id"))
                {
                    throw new Exception("Customer ID can't be Updated...");
                }
                csjson.ApplyTo(cs);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new Exception("Customer doest not exist...");
            }
           

        }
    }
}
