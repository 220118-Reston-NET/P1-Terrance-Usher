using ProjectOneBL;
using ProjectOneModel;

namespace ProjectOneUI
{
    public class ViewOrderHistMenu : IMenu
    {
        private ICustomerBL _custBL;
        public ViewOrderHistMenu(ICustomerBL c_custBL) { _custBL = c_custBL;}

        public List<Orders> listOfOrders = new List<Orders>();

        public void Display()
        {
            // Console.WriteLine("How would you like to filter Order History?");
            // Console.WriteLine("[3] No Filter");
            // Console.WriteLine("[2] Filter through Customer ID");
            // Console.WriteLine("[1] Filter through Store ID");
            
            listOfOrders = _custBL.GetAllOrders();

            Console.Clear();

            foreach (Orders order in listOfOrders)
            {
                Console.WriteLine("=========================");
                Console.WriteLine("Order Summary:\n" + order.CustomerName + "\n" + order.CustomerAddress +
                                    "           Order#: " + order.OrderID);
                foreach (Inv OrderItem in order.ItemsPurchased)
                {
                    Console.WriteLine("=========================");
                    Console.WriteLine(OrderItem.ItemName);
                    Console.WriteLine("ID: " + OrderItem.ItemID);
                    Console.WriteLine("$" + OrderItem.ItemPrice);
                    Console.WriteLine("=========================");
                    order.OrderTotal = order.OrderTotal + OrderItem.ItemPrice;
                }
                Console.WriteLine("Store: " + order.StoreName + ", " + order.StoreAddress);
                Console.WriteLine("=========================");
            }

            Console.WriteLine("[0] Go back to Menu");
            
        }

        public string UserChoice()
        {
            string userInput = Console.ReadLine();
            Console.Clear();

            switch (userInput)
            {
             case "0":
                return "MainMenu";
            // case "1":
            //     bool validStoreID = true;
            //     List<Store> listOfStores = _custBL.GetAllStores();
            //     while (validStoreID)
            //     {
            //         Console.Clear();
            //         Console.WriteLine("Please Enter a valid store ID");
            //         int userStoreID = Convert.ToInt32(Console.ReadLine());
            //         if (userStoreID > 0 && userStoreID <= listOfStores.Count)
            //             validStoreID = false;
            //     }



            default:
                Console.WriteLine("Please Input a valid response");
                Console.WriteLine("Please press Enter to continue");
                Console.ReadLine();
                return "ViewOrderHistory";

            }

        }
    }
}