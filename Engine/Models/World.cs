using System.Collections.Generic;

namespace Engine.Models
{
    public class World
    {
        private List<Location> locations = new List<Location>();
        internal void AddLocation(int xCoordinate, int yCoordinate, string name, string description, string imageName)
        {
            locations.Add(new Location(xCoordinate, yCoordinate, name, description, $"/Engine;component/Images/Locations/{imageName}"));
            //loc.ImageName = $"/Engine;component/Images/Locations/{imageName}";// This way it is only in one place, not in every location in the WorldFactory.cs
        }

        public Location LocationAt(int xCoordinate, int yCoordinate)
        {
            foreach (Location loc in locations)
            {
                if (loc.XCoordinate == xCoordinate && loc.YCoordinate == yCoordinate)
                {
                    return loc;
                }
            }
            return null;
        }
    }
}
