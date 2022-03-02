using System.Data.SqlClient;
using ProjectOneModel;

namespace ProjectOneDL
{
    public class SQLRepository : IRepository
    {   private readonly string _connectionStrings;
        public SQLRepository(string c_connectionStrings)
        {
            _connectionStrings = c_connectionStrings;
        }

        public Cust AddCust(Cust c_cust)
        {
            
            string sqlQuery = @"insert  into Customer 
                            values(@CustName, @CustAddress, @CustNum, @PassWord, @UserName, 0)";

            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();

                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@CustName", c_cust.CustName);
                command.Parameters.AddWithValue("@CustAddress", c_cust.CustAddress);
                command.Parameters.AddWithValue("@CustNum", c_cust.CustNum);
                command.Parameters.AddWithValue("@PassWord", c_cust.PassWord);
                command.Parameters.AddWithValue("@UserName", c_cust.UserName);

                command.ExecuteNonQuery();

            }

            return c_cust;

        }

        public Orders AddToOrder(int StoreItemID, int Amount)
        {
            Orders UpdatedOrder = new Orders();
            // string sqlQuery = @"insert into Orders_StoreItem 
            //                 values (@OrderID,@StoreItemID)";
            string sqlQuery = @"select * from Customer c 
                        inner join Orders o on c.CustID = o.CustID 
                        inner join Store s  on s.StoreID = o.StoreID 
                        where OrderID = (select MAX(OrderID) from Customer c2  
                        inner join Orders o2 on c2.CustID = o2.CustID )";
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();

                SqlCommand command = new SqlCommand(sqlQuery, con);
                
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    UpdatedOrder.CustomerID = reader.GetInt32(0);
                    UpdatedOrder.CustomerName = reader.GetString(1);
                    UpdatedOrder.CustomerAddress = reader.GetString(2);
                    UpdatedOrder.CustomerNumber = reader.GetString(3);
                    UpdatedOrder.OrderID = reader.GetInt32(7);
                    UpdatedOrder.storeID = reader.GetInt32(9);
                    UpdatedOrder.StoreName = reader.GetString(11);
                    UpdatedOrder.StoreAddress = reader.GetString(12);

                }
            }
                sqlQuery = @"select * from Store_Item si
                        inner join Item i on si.ItemID = i.ItemID 
                        where si.Store_ItemID = @StoreItemID";

            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@StoreItemID", StoreItemID);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (Amount > reader.GetInt32(3))
                        throw new Exception("Store does not have enough quantity.");
                
                }
            }
                sqlQuery = @"insert into Orders_StoreItem 
                         values (@OrderID,@StoreItemID,@OrderQuantity)";
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@OrderID", UpdatedOrder.OrderID);
                command.Parameters.AddWithValue("@StoreItemID", StoreItemID);
                command.Parameters.AddWithValue("@OrderQuantity", Amount);
                command.ExecuteNonQuery();
            }

                sqlQuery = @"select * from Orders o 
                        inner join Orders_StoreItem osi on o.OrderID = osi.OrderID 
                        inner join Store_Item si on si.Store_ItemID = osi.Store_ItemID 
                        inner join Item i on i.ItemID = si.ItemID 
                        where o.OrderID = (select MAX(OrderID) from Orders o2 )";
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();
                SqlCommand command = new SqlCommand(sqlQuery, con);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    UpdatedOrder.ItemsPurchased.Add(new Inv(){
                        ItemID = reader.GetInt32(4),
                        ItemQuantity = reader.GetInt32(5),
                        ItemName = reader.GetString(11),
                        ItemPrice = reader.GetDecimal(12),
                        ItemDesc = reader.GetString(13),
                        ItemCate = reader.GetString(14)
                    });
                }
            }



            
            ChangeInvQuantity(StoreItemID, -(Amount));
            foreach (Inv Item in UpdatedOrder.ItemsPurchased)
            {
                UpdatedOrder.OrderTotal = UpdatedOrder.OrderTotal + (Item.ItemPrice * Item.ItemQuantity);
            }
                    
            return UpdatedOrder;

        }


        public Inv ChangeInvQuantity(int StoreItemID, int Amount)
        {
            

            string sqlQuery = @"update Store_Item 
                            set Quantity = Quantity + (@Value)
                            where Store_ItemID = @StoreItemID";
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();

                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@Value", Amount);
                command.Parameters.AddWithValue("@StoreItemID", StoreItemID);
                command.ExecuteNonQuery();

                sqlQuery = @"select * from Store_Item si 
                        inner join Item i on si.ItemID = i.ItemID 
                        where si.Store_ItemID = @StoreItemID";
                command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@StoreItemID", StoreItemID);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Inv selectedInv = new Inv(){
                        ItemID = reader.GetInt32(0),
                        ItemQuantity = reader.GetInt32(3),
                        ItemName = reader.GetString(5),
                        ItemPrice = reader.GetDecimal(6),
                        ItemDesc = reader.GetString(7),
                        ItemCate = reader.GetString(8)
                    };
                    return selectedInv;
                }

            }
            //Should Never happen
            Inv emptyInv = new Inv();
            return emptyInv;
            
        }

        public Orders CreateOrder(int CustID, int StoreID)
        {
            Orders createdOrder = new Orders();
            string sqlQuery = @"insert into Orders 
                            values (@CustID,@StoreID)";
            
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();

                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@CustID", CustID);
                command.Parameters.AddWithValue("@StoreID", StoreID);
                command.ExecuteNonQuery();

                sqlQuery = @"select * from Customer c 
                        inner join Orders o on c.CustID = o.CustID 
                        inner join Store s  on s.StoreID = o.StoreID 
                        where OrderID = (select MAX(OrderID) from Customer c2  
                        inner join Orders o2 on c2.CustID = o2.CustID )";

                command = new SqlCommand(sqlQuery, con);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    createdOrder.CustomerID = reader.GetInt32(0);
                    createdOrder.CustomerName = reader.GetString(1);
                    createdOrder.CustomerAddress = reader.GetString(2);
                    createdOrder.CustomerNumber = reader.GetString(3);
                    createdOrder.OrderID = reader.GetInt32(7);
                    createdOrder.storeID = reader.GetInt32(9);
                    createdOrder.StoreName = reader.GetString(11);
                    createdOrder.StoreAddress = reader.GetString(12);

                }
            }

            return createdOrder;

        }
        

        public List<Cust> GetAllCust()
        {
            List<Cust> listOfCustomer  = new List<Cust>();

            string sqlQuery = @"select * from Customer";

            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {

                con.Open();

                SqlCommand command = new SqlCommand(sqlQuery, con);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    listOfCustomer.Add(new Cust(){
                        CustID = reader.GetInt32(0),
                        CustName = reader.GetString(1),
                        CustAddress = reader.GetString(2),
                        CustNum = reader.GetString(3),
                        PassWord = reader.GetString(4),
                        UserName = reader.GetString(5),
                        Authorized = reader.GetBoolean(6)
                    });
                }
            }

            return listOfCustomer;
        }

        public async Task<List<Cust>> GetAllCustAsync()
        {
            List<Cust> listOfCustomer  = new List<Cust>();

            string sqlQuery = @"select * from Customer";

            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {

                await con.OpenAsync();

                SqlCommand command = new SqlCommand(sqlQuery, con);

                SqlDataReader reader = await command.ExecuteReaderAsync();

                while (reader.Read())
                {
                    listOfCustomer.Add(new Cust(){
                        CustID = reader.GetInt32(0),
                        CustName = reader.GetString(1),
                        CustAddress = reader.GetString(2),
                        CustNum = reader.GetString(3),
                        PassWord = reader.GetString(4),
                        UserName = reader.GetString(5),
                        Authorized = reader.GetBoolean(6)
                    });
                }
            }

            return listOfCustomer;
        }

        public List<Inv> GetAllOrderItems(int ID)
        {
            List<Inv> listOfPurchasedItems = new List<Inv>();

            string sqlQuery = @"select  * from Orders o 
                            inner join Orders_StoreItem osi on o.OrderID = osi.OrderID 
                            inner join Store_Item si on si.Store_ItemID = osi.Store_ItemID 
                            where o.OrderID = @OrderID";
            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {
                con.Open();

                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@OrderID", ID);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    listOfPurchasedItems.Add(new Inv(){
                        ItemID = reader.GetInt32(5),
                        ItemName = reader.GetString(10),
                        ItemPrice = reader.GetDecimal(11)
                    });
                }
            }
            return listOfPurchasedItems;
        }

        public List<Orders> GetAllOrdersByCustomerID(int CustID)
        {
            
            List<Orders> listOfOrders = new List<Orders>();
            string sqlQuery = @"select * from Customer c 
                            inner join Orders o on c.CustID = o.CustID 
                            inner join Store s  on s.StoreID = o.StoreID 
                            where c.CustID = @CustID";

            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {

                con.Open();

                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@CustID",CustID);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    listOfOrders.Add(new Orders(){
                        CustomerID = reader.GetInt32(0),
                        CustomerName = reader.GetString(1),
                        CustomerAddress = reader.GetString(2),
                        CustomerNumber = reader.GetString(3),
                        OrderID = reader.GetInt32(7),
                        storeID = reader.GetInt32(9),
                        StoreName = reader.GetString(11),
                        StoreAddress = reader.GetString(12)
                    });
                }
            }

            foreach (Orders order in listOfOrders)
            {
                sqlQuery = @"select osi.OrderQuantity, si.Store_ItemID , i.ItemName ,i.ItemPrice ,
                        i.ItemDesc ,i.ItemCate  from Orders o 
                        inner join Orders_StoreItem osi on o.OrderID = osi.OrderID 
                        inner join Store_Item si on si.Store_ItemID = osi.Store_ItemID 
                        inner join Item i on i.ItemID = si.ItemID 
                        where o.OrderID = @OrderID";
                
                using (SqlConnection con = new SqlConnection(_connectionStrings))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand(sqlQuery, con);
                    command.Parameters.AddWithValue("@OrderID",order.OrderID);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        order.ItemsPurchased.Add(new Inv{
                            ItemQuantity = reader.GetInt32(0),
                            ItemID = reader.GetInt32(1),
                            ItemName = reader.GetString(2),
                            ItemPrice = reader.GetDecimal(3),
                            ItemDesc = reader.GetString(4),
                            ItemCate = reader.GetString(5)

                        });
                    }
                }
                foreach (Inv item in order.ItemsPurchased)
                {
                    order.OrderTotal = order.OrderTotal + (item.ItemPrice * item.ItemQuantity);
                }
            }

            return listOfOrders;
        }

        public List<Orders> GetAllOrdersByStoreID(int StoreID)
        {
            
            List<Orders> listOfOrders = new List<Orders>();
            string sqlQuery = @"select * from Customer c 
                            inner join Orders o on c.CustID = o.CustID 
                            inner join Store s  on s.StoreID = o.StoreID 
                            where s.StoreID = @StoreID";

            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {

                con.Open();

                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@StoreID",StoreID);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    listOfOrders.Add(new Orders(){
                        CustomerID = reader.GetInt32(0),
                        CustomerName = reader.GetString(1),
                        CustomerAddress = reader.GetString(2),
                        CustomerNumber = reader.GetString(3),
                        OrderID = reader.GetInt32(7),
                        storeID = reader.GetInt32(9),
                        StoreName = reader.GetString(11),
                        StoreAddress = reader.GetString(12)
                    });
                }
            }

            foreach (Orders order in listOfOrders)
            {
                sqlQuery = @"select osi.OrderQuantity, si.Store_ItemID , i.ItemName ,i.ItemPrice ,
                        i.ItemDesc ,i.ItemCate  from Orders o 
                        inner join Orders_StoreItem osi on o.OrderID = osi.OrderID 
                        inner join Store_Item si on si.Store_ItemID = osi.Store_ItemID 
                        inner join Item i on i.ItemID = si.ItemID 
                        where o.OrderID = @OrderID";
                
                using (SqlConnection con = new SqlConnection(_connectionStrings))
                {
                    con.Open();
                    SqlCommand command = new SqlCommand(sqlQuery, con);
                    command.Parameters.AddWithValue("@OrderID",order.OrderID);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        order.ItemsPurchased.Add(new Inv{
                            ItemQuantity = reader.GetInt32(0),
                            ItemID = reader.GetInt32(1),
                            ItemName = reader.GetString(2),
                            ItemPrice = reader.GetDecimal(3),
                            ItemDesc = reader.GetString(4),
                            ItemCate = reader.GetString(5)

                        });
                    }
                }
                foreach (Inv item in order.ItemsPurchased)
                {
                    order.OrderTotal = order.OrderTotal + (item.ItemPrice * item.ItemQuantity);
                }
            }

            return listOfOrders;
        }

        public List<Store> GetAllStores()
        {
            List<Store> listOfStores = new List<Store>();

            string sqlQuery = @"select * from Store s";

            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {

                con.Open();

                SqlCommand command = new SqlCommand(sqlQuery, con);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    listOfStores.Add(new Store(){
                        StoreID = reader.GetInt32(0),
                        StoreName = reader.GetString(1),
                        StoreAddress = reader.GetString(2)
                    });
                }
            }

            return listOfStores;
        }

        public Cust GetCustByID(int CustID)
        {
            Cust foundCust = new Cust();
            string sqlQuery = @"select * from Customer c
                                where c.CustID = @CustID";

            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {

                con.Open();

                SqlCommand command = new SqlCommand(sqlQuery, con);
                command.Parameters.AddWithValue("@CustID", CustID);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    foundCust.CustID = reader.GetInt32(0);
                    foundCust.CustName = reader.GetString(1);
                    foundCust.CustAddress = reader.GetString(2);
                    foundCust.CustNum = reader.GetString(3);
                    foundCust.PassWord = reader.GetString(4);
                    foundCust.UserName = reader.GetString(5);
                    foundCust.Authorized = reader.GetBoolean(6);
                }
            }
            return foundCust;
        }

        public int GetLastCust()
        {
            int lastCustID = 0;
            string sqlQuery = "select MAX(c.CustID) from Customer c";

            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {

                con.Open();

                SqlCommand command = new SqlCommand(sqlQuery, con);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    lastCustID = reader.GetInt32(0);
                }
            }
            return lastCustID;
        }

        public List<Inv> GetStoreInv(int StoreID)
        {
            List<Inv>listOfInv = new List<Inv>();

            string sqlQuery = @"select s.StoreID ,s.StoreName ,s.StoreAddress ,si.Store_ItemID ,
                            si.Quantity ,i.ItemName ,i.ItemPrice ,i.ItemDesc ,i.ItemCate  from Store s
                            inner join Store_Item si on s.StoreID = si.StoreID 
                            inner join Item i on i.ItemID = si.ItemID 
                            where s.StoreID = @StoreID";

            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {

                con.Open();

                SqlCommand command = new SqlCommand(sqlQuery, con);

                command.Parameters.AddWithValue("@StoreID", StoreID);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    listOfInv.Add(new Inv(){
                        ItemID = reader.GetInt32(3),
                        ItemQuantity = reader.GetInt32(4),
                        ItemName = reader.GetString(5),
                        ItemPrice = reader.GetDecimal(6),
                        ItemDesc = reader.GetString(7),
                        ItemCate = reader.GetString(8)
                    });
                }

                return listOfInv;

            }
        }

        public void GiveCustauthorization(int CustID)
        {
            string sqlQuery = @"update Customer 
                            set Authorized = 1
                            where CustID  = @CustID";

            using (SqlConnection con = new SqlConnection(_connectionStrings))
            {

                con.Open();

                SqlCommand command = new SqlCommand(sqlQuery, con);

                command.Parameters.AddWithValue("@CustID", CustID);

                command.ExecuteNonQuery();
            }
        }
    }
}