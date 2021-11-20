namespace Engine.Models
{
    public class GameItem
    {
        public int ItemTypeID { get; }
        public string Name { get; }
        public int Price { get; }
        public bool IsUnique { get; }

        public GameItem(int itemTypeID, string name, int price, bool isUnique = false)
        {// The false means if we only use the first 3 parameters it does not have a unique property aka unique = false
            ItemTypeID = itemTypeID;
            Name = name;
            Price = price;
            IsUnique = isUnique;// to set the parameter in the constructor, it must be at the end of the list
        }

        public GameItem Clone()
        {
            return new GameItem(ItemTypeID, Name, Price, IsUnique);
        }
    }
}
