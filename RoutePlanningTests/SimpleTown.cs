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

            /*
             * Example output:
            All customers are satisfied.
            Total distance travelled: 33
            Longest distance travelled: 33
    
            Car log: Adam
            Picked up Xavier at Supermarket.
            Travelled from Supermarket to Station.
            Travelled from Station to Pub.
            Dropped off Xavier at Pub.
            Picked up Zach at Pub.
            Travelled from Pub to Home.
            Picked up Yvonne at Home.
            Travelled from Home to Office.
            Dropped off Zach at Office.
            Travelled from Office to Supermarket.
            Travelled from Supermarket to Station.
            Dropped off Yvonne at Station.
            Total distance: 33
             */
        }

        [Fact]
        public void MultiCar()
        {
            Scenario scenario = ResolveScenario(3);

            var reporting = new ReportingService();

            var report = reporting.GenerateReport(scenario);

            output.WriteLine(report);

            Assert.Empty(reporting.GetUnsatisfied(scenario));

            /*
             * Example output:
            All customers are satisfied.
            Total distance travelled: 49
            Longest distance travelled: 22
    
            Car log: Adam
            Picked up Xavier at Supermarket.
            Travelled from Supermarket to Station.
            Travelled from Station to Pub.
            Dropped off Xavier at Pub.
            Total distance: 12
    
            Car log: Becca
            Travelled from Station to Pub.
            Picked up Zach at Pub.
            Travelled from Pub to Home.
            Travelled from Home to Office.
            Dropped off Zach at Office.
            Total distance: 15
    
            Car log: Charlie
            Travelled from Office to Pub.
            Travelled from Pub to Home.
            Picked up Yvonne at Home.
            Travelled from Home to Supermarket.
            Travelled from Supermarket to Station.
            Dropped off Yvonne at Station.
            Total distance: 22
             */
        }
    }
}
