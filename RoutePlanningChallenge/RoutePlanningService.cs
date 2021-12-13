using System;
using System.Collections.Generic;
using System.Linq;

namespace RoutePlanningChallenge
{
    public class RoutePlanningService
    {
        class PathfindingStep
        {
            public PathfindingStep(Road road, PathfindingStep? parent, decimal distance)
            {
                Road = road;
                Parent = parent;
                TotalDistance = distance;
            }

            public Road Road { get; }

            public PathfindingStep? Parent { get; }

            public decimal TotalDistance { get; }
        }

        class RouteInfo
        {
            public RouteInfo(Road nextRoad, decimal totalDistance)
            {
                NextRoad = nextRoad;
                TotalDistance = totalDistance;
            }

            public Road NextRoad { get; }
            public decimal TotalDistance { get; }
        }
        public void Resolve(Scenario scenario)
        {
            // TODO: instruct all Cars in the scenario to Travel to, PickUp and DropOff passengers,
            // until each passenger has reached their destination.
            // Aim for all cars (combined) to travel the shortest distance possible.

            throw new NotImplementedException();
        }

        private Dictionary<Location, Dictionary<Location, RouteInfo>> DetermineBestRouteFromEachCell(Scenario scenario)
        {
            var bestRouteFromEveryCell = new Dictionary<Location, Dictionary<Location, RouteInfo>>();

            foreach (var startLocation in scenario.Town.Locations)
            {
                var routesFromThisCell = new Dictionary<Location, PathfindingStep>();

                var start = new PathfindingStep(new Road(startLocation, 0), null, 0);

                var activeSteps = new List<PathfindingStep> { start };

                while (activeSteps.Any())
                {
                    var checkStep = activeSteps.First();

                    routesFromThisCell.Add(checkStep.Road.To, checkStep);
                    activeSteps.Remove(checkStep);

                    foreach (Road possibleRoad in checkStep.Road.To.Roads)
                    {
                        if (routesFromThisCell.ContainsKey(possibleRoad.To))
                        {
                            continue;
                        }

                        var distance = checkStep.TotalDistance + possibleRoad.Length;
                        var newStep = new PathfindingStep(possibleRoad, checkStep, distance);

                        var existingStep = activeSteps.FirstOrDefault(step => step.Road.To == possibleRoad.To);
                        if (existingStep != null)
                        {
                            if (existingStep.TotalDistance > distance)
                            {
                                activeSteps.Remove(existingStep);
                                activeSteps.Add(newStep);
                            }
                        }
                        else
                        {
                            activeSteps.Add(newStep);
                        }
                    }
                }

                var bestRoutes = new Dictionary<Location, RouteInfo>();
                bestRouteFromEveryCell.Add(startLocation, bestRoutes);

                foreach (var route in routesFromThisCell)
                {
                    PathfindingStep nextStep = route.Value;
                    while (nextStep.Parent != null && nextStep.Parent != start)
                    {
                        nextStep = nextStep.Parent;
                    }

                    bestRoutes.Add(route.Key, new RouteInfo(nextStep.Road, route.Value.TotalDistance));
                }
            }

            return bestRouteFromEveryCell;
        }
    }
}
