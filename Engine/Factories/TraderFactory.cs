using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Models;

namespace Engine.Factories
{
    public static class TraderFactory
    {
        private static readonly List<Trader> traders = new List<Trader>();

        static TraderFactory()// TraderFactory constructor - creates 3 trader objects and adds them to the list
        {
            Trader shopkeeperSusan = new Trader("shopkeeper Susan");// First trader object, using new instantiates the object
            shopkeeperSusan.AddItemToInventory(ItemFactory.CreateGameItem(1001));

            Trader farmerTed = new Trader("Farmer Ted");// Second trader object using, new instantiates the object
            farmerTed.AddItemToInventory(ItemFactory.CreateGameItem(1001));

            Trader peteTheHerbalist = new Trader("Pete the Herbalist");// Third trader object, using new instantiates the object
            peteTheHerbalist.AddItemToInventory(ItemFactory.CreateGameItem(1001));

            AddTraderToList(shopkeeperSusan);// Add shopkeeperSusan to the traders list
            AddTraderToList(farmerTed);// Add farmerTed to the traders list
            AddTraderToList(peteTheHerbalist);// Add peteTheHerbalist to the traders list
        }
        public static Trader GetTraderByName(string name)
        {
            return traders.FirstOrDefault(t => t.Name == name);
        }

        private static void AddTraderToList(Trader trader)
        {
            if (traders.Any(t => t.Name == trader.Name))
            {
                throw new ArgumentException($"There is already a trader named '{trader.Name}");
            }

            traders.Add(trader);
        }
    }
}
