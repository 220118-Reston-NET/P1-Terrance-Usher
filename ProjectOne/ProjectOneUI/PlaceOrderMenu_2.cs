using ProjectOneBL;
using ProjectOneModel;

namespace ProjectOneUI
{
    public class PlaceOrderMenu_2 : IMenu
    {
        private ICustomerBL _custBL;
        public PlaceOrderMenu_2(ICustomerBL c_custBL) { _custBL = c_custBL;}
        public int CustID = 0;
        public int StoreID = 0;
        public List<Store>listOfStores = new List<Store>();
        public Cust currentCust = new Cust();

        public void Display()
        {
            bool reset = true;
            while (reset)
            {
                Console.WriteLine("Please Enter your Customer ID.");
                CustID = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                
                currentCust = _custBL.SearchCustomer(CustID);
                Console.WriteLine("So you are " + currentCust.CustName + "? [y/n]");
                string userInputID = Console.ReadLine();
                if (userInputID == "y" || userInputID == "Y")
                    reset = false;
            }
            
            bool reset1 = true;
            while (reset1)
            {
                Console.Clear();
                Console.WriteLine("Please select what store you would like to order from.");

                listOfStores = _custBL.GetAllStores();
                for (int i = listOfStores.Count - 1; i >= 0; i--)
                {
                    Console.WriteLine("[" + listOfStores[i].StoreID + "] " + listOfStores[i].StoreName);
                }

                Console.WriteLine("[0] Go Back to Main Menu");
                StoreID = Convert.ToInt32(Console.ReadLine());
                if (StoreID > 0 && StoreID <= listOfStores.Count)
                    reset1 = false;
            }

            Orders currentOrder = new Orders();
            currentOrder = _custBL.CreateOrder(CustID,StoreID);

            List<Inv>listOfInv = new List<Inv>();

            
            bool storeLoop = true;
            while (storeLoop)
            {
                listOfInv = _custBL.GetStoreInv(StoreID);
                foreach (Inv StoreItem in listOfInv)
                {
                    Console.WriteLine("=========================");
                    Console.WriteLine(StoreItem);
                    Console.WriteLine("=========================");
                }
                Console.WriteLine("Number of Items in Order: " + currentOrder.ItemsPurchased.Count() +"         Total: " + currentOrder.OrderTotal);
                Console.WriteLine("Please enter the item ID of what item you'd like to purchase.");
                int selectedItemID = Convert.ToInt32(Console.ReadLine());
                
                for (int i = 0; i < listOfInv.Count(); i++)
                {
                    if (listOfInv[i].ItemID == selectedItemID)
                    {
                        currentOrder.ItemsPurchased.Add(listOfInv[i]);
                        _custBL.AddToOrder(currentOrder,listOfInv[i]);
                    }
                }
                Console.WriteLine("Would you like to stop shopping?  [y/n]");
                string userInputShopping = Console.ReadLine();
                if (userInputShopping == "y" || userInputShopping == "Y")
                    storeLoop = false;

            }
            Console.Clear();
            Console.WriteLine("Your Order Summary:\n" + currentCust.CustName + "\n" + currentCust.CustAddress +
                                "           Order#: " + currentOrder.OrderID);
            foreach (Inv OrderItem in currentOrder.ItemsPurchased)
            {
                Console.WriteLine("=========================");
                Console.WriteLine(OrderItem.ItemName);
                Console.WriteLine("ID: " + OrderItem.ItemID);
                Console.WriteLine("$" + OrderItem.ItemPrice);
                Console.WriteLine("=========================");
            }
            Console.WriteLine("Store: " + listOfStores[StoreID-1].StoreName + ", " + listOfStores[StoreID-1].StoreAddress +
                                "\nTotal: " + currentOrder.OrderTotal);
            Console.WriteLine("Press Enter to return to Menu");
            Console.ReadLine();
            
            


            
        }

        public string UserChoice()
        {
            return "MainMenu";
        }
    }
}