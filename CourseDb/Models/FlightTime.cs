

namespace CourseDb.Models
{
    public class FlightTime
    {
        public int Id { get; set; }
        public TimeOnly DepartureTime { get; set; }
        public TimeOnly ArrivalTime { get; set; }
        public TimeSpan TimeDuration { get; set; }
        public TimeSpan FlightDuration { get; set; }
    }
}
