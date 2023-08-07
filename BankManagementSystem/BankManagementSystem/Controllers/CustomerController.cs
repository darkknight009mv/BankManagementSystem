using AutoMapper;
using BankManagementSystem.DAL;
using BankManagementSystem.Model;
using BankManagementSystem.Repository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BankManagementSystem.Controllers
{
    [ApiController]
    public class CustomerController : ControllerBase
    {
       
        private ICustomerRepo _customerRepo;
        private IMapper _mapper;
        
        private ITransactionRepo _transactionRepo;
        private IAccountRepo _accountRepo;
      

        public CustomerController(IAccountRepo accountrepo,ICustomerRepo customerRepo, IMapper mapper,ITransactionRepo transactionRepo)
        {
            _customerRepo = customerRepo;
            _mapper = mapper;
           
            _transactionRepo = transactionRepo;
            _accountRepo = accountrepo;
          
        }

        [HttpPost("AddCustomer")]
        public IActionResult AddCustomer(CustomerModel customer)
        {
            try
            {
                _customerRepo.AddCustomer(customer, _accountRepo, _transactionRepo);
                return Ok("Customer added Successfully...");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("DeleteCustomer")]
        public IActionResult DeleteCustomer(int customerId )
        {
            try
            {
                _customerRepo.DeleteCustomer(customerId,_transactionRepo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Customer removed Successfully...");
        }

        [HttpPatch("UpdateCustomer")]
        public IActionResult UpdateCustomer(int customerID, JsonPatchDocument<Customer> csjson)
        {
            try
            {
                _customerRepo.UpdateCustomer(customerID, csjson);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Custmer Update Successfully...");
        }

        [HttpGet("GetAllCustomers")]
        public IActionResult GetAllCustomers()
        {
           
            return Ok(_customerRepo.GetAllCustomers().ToArray());
        }

    }
}
