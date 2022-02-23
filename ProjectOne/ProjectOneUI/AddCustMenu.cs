using ProjectOneModel;
using ProjectOneBL;

namespace ProjectOneUI
{
    public class AddCustMenu : IMenu
    {

        private static Cust _newCust = new Cust();
        
        //Dependency Injection
        private ICustomerBL _custBL;
        public AddCustMenu(ICustomerBL c_custBL) { _custBL = c_custBL; }

        public void Display()
        {
            Console.WriteLine("Enter Your Information");
            Console.WriteLine("[4] Address - " + _newCust.CustAddress);
            Console.WriteLine("[3] Phone Number - " + _newCust.CustNum);
            Console.WriteLine("[2] Name - " + _newCust.CustName);
            Console.WriteLine("[1] Save");
            Console.WriteLine("[0] Go Back to Main Menu");
        }

        public string UserChoice()
        {
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "0":
                    return "MainMenu";
                case "1":
                    Console.Clear();
                    _custBL.AddCust(_newCust);
                    Console.WriteLine("Your Customer ID is: " + _custBL.GetLastCust());
                    Console.WriteLine("\nPress Enter to leave this menu.");
                    Console.ReadLine();
                    return "MainMenu";
                case "2":
                    Console.WriteLine("Please enter a Name");
                    _newCust.CustName = Console.ReadLine();
                    return "AddCustomer";
                case "3":
                    Console.WriteLine("Please Enter a Phone Number");
                    try
                    {
                        _newCust.CustNum = Console.ReadLine();
                    }
                    catch (System.Exception exc)
                    {
                        Console.WriteLine(exc.Message);
                        Console.WriteLine("Please press Enter to continue");
                        Console.ReadLine();
                    }
                    return "AddCustomer";
                case "4":
                    Console.WriteLine("Please enter an Address.");
                    _newCust.CustAddress = Console.ReadLine();
                    return "AddCustomer";
                default:
                    Console.WriteLine("Please input a vaild response");
                    Console.WriteLine("Please Press Enter to continue");
                    Console.ReadLine();
                    return "AddCustomer";
            }
        }
    }
}