using ProjectOneModel;

namespace ProjectOneDL
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Add a customer to the database
        /// </summary>
        /// <param name="c_cust"></param>
        /// <returns> Will return the customer that was added </returns>
        Cust AddCust(Cust c_cust);

        /// <summary>
        /// Grabs the last customer by searching for the largest id number
        /// </summary>
        /// <returns></returns>
        int GetLastCust();

        /// <summary>
        /// Will give a list of all customers in the database
        /// </summary>
        /// <returns> A list collection of Cust objects </returns>
        List<Cust> GetAllCust();

        /// <summary>
        /// Grab a customer by their ID
        /// </summary>
        /// <param name="CustID"></param>
        /// <returns></returns>
        Cust GetCustByID(int CustID);


        /// <summary>
        /// Will give a list of all currently available stores in the database
        /// </summary>
        /// <returns></returns>
        List<Store> GetAllStores();


        List<Inv> GetStoreInv(int StoreID);


        Orders CreateOrder(int CustID, int StoreID);


        void AddToOrder(Orders CurrentOrder, Inv StoreItem);


        void ChangeInvQuantity(int value,int StoreItemID);

        List<Orders> GetAllOrders(int ID, string filter);

        List<Inv> GetAllOrderItems(int ID);
    }
}

