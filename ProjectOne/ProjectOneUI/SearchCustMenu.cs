using ProjectOneBL;
using ProjectOneModel;

namespace ProjectOneUI
{
    public class SearchCustMenu : IMenu
    {
        private ICustomerBL _custBL;
        public SearchCustMenu(ICustomerBL c_custBL) { _custBL = c_custBL;}

        public void Display()
        {
            Console.WriteLine("Please select an option to filter the customer database");
            Console.WriteLine("[3] By Name");
            Console.WriteLine("[2] By Phone Number");
            Console.WriteLine("[1] By Address");
            Console.WriteLine("[0] Go Back to Main Menu");
        }

        public string UserChoice()
        {
            string userInput = Console.ReadLine();
            Console.Clear();

            switch (userInput)
            {
                case "0":
                    return "MainMenu";
                case "1":
                    Console.WriteLine("Please enter an addresss");
                    string address = Console.ReadLine();
                     List<Cust> listOfCustA = _custBL.SearchCustomer("Address",address);
                     foreach (var item in listOfCustA)
                     {
                         Console.WriteLine("===================");
                         Console.WriteLine(item);
                     }
                     Console.WriteLine("Please press Enter to continue");
                     Console.ReadLine();

                     return "MainMenu";
                case "2":
                    Console.WriteLine("Please enter a phone number");
                    string pnum = Console.ReadLine();
                     List<Cust> listOfCustP = _custBL.SearchCustomer("PhoneNumber",pnum);
                     foreach (var item in listOfCustP)
                     {
                         Console.WriteLine("===================");
                         Console.WriteLine(item);
                     }
                     Console.WriteLine("Please press Enter to continue");
                     Console.ReadLine();

                     return "MainMenu";
                case "3":
                    Console.WriteLine("Please enter a name");
                    string name = Console.ReadLine();
                     List<Cust> listOfCustN = _custBL.SearchCustomer("Name",name);
                     foreach (var item in listOfCustN)
                     {
                         Console.WriteLine("===================");
                         Console.WriteLine(item);
                     }
                     Console.WriteLine("Please press Enter to continue");
                     Console.ReadLine();

                     return "MainMenu";
                default:
                    Console.WriteLine("Please Input a valid response");
                    Console.WriteLine("Please press Enter to continue");
                    Console.ReadLine();
                    return "SearchCustomer";
            }
        }
    }
}