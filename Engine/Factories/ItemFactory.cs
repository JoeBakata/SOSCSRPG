using System.Collections.Generic;
using System.Linq;
using Engine.Actions;
using Engine.Models;

namespace Engine.Factories
{
    public static class ItemFactory
    {
        private static readonly List<GameItem> standardGameItems = new List<GameItem>();// readonly means the variable, standardGameItems, can only be set equal to something here 
                       // where its declared on line 9 or inside a constructor. Not needed but protects us from accidently setting the value somewhere else.
        static ItemFactory()// ItemFactory constructor
        {
            BuildWeapon(1001, "Pointy Stick", 1, 1, 2);
            BuildWeapon(1002, "Rusty Sword", 5, 1, 3);

            BuildWeapon(1501, "Snake fangs", 0, 0, 2);
            BuildWeapon(1502, "Rat claws", 0, 0, 2);
            BuildWeapon(1503, "Spider fangs", 0, 0, 4);

            BuildMiscellaneousItem(9001, "Snake fang", 1);
            BuildMiscellaneousItem(9002, "Snakeskin", 2);
            BuildMiscellaneousItem(9003, "Rat tail", 1);
            BuildMiscellaneousItem(9004, "Rat fur", 2);
            BuildMiscellaneousItem(9005, "Spider fang", 1);
            BuildMiscellaneousItem(9006, "Spider silk", 2);
        }

        public static GameItem CreateGameItem(int itemTypeID)
        {
            return standardGameItems.FirstOrDefault(item => item.ItemTypeID == itemTypeID)?.Clone();
        }

        private static void BuildMiscellaneousItem(int id, string name, int price)
        {
            standardGameItems.Add(new GameItem(GameItem.ItemCategory.Miscellaneous, id, name, price));
        }

        private static void BuildWeapon(int id, string name, int price,
            int minimumDamage, int maximumDamage)
        {
            GameItem weapon = new GameItem(GameItem.ItemCategory.Weapon, id, name, price, true);

            weapon.Action = new AttackWithWeapon(weapon, minimumDamage, maximumDamage);

            standardGameItems.Add(weapon);
        }
    }
}
