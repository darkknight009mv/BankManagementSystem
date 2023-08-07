using BankManagementSystem.DAL;
using System.ComponentModel.DataAnnotations;

namespace BankManagementSystem.Model
{
    public class TransactionModel
    {
        public TransactionModel() { }   
        public TransactionModel(int transaction_Id, DateTime date, float amount, TransactionType type, long account_Number)
        {
            Transaction_Id = transaction_Id;
            Date = date;
            Amount = amount;
            Type = type;
            Account_Number = account_Number;
            
        }
      
        public int Transaction_Id { get; set; }
        public DateTime Date { get; set; }
        public float Amount { get; set; }
        public TransactionType Type { get; set; }
        public long Account_Number { get; set; }
        
    }
}
