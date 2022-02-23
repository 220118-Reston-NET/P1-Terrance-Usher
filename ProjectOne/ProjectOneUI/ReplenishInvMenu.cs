using ProjectOneBL;
using ProjectOneModel;

namespace ProjectOneUI
{
    public class ReplenishInvMenu : IMenu
    {
        private ICustomerBL _custBL;
        public ReplenishInvMenu(ICustomerBL c_custBL) { _custBL = c_custBL;}

        public void Display()
        {
            List<Store>listOfStores = new List<Store>();
            listOfStores = _custBL.GetAllStores();

            Console.WriteLine("Please Select a Store to Replenish.");

            for (int i = listOfStores.Count - 1; i >= 0; i--)
            {
                Console.WriteLine("[" + listOfStores[i].StoreID + "] " + listOfStores[i].StoreName);
            }

            Console.WriteLine("[0] Go Back to Main Menu");
        }

        public string UserChoice()
        {
            int userInput = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            List<Store>listOfStores = new List<Store>();
            listOfStores = _custBL.GetAllStores();

            if (userInput == 0)
            {
                return "MainMenu";
            }
            else if (userInput > 0 && userInput <= listOfStores.Count)
            {
                Console.WriteLine("Store: " + listOfStores[userInput - 1].StoreName);
                List<Inv>listOfInv = new List<Inv>();
                listOfInv = _custBL.GetStoreInv(userInput);
                foreach (Inv StoreItem in listOfInv)
                {
                    Console.WriteLine("=========================");
                    Console.WriteLine(StoreItem);
                    Console.WriteLine("=========================");
                }
                Console.WriteLine("\nPlease enter the item ID of what Item you wish to replenish.");
                int selectedItemID = Convert.ToInt32(Console.ReadLine());
                
                for (int i = 0; i < listOfInv.Count(); i++)
                {
                    if (listOfInv[i].ItemID == selectedItemID)
                    {
                        Console.WriteLine("How many " + listOfInv[i].ItemName + " would you like to add?");
                        int userAmount = Convert.ToInt32(Console.ReadLine());
                        _custBL.ChangeInvQuantity(Math.Abs(userAmount),listOfInv[i].ItemID);
                    }
                }


                return "ReplenishInventory";

            }
            else
            {
                Console.WriteLine("Please Input a valid response");
                Console.WriteLine("Please press Enter to continue");
                Console.ReadLine();
                return "ReplenishInventory";
            }
        }
    }
}