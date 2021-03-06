/*
    This Class is for customer model and what it should contain
*/

namespace ProjectOneModel
{
    public class Cust 
    {
        public int CustID {get; set;}
        public string? CustName { get; set;}
        public string? CustAddress {get; set;}
        public string? CustNum { get; set; }
        public List<Item> CustOrders { get; set; }
        public string UserName { get; set; }
        public string PassWord {get; set; }
        public bool Authorized {get; set; }

        public Cust()
        {
            CustName = " ";
            CustAddress = " ";
            CustNum = "0000000000";
            UserName = "";
            PassWord = "";
            Authorized = false;
        }

        public override string ToString()
        {
            return $"ID: {CustID}\nName: {CustName}\nPhone Number: {CustNum}" +
              $"\nAddress: {CustAddress}\nOrder #'s: {CustOrders}";
        }
    }

}