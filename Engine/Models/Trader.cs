using System.Collections.ObjectModel;

namespace Engine.Models
{
    public class Trader : BaseNotificationClass
    {
        public string Name { get; set; }// Property of trader class
        public ObservableCollection<GameItem> Inventory { get; set; }// Property of trader class

        public Trader(string name)// Trader constructor
        {
            Name = name;
            Inventory = new ObservableCollection<GameItem>();
        }

        public void AddItemToInventory(GameItem item)// This is a member function of the Trader class. AddItemToInventory function
        {
            Inventory.Add(item);// Adds item to Inventory
        }

        public void RemoveItemFromInventory(GameItem item)// RemoveItemFromInventory function
        {
            Inventory.Remove(item);// Remove item from Inventory
        }
    }
}
