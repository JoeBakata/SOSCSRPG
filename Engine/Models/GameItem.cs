using Engine.Actions;

namespace Engine.Models
{
    public class GameItem
    {
        public enum ItemCategory// Enum?
        {
            Miscellaneous,
            Weapon
        }

        public ItemCategory Category { get; }
        public int ItemTypeID { get; }
        public string Name { get; }
        public int Price { get; }
        public bool IsUnique { get; }
        public IAction Action { get; set; }

        public GameItem(ItemCategory category, int itemTypeID, string name, int price,// This is the GameItem constructor
            bool isUnique = false, IAction action = null)
        {// The false means if we only use the first 3 parameters it does not have a unique property aka unique = false
            Category = category;
            ItemTypeID = itemTypeID;
            Name = name;
            Price = price;
            IsUnique = isUnique;// to set the parameter in the constructor, it must be at the end of the list
            Action = action;
        }

        public void PerformAction(LivingEntity actor, LivingEntity target)
        {
            Action?.Execute(actor, target);
        }

        public GameItem Clone()
        {
            return new GameItem(Category, ItemTypeID, Name, Price,
                IsUnique, Action);
        }
    }
}
