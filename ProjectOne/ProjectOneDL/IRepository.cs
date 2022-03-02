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

        Task<List<Cust>> GetAllCustAsync();

        void GiveCustauthorization(int CustID);

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


        Orders AddToOrder(int StoreItemID, int Amount);


        Inv ChangeInvQuantity(int StoreItemID,int Amount);

        List<Orders> GetAllOrdersByCustomerID(int CustomerID);

        List<Orders> GetAllOrdersByStoreID(int StoreID);

        List<Inv> GetAllOrderItems(int ID);
    }
}

