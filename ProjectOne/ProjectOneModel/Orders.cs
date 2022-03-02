

namespace ProjectOneModel
{
    public class Orders
    {
        public int OrderID { get; set; }
        public int storeID {get; set; }
        public string StoreName { get; set; }
        public string StoreAddress { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerNumber { get; set; }
        public string CustomerAddress { get; set; }

        public decimal OrderTotal { get; set; }

        public List<Inv> ItemsPurchased {get; set;}

        public Orders()
        {
            OrderID = 0;
            StoreName = " ";
            StoreAddress = " ";
            CustomerName = " ";
            CustomerAddress = " ";
            CustomerNumber = " ";
            ItemsPurchased = new List<Inv>();
            OrderTotal = 0;
        }

        public override string ToString()
        {
            return $"Order ID: {OrderID}\nCustomer ID: {CustomerID}\nName: {CustomerName}\nPhone Number: {CustomerNumber}" +
              $"\nAddress: {CustomerAddress}\n{ItemsPurchased}\nTotal: {OrderTotal}";
        }

    
        
    
    
    
    
    }

}