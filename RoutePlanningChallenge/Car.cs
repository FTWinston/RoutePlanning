using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace RoutePlanningChallenge
{
    public sealed class Car
    {
        public Car(string name, Location location)
        {
            Name = name;
            Location = location;
        }

        public string Name { get; }

        public Location Location { get; private set; }

        private readonly List<Customer> passengers = new List<Customer>();

        public ReadOnlyCollection<Customer> Passengers { get => passengers.AsReadOnly(); }

        public decimal DistanceTravelled { get; private set; } = 0;

        private StringBuilder TravelLog { get; } = new StringBuilder();

        public void PickUp(Customer customer)
        {
            if (Location != customer.Location)
            {
                throw new ArgumentException("Cannot pick up a customer in a different location");
            }

            customer.Location = null;
            passengers.Add(customer);

            TravelLog.AppendLine($"Picked up {customer.Name} at {Location.Name}.");
        }

        public void DropOff(Customer customer)
        {
            if (!passengers.Contains(customer))
            {
                throw new ArgumentException("Cannot drop off a customer that isn't a passenger");
            }

            customer.Location = Location;
            passengers.Remove(customer);

            TravelLog.AppendLine($"Dropped off {customer.Name} at {Location.Name}.");
        }

        public void Travel(Road road)
        {
            if (!Location.Roads.Contains(road))
            {
                throw new ArgumentException("Cannot travel a road that doesn't start at car's current location");
            }

            var from = Location;

            Location = road.To;
            DistanceTravelled += road.Length;

            TravelLog.AppendLine($"Travelled from {from.Name} to {Location.Name}.");
        }

        public string GetTravelLog()
        {
            TravelLog.Append($"Total distance: {DistanceTravelled}");

            return TravelLog.ToString();
        }
    }
}
