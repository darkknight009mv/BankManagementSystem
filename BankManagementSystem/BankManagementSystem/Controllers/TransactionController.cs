using BankManagementSystem.DAL;
using BankManagementSystem.Model;
using BankManagementSystem.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BankManagementSystem.Controllers
{
    [ApiController]
    public class TransactionController : ControllerBase
    {

        private ITransactionRepo _transactionRepo;
        private IAccountRepo _accountRepo;


        public TransactionController(ITransactionRepo transactionRepo,IAccountRepo accountRepo)
        {
            _transactionRepo = transactionRepo;
            _accountRepo = accountRepo;
        }

        

        [HttpGet("TransactionSummary")]
        public IActionResult TransactionSummary(int accountId, DateTime sd, DateTime ed)
        {
            
                List<Transaction> summary = _transactionRepo.TransactionSummary(accountId,sd,ed);
                return Ok(summary);
           
        }

        [HttpPost("TransferMoney")]
        public IActionResult TransferMoney(long sourceAccountId, long destinationAccountId, float transferAmount)
        {
            try
            {
                _transactionRepo.TransferMoney(sourceAccountId, destinationAccountId, transferAmount, _accountRepo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
                return Ok("Transfer successful.");
            
        }

        [HttpPost("UpdateAccountType")]
        public IActionResult UpdateAccountType(long accountNumber, AccountType accountType)
        {
            try
            {
                _transactionRepo.UpdateAccountType(accountNumber, accountType);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Account type updated Successfully...");
        }

        [HttpPost("UpdateTransactionType")]
        public IActionResult UpdateTransactionType(int transactionId, TransactionType transactionType)
        {
            try
            {
                _transactionRepo.UpdateTransactionType(transactionId, transactionType);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Transaction type updated Successfully...");
        }

        [HttpPost("UpdateTransactionAmount")]
        public IActionResult UpdateTransactionAmount(int transactionId, float amount)
        {
            try
            {
                _transactionRepo.UpdateTransactionAmount(transactionId, amount);
                return Ok("Transaction amount  updated Successfully...");
            }
            catch (Exception ex) { 
                    return BadRequest(ex.Message);
            }

           
        }

        [HttpDelete("DeleteTransaction")]
        public IActionResult DeleteTransaction(int transactionId)
        {
            try
            {
                _transactionRepo.DeleteTransaction(transactionId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Transaction deleted SuccessFully...");
        }

    }
}
