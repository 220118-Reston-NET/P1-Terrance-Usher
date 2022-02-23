using ProjectOneModel;

namespace ProjectOneBL
{
    /// <summary>
    /// Business Layer is used for validating and processing data from either the user or database
    /// The kind of processing is dependent on the type of functionality
    /// </summary>
    public interface ICustomerBL
    {
        /// <summary>
        /// Will add a customer to the database
        /// </summary>
        /// <param name="c_cust"></param>
        /// <returns></returns>
        Cust AddCust(Cust c_cust);

        /// <summary>
        /// Grabs the largest customer id a.k.a the last customer added
        /// </summary>
        /// <returns></returns>
        int GetLastCust();

        /// <summary>
        /// Will give a list of cust objects that are related to the searched name
        /// </summary>
        /// <param name="c_name"></param>
        /// <returns></returns>
        List<Cust> SearchCustomer(string c_cate,string c_name);

        Cust SearchCustomer(int c_cID);


        List<Store> GetAllStores();

        List<Inv> GetStoreInv (int StoreID);

        Orders CreateOrder(int CustID, int StoreID);

        void AddToOrder(Orders CurrentOrder, Inv StoreItem);

        void ChangeInvQuantity(int value,int StoreItemID);

        List<Orders> GetAllOrders();


    }
}