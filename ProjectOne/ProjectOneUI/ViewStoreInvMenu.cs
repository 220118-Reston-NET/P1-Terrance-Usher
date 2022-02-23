using ProjectOneBL;
using ProjectOneModel;

namespace ProjectOneUI
{
    public class ViewStoreInvMenu : IMenu
    {
        private ICustomerBL _custBL;
        public ViewStoreInvMenu(ICustomerBL c_custBL) { _custBL = c_custBL;}
        public void Display()
        {
            List<Store>listOfStores = new List<Store>();
            listOfStores = _custBL.GetAllStores();

            Console.WriteLine("Please Select a Store to View.");

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
                Console.WriteLine("Located: "+ listOfStores[userInput - 1].StoreAddress);
                List<Inv>listOfInv = new List<Inv>();
                listOfInv = _custBL.GetStoreInv(userInput);
                foreach (Inv StoreItem in listOfInv)
                {
                    Console.WriteLine("=========================");
                    Console.WriteLine(StoreItem);
                    Console.WriteLine("=========================");
                }
                Console.WriteLine("\nPress Enter to Exit Menu");
                Console.ReadLine();

                return "MainMenu";

            }
            else
            {
                Console.WriteLine("Please Input a valid response");
                Console.WriteLine("Please press Enter to continue");
                Console.ReadLine();
                return "ViewStoreInventory";
            }
        }
    }
}