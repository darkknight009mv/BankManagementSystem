using System.ComponentModel.DataAnnotations;

namespace BankManagementSystem.Model
{
    public class GetAllCustomerModel
    {
        public GetAllCustomerModel() { }
        public GetAllCustomerModel(long adharnumber, int customer_Id, string name, DateTime dob, string address, string city, long mobible_Number)
        {
            Customer_Id = customer_Id;
            Name = name;
            Address = address;
            City = city;
            Mobile_Number = mobible_Number;

            DOB = dob;
            Adhar_Number = adharnumber;
        }

        
        public int Customer_Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public long Mobile_Number { get; set; }

        public long Adhar_Number { get; set; }


        public DateTime DOB { get; set; }
    }
}
