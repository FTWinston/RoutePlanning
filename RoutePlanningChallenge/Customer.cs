namespace RoutePlanningChallenge
{
    public sealed class Customer
    {
        public Customer(string name, Location location, Location destination)
        {
            Name = name;
            Location = location;
            Destination = destination;
        }

        public string Name { get; }

        public Location? Location { get; set; }

        public Location Destination { get; }

        public bool IsSatisfied() { return Location == Destination; }
    }
}
