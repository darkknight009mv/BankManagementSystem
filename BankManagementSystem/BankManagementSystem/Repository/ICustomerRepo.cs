using BankManagementSystem.DAL;
using BankManagementSystem.Model;
using Microsoft.AspNetCore.JsonPatch;

namespace BankManagementSystem.Repository
{
    public interface ICustomerRepo
    {
         void AddCustomer(CustomerModel customer, IAccountRepo accountRepo, ITransactionRepo transactionRepo);

         void DeleteCustomer(int customerId, ITransactionRepo _transactionrepo);

         void UpdateCustomer(int customerID, JsonPatchDocument<Customer> csjson);

         List<GetAllCustomerModel> GetAllCustomers();
       
    }
}
