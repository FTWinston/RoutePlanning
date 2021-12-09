using RoutePlanningChallenge;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using Xunit.Abstractions;

namespace RoutePlanningTests
{
    public class RingTown
    {
        private readonly ITestOutputHelper output;

        public RingTown(ITestOutputHelper output)
        {
            this.output = output;
        }

        private static Scenario CreateScenario()
        {
            var a = new Location("A");
            var b = new Location("B");
            var c = new Location("C");
            var d = new Location("D");
            var e = new Location("E");
            var f = new Location("F");
            var g = new Location("G");
            var h = new Location("H");
            var i = new Location("I");
            var j = new Location("J");
            var k = new Location("K");
            var l = new Location("L");
            var m = new Location("M");
            var n = new Location("N");
            var o = new Location("O");
            var p = new Location("P");

            var town = new Town
            {
                Locations = new List<Location>
                {
                    a, b, c, d, e, f, g, h, i, j, k, l, m, n, o , p
                }
            };

            a.AddRoad(new Road(e, 2.2m));
            
            b.AddRoad(new Road(c, 2.05m));
            
            c.AddRoad(new Road(d, 1.85m));
            c.AddRoad(new Road(g, 2.15m));

            d.AddRoad(new Road(h, 2.05m));

            e.AddRoad(new Road(f, 1.75m));

            f.AddRoad(new Road(a, 3.15m));
            f.AddRoad(new Road(b, 2.2m));
            f.AddRoad(new Road(g, 1.95m));
            f.AddRoad(new Road(j, 1.85m));
            f.AddRoad(new Road(i, 2.75m));

            g.AddRoad(new Road(c, 2.10m));
            g.AddRoad(new Road(k, 2.20m));
            g.AddRoad(new Road(f, 2.15m));

            h.AddRoad(new Road(g, 1.65m));

            i.AddRoad(new Road(j, 2.30m));
            i.AddRoad(new Road(m, 1.70m));

            j.AddRoad(new Road(f, 1.95m));
            j.AddRoad(new Road(k, 2.30m));
            j.AddRoad(new Road(n, 2.05m));
            j.AddRoad(new Road(i, 1.90m));

            k.AddRoad(new Road(g, 2m));
            k.AddRoad(new Road(l, 2.25m));
            k.AddRoad(new Road(o, 1.75m));
            k.AddRoad(new Road(j, 2.25m));

            l.AddRoad(new Road(p, 2.05m));
            l.AddRoad(new Road(o, 3.20m));

            m.AddRoad(new Road(i, 2.10m));
            m.AddRoad(new Road(n, 2.00m));

            n.AddRoad(new Road(j, 2.05m));
            n.AddRoad(new Road(o, 1.80m));
            n.AddRoad(new Road(m, 1.90m));

            o.AddRoad(new Road(j, 3.05m));
            o.AddRoad(new Road(l, 3.15m));
            o.AddRoad(new Road(p, 2.05m));
            o.AddRoad(new Road(n, 2.15m));

            a.AddRoad(new Road(d, 4.25m));
            d.AddRoad(new Road(p, 4.25m));
            p.AddRoad(new Road(m, 4.25m));
            m.AddRoad(new Road(a, 4.25m));

            var fleet = new Scenario(town)
            {
                Cars = new List<Car>
                {
                    new Car("Adam", a),
                    new Car("Becca", d),
                    new Car("Charlie", k),
                    new Car("Debbie", m),
                    new Car("Ewan", f),
                }
                .ToList(),

                Customers = new List<Customer>
                {
                    new Customer("Nicole", e, h),
                    new Customer("Oliver", m, k),
                    new Customer("Penny", p, b),
                    new Customer("Quentin", h, l),
                    new Customer("Rob", l, h),
                    new Customer("Steph", o, b),
                    new Customer("Tony", e, k),
                    new Customer("Una", a, k),
                    new Customer("Victor", l, i),
                    new Customer("Wendy", g, n),
                    new Customer("Xavier", j, d),
                    new Customer("Yvonne", n, f),
                    new Customer("Zach", k, j),
                },
            };

            return fleet;
        }

        private static Scenario ResolveScenario()
        {
            var scenario = CreateScenario();

            new RoutePlanningService()
                .Resolve(scenario);

            return scenario;
        }

        [Fact]
        public void ProcessScenario()
        {
            Scenario scenario = ResolveScenario();

            var reporting = new ReportingService();

            var report = reporting.GenerateReport(scenario);

            output.WriteLine(report);

            Assert.Empty(reporting.GetUnsatisfied(scenario));
        }
    }
}
