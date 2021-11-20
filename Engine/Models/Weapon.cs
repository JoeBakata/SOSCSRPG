namespace Engine.Models
{
    public class Weapon : GameItem
    {
        #region Properties
        public int MinimumDamage { get; }
        public int MaximumDamage { get; }
        #endregion Properties
        public Weapon(int itemTypeID, string name, int price, int minDamage, int maxDamage)
            : base(itemTypeID, name, price, true)
        {
            MinimumDamage = minDamage;
            MaximumDamage = maxDamage;
        }

        public new Weapon Clone()
        {
            return new Weapon(ItemTypeID, Name, Price, MinimumDamage, MaximumDamage);
        }
    }
}
