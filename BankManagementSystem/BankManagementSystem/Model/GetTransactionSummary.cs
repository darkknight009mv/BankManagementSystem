using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BankManagementSystem.Model
{
    public class GetTransactionSummary
    {
        public GetTransactionSummary() { }
        public GetTransactionSummary(int transaction_Id, string remark, DateTime date, float amount, TransactionType type, long accountnumber)
        {
            Transaction_Id = transaction_Id;
            CreatedOn = date;
            Amount = amount;
            Type = type;
            Remark = remark;

            this.Account_number = accountnumber;
        }

      
        public int Transaction_Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public float Amount { get; set; }
        public TransactionType Type { get; set; }
        public string Remark { get; set; }


        //public int Account_Number { get; set; }

      
        public long Account_number { get; set; }
    }
}
