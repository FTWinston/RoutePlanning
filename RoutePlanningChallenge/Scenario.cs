using System.Collections.Generic;

namespace RoutePlanningChallenge
{
    public class Scenario
    {
        public Scenario(Town town)
        {
            Town = town;
        }

        public Town Town { get; }

        private readonly List<Car> cars = new List<Car>();

        public IReadOnlyList<Car> Cars
        {
            get => cars.AsReadOnly();
            init => cars.AddRange(value);
        }

        private readonly List<Customer> customers = new List<Customer>();

        public IReadOnlyList<Customer> Customers
        {
            get => customers.AsReadOnly();
            init => customers.AddRange(value);
        }
    }
}
