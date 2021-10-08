using Engine.Models;

namespace Engine.Factories
{
    internal static class WorldFactory
    {
        internal static World CreateWorld()
        {
            World newWorld = new World();

            newWorld.AddLocation(-2, -1, "Farmer's Field",
                "There are rows of corn growing here, with giant rats hiding between them.",// Moved to AddLocation function- "/Engine;component/Images/Locations/FarmFields.png" 
                "FarmFields.png");// so it does not haveto be typed in each location added.  Which saves us from mistyping where to find the .png files

            newWorld.LocationAt(-2, -1).AddMonster(2, 100);// Monster 2 is rat. Chance of encountering.

            newWorld.AddLocation(-1, -1, "Farmer's House",
                "This is the hourse of your neighbor, Farmer Ted.",
                "FarmHouse.png");
            newWorld.LocationAt(-1, -1).TraderHere =
                TraderFactory.GetTraderByName("Farmer Ted");

            newWorld.AddLocation(0, -1, "Home", 
                "This is your home", 
                "Home.png");

            newWorld.AddLocation(-1, 0, "Trading Shop",
                "The shop of Susan, the shopkeeper.",
                "Trader.png");
            newWorld.LocationAt(-1, 0).TraderHere =
                TraderFactory.GetTraderByName("shopkeeper Susan");

            newWorld.AddLocation(0, 0, "Town Square",
                "You see a fountain here.",
                "TownSquare.png");

            newWorld.AddLocation(1, 0, "Town Gate",
                "There is a gate here, protecting the town from giant spiders",
                "TownGate.png");

            newWorld.AddLocation(2, 0, "Spider Forest",
                "The trees in this forest are covered with spider webs.",
                "SpiderForest.png");

            newWorld.LocationAt(2, 0).AddMonster(3, 100);// Monster 3 is GiantSpider, 100% chance.

            newWorld.AddLocation(0, 1, "Herbalist's Hut",
                "You see a small hut, with plants drying from the roof.",
                "HerbalistsHut.png");
            newWorld.LocationAt(0, 1).TraderHere =
                TraderFactory.GetTraderByName("Pete the Herbalist");

            newWorld.LocationAt(0, 1).QuestsAvailableHere.Add(QuestFactory.GetQuestByID(1));


            newWorld.AddLocation(0, 2, "Herbalist's Garden",
                "There are many plants here, with snakes hiding behind them.",
                "HerbalistsGarden.png");

            newWorld.LocationAt(0, 2).AddMonster(1, 100);// Monster 1 is Snake, 100% chance.

            return newWorld;
        }
    }
}
