using BankManagementSystem.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankManagementSystem.DAL
{
    public class Account
    {
        public Account() { }
        public Account(DateTime createdon,long account_Number, AccountType account_Type, float balance,int customer_id)
        {
            Account_Number = account_Number;
            Account_Type = account_Type;
            Balance = balance;
            Customer_id = customer_id;
            CreatedOn = createdon;
        }

        [Key]
        public long Account_Number { get; set; }
        public AccountType Account_Type { get; set; }
        public float Balance { get; set; }


        [ForeignKey("Customer_Id")]
        
        public int Customer_id { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
