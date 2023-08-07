using BankManagementSystem.DAL;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BankManagementSystem.Model
{
    public class GetAllAccModel
    {
        public float InterestRate { get; set; }

        public long Account_Number { get; set; }
        public AccountType Account_Type { get; set; }
        public float Balance { get; set; }


        public long Customer_ID { get; set; }

        public GetAllAccModel() { }
            public GetAllAccModel(float InterestRate,long account_Number, AccountType account_Type, float balance, long customer)
            {
                Account_Number = account_Number;
                Account_Type = account_Type;
                Balance = balance;
                Customer_ID = customer;
             this.InterestRate = InterestRate;

            }

            
        }
    
}
