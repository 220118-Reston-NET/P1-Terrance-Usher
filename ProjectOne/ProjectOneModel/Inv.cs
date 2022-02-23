/*
    This Class is for the specific inventory for 1 store
*/

namespace ProjectOneModel
{
    public class Inv : Item //Store_Item
    {
        private int _itemQuantity;
        public int ItemQuantity
        {
            get { return _itemQuantity; }
            set 
            { 
                if (value >=  0)
                {
                    _itemQuantity = value;
                }
                else
                {
                    throw new Exception("Item Quantity can not be less than zero!");
                }
            }
        }
        public List<Item>? InvList { get; set;}
        

        public override string ToString()
        {
            return $"{ItemName}\nID: {ItemID}\n${ItemPrice}       qty:{ItemQuantity}" +
              $"\nDescription:\n{ItemDesc}\n#{ItemCate}";
        }
    }

}
