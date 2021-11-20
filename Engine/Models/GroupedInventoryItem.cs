namespace Engine.Models
{
    public class GroupedInventoryItem : BaseNotificationClass
    {
        private GameItem item;
        private int quantity;

        public GameItem Item
        {
            get { return item; }
            set
            {
                item = value;
                OnPropertyChanged(nameof(Item));
            }
        }

        public int Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                OnPropertyChanged(nameof(Quantity));
            }
        }

        public GroupedInventoryItem(GameItem item, int quantity)// GroupedInventoryItem constructor
        {               // which takes in an item and quantity
            Item = item;
            Quantity = quantity;
        }
    }
}
