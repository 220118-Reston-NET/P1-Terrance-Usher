namespace ProjectOneUI
{
    /// <summary>
    /// The first menu for placing an order checks to see if the user already has a Customer ID
    /// </summary>
    public class PlaceOrderMenu_1 : IMenu
    {
        public void Display()
        {
            Console.WriteLine("Do you have a Customer ID?");
            Console.WriteLine("[3] Yes, I do and I remember it.");
            Console.WriteLine("[2] Yes, I do but I don't remember it.");
            Console.WriteLine("[1] No, I do not have one.");
            Console.WriteLine("[0] Go Back to Main Menu");
            
        }

        public string UserChoice()
        {
            int userInput = Convert.ToInt32(Console.ReadLine());

            switch (userInput)
            {
                case 3:
                    return "PlaceOrder_2";
                case 2:
                    return "SearchCustomer";
                case 1:
                    return "AddCustomer";
                case 0:
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