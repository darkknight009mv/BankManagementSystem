using BankManagementSystem.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankManagementSystem.DAL
{
    public class Transaction
    {
        public Transaction() { }
        public Transaction(int transaction_Id, string remark, DateTime date, float amount, TransactionType type,  long accountnumber)
        {
            Transaction_Id = transaction_Id;
            CreatedOn = date;
            Amount = amount;
            Type = type;
            Remark = remark;
            
            this.Account_number = accountnumber;
        }

        [Key]
        public int Transaction_Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public float Amount { get; set; }
        public TransactionType Type { get; set; }
        public string Remark { get; set; }


        //public int Account_Number { get; set; }

        [ForeignKey("Account_Number")]
        public long Account_number { get; set; }
    }
}
