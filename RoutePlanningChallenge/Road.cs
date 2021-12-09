namespace RoutePlanningChallenge
{
    public sealed class Road
    {
        public Road(Location to, decimal length)
        {
            To = to;
            Length = length;
        }

        public Location To { get; }

        public decimal Length { get; }
    }
}
