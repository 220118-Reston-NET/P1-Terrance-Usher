namespace ProjectOneUI
{


    public class MainMenu : IMenu
    {
        public void Display()
        {
            Console.WriteLine("Welcome to Store");
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("[6] Replenish Inventory");
            Console.WriteLine("[5] View Order History");
            Console.WriteLine("[4] Place Order");
            Console.WriteLine("[3] View Store Inventory");
            Console.WriteLine("[2] Search For a Customer");
            Console.WriteLine("[1] Add Customer");
            Console.WriteLine("[0] Exit");


        }

        public string UserChoice()
        {
            string userInput = Console.ReadLine();

            switch (userInput)
            {
                case "0":
                    return "Exit";
                case "1":
                    return "AddCustomer";
                case "2":
                    return "SearchCustomer";
                case "3":
                    return "ViewStoreInventory";
                case "4":
                    return "PlaceOrder_1";
                case "5":
                    return "ViewOrderHistory";
                case "6":
                    return "ReplenishInventory";
                default:
                    Console.WriteLine("Please input a valid response");
                    Console.WriteLine("Please press Enter to continue");
                    Console.ReadLine();
                    return "MainMenu";

            }
        }
    }
}