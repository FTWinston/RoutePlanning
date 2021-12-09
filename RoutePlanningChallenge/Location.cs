using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace RoutePlanningChallenge
{
    public sealed class Location
    {
        public Location(string name)
        {
            Name = name;
        }

        public string Name { get; }

        private readonly List<Road> roads = new List<Road>();

        public ReadOnlyCollection<Road> Roads
        {
            get => roads.AsReadOnly();
        }

        public void AddRoad(Road road)
        {
            roads.Add(road);
        }
    }
}
