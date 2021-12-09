using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoutePlanningChallenge
{
    public class ReportingService
    {
        public List<Customer> GetUnsatisfied(Scenario scenario)
        {
            return scenario.Customers
                .Where(customer => !customer.IsSatisfied())
                .ToList();
        }

        public decimal GetTotalDistance(Scenario scenario)
        {
            return scenario.Cars
                .Sum(car => car.DistanceTravelled);
        }

        public decimal GetLongestDistance(Scenario scenario)
        {
            return scenario.Cars
                .Max(car => car.DistanceTravelled);
        }

        public string GenerateReport(Scenario scenario)
        {
            var output = new StringBuilder();

            var unsatisfied = GetUnsatisfied(scenario);

            if (unsatisfied.Count == 0)
            {
                output.AppendLine("All customers are satisfied.");
            }
            else
            {
                output.AppendLine($"Unsatisified customers: {string.Join(", ", unsatisfied.Select(customer => customer.Name))}");
            }

            output.AppendLine($"Total distance travelled: {GetTotalDistance(scenario)}");
            output.AppendLine($"Longest distance travelled: {GetLongestDistance(scenario)}");

            foreach (var car in scenario.Cars)
            {
                output.AppendLine();
                output.AppendLine($"Car log: {car.Name}");
                output.AppendLine(car.GetTravelLog());
            }

            return output.ToString();
        }
    }
}
