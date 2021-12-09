using RoutePlanningChallenge;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace RoutePlanningTests
{
    public class SimpleTown
    {
        private readonly ITestOutputHelper output;

        public SimpleTown(ITestOutputHelper output)
        {
            this.output = output;
        }

        private static Scenario CreateScenario(int numberOfCars)
        {
            var supermarket = new Location("Supermarket");
            var station = new Location("Station");
            var pub = new Location("Pub");
            var office = new Location("Office");
            var home = new Location("Home");

            var town = new Town
            {
                Locations = new List<Location>
                {
                    supermarket,
                    station,
                    pub,
                    office,
                    home,
                }
            };

            supermarket.AddRoad(new Road(station, 5));
            supermarket.AddRoad(new Road(office, 8));

            station.AddRoad(new Road(home, 4));
            station.AddRoad(new Road(pub, 7));

            pub.AddRoad(new Road(home, 3));
            pub.AddRoad(new Road(station, 7));

            office.AddRoad(new Road(supermarket, 8));
            office.AddRoad(new Road(pub, 8));

            home.AddRoad(new Road(supermarket, 6));
            home.AddRoad(new Road(office, 5));

            var scenario = new Scenario(town)
            {
                Cars = new List<Car>
                {
                    new Car("Adam", supermarket),
                    new Car("Becca", station),
                    new Car("Charlie", office),
                }.Take(numberOfCars)
                .ToList(),

                Customers = new List<Customer>
                {
                    new Customer("Xavier", supermarket, pub),
                    new Customer("Yvonne", home, station),
                    new Customer("Zach", pub, office),
                },
            };

            return scenario;
        }

        private static Scenario ResolveScenario(int numberOfCars)
        {
            var scenario = CreateScenario(numberOfCars);

            new RoutePlanningService()
                .Resolve(scenario);

            return scenario;
        }

        [Fact]
        public void SingleCar()
        {
            Scenario scenario = ResolveScenario(1);

            var reporting = new ReportingService();

            var report = reporting.GenerateReport(scenario);

            output.WriteLine(report);

            Assert.Empty(reporting.GetUnsatisfied(scenario));
        }

        [Fact]
        public void MultiCar()
        {
            Scenario scenario = ResolveScenario(3);

            var reporting = new ReportingService();

            var report = reporting.GenerateReport(scenario);

            output.WriteLine(report);

            Assert.Empty(reporting.GetUnsatisfied(scenario));
        }
    }
}
