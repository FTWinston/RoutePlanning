using System.Collections.Generic;

namespace RoutePlanningChallenge
{
    public sealed class Town
    {
        private readonly List<Location> locations = new List<Location>();

        public IReadOnlyList<Location> Locations
        {
            get => locations.AsReadOnly();
            init => locations.AddRange(value);
        }
    }
}
