using ProjectOneDL;
using ProjectOneModel;

namespace ProjectOneBL
{

    public class CustomerBL : ICustomerBL
    {

        //Dependency Injection Pattern
        private IRepository _repo;
        public CustomerBL(IRepository c_repo) { _repo = c_repo; }


        public Cust AddCust(Cust c_cust)
        {
            return _repo.AddCust(c_cust);
        }

        public void AddToOrder(Orders CurrentOrder, Inv StoreItem)
        {
            _repo.AddToOrder(CurrentOrder,StoreItem);
        }

        public bool AuthenticateCust(string UserName, string PassWord)
        {
            List<Cust>listOfAllCust = _repo.GetAllCust();
            foreach (Cust user in listOfAllCust)
            {
                if (user.UserName == UserName && user.PassWord == PassWord)
                {
                    if (user.Authorized == true)
                    {
                        return true;
                    }
                    else
                    {
                        throw new Exception("You are not authorized.");
                    }
                }
            }
            throw new Exception("Wrong Username or Password.");
        }

        public void ChangeInvQuantity(int value, int StoreItemID)
        {
            _repo.ChangeInvQuantity(value, StoreItemID);
        }

        public Orders CreateOrder(int CustID, int StoreID)
        {
            return _repo.CreateOrder(CustID,StoreID);
        }

        public List<Cust> GetAllCustomers(string UserName, string PassWord)
        {
            if (AuthenticateCust(UserName,PassWord))
            {
                return _repo.GetAllCust();
            }
            else
            {
                return null;
            }
            
        }

        public async Task<List<Cust>> GetAllCustomersAsync(string UserName, string PassWord)
        {
            if (AuthenticateCust(UserName,PassWord))
                return await _repo.GetAllCustAsync();
            else
                return null;
        }

        public List<Orders> GetAllOrders()
        {
            return _repo.GetAllOrders(0, " ");
        }

        public List<Store> GetAllStores()
        {
            return _repo.GetAllStores();
        }

        public int GetLastCust()
        {
            return _repo.GetLastCust();
        }

        public List<Inv> GetStoreInv(int StoreID)
        {
            return _repo.GetStoreInv(StoreID);
        }

        public Cust GiveCustAuthentication(string UserName, string PassWord, int CustID)
        {
            if (AuthenticateCust(UserName,PassWord))
            {
                _repo.GiveCustAuthentication(CustID);
                return _repo.GetCustByID(CustID);
            }
            else
            {
                throw new Exception("You're not authorized.");
            }
        }

        public List<Cust> SearchCustomer(string c_cate, string c_name)
        {
            List<Cust> listofCustomer = _repo.GetAllCust();

            switch (c_cate)
            {
                case "Name":
                    return listofCustomer.Where(cust => cust.CustName.Contains(c_name)).ToList();
                case "Address":
                    return listofCustomer.Where(cust => cust.CustAddress.Contains(c_name)).ToList();
                case "PhoneNumber":
                    return listofCustomer.Where(cust => cust.CustNum.Contains(c_name)).ToList();
                default:
                    Console.WriteLine("You can't spell.");
                    Console.ReadLine();
                    return listofCustomer;
            }

        }

        public Cust SearchCustomer(int c_cID)
        {
            return _repo.GetCustByID(c_cID);

        }

        public Store SearchStores(int StoreID)
        {
            List<Store>listOfAllStores =  _repo.GetAllStores();
            List<Store>selectedStore = listOfAllStores.Where(store => store.StoreID.Equals(StoreID)).ToList();
            return selectedStore[0];
        }
    }


}