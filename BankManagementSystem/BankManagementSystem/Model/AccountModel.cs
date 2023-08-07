using System.ComponentModel.DataAnnotations;

namespace BankManagementSystem.Model
{
    public class AccountModel
    {
        public AccountModel() { }
        public AccountModel( AccountType account_Type, float balance)
        {
            
            Account_Type = account_Type;
            Balance = balance;
          
           
        }

      
       
        public AccountType Account_Type { get; set; }
        public float Balance { get; set; }


        
    

    }
}
