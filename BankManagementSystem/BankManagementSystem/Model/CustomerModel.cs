using System.ComponentModel.DataAnnotations;

namespace BankManagementSystem.Model
{
    public class CustomerModel
    {
        public CustomerModel() { }
        public CustomerModel(long adharnumber,AccountModel account, string name,DateTime dob, string address, string city, long mobible_Number)
        {
          //  Customer_Id = customer_Id;
            Name = name;
            Address = address;
            City = city;
            Mobile_Number = mobible_Number;
            Account = account;
            DOB = dob;
            Adhar_Number = adharnumber;
           
        }
   
       // public string Customer_Id { get; set; } 
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }    
        public long Mobile_Number { get; set; }
      public AccountModel Account { get; set; }

       
        public DateTime DOB { get; set; }
        public long Adhar_Number { get; set; }
        public override string ToString()
        {
            return $" Name : {Name}, Address : {Address}, City : {City}, Mobile Number : {Mobile_Number},   Date of birth : {DOB}  ";
        }
    }
}
