using AutoMapper;
using BankManagementSystem.DAL;
using BankManagementSystem.Model;
using BankManagementSystem.Repository;
using BankManagementSystem.Utils;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BankManagementSystem.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IAccountRepo _accountRepo;
       private ITransactionRepo _transactionRepo;
       

        public AccountController(IAccountRepo accountRepo,ITransactionRepo transactionRepo)
        {
           _accountRepo = accountRepo;
            _transactionRepo = transactionRepo;
          
          
        }

        [HttpPost("AddAccount")]
        public IActionResult AddAccount(int cmid,AccountModel account)
        {
            try
            {
                _accountRepo.AddAccount(cmid, account, _transactionRepo);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
            return Ok("Account added SuccessFully...");

        }


        [HttpDelete("RemoveAccount")]
        public IActionResult RemoveAccount(int accountNumber, AccountType Account_Type)
        {
            try
            {
                _accountRepo.RemoveAccount(accountNumber, Account_Type, _transactionRepo);
                
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Account removed successfully....");
        }

        [HttpGet("GetAllAccounts")]
        public IActionResult GetAllAccounts()
        {
           
            return Ok(_accountRepo.GetAllAccounts().ToArray());
        }

        [HttpPost("WithdrawFromAccount")]
        public IActionResult WithdrawFromAccount(AccountType act, long accountnumber,float withdrawalAmount)
        {
            try
            {
                _accountRepo.WithdrawFromAccount(act, accountnumber, withdrawalAmount, false);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
            return Ok("Withdrawal successful.");


        }

        [HttpPost("DepositInAccount")]
        public IActionResult DepositInAccount(AccountType act, long accountnumber, float depositAmount)
        {
            try
            {
                _accountRepo.DepositInAccount(act, accountnumber, depositAmount, false);

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("Deposit successful.");

        }

    }
}
