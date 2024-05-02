namespace CourseDb.Models
{
    public class FlightPath
    {
        public int Id { get; set; }
        public TimeOnly ArrivalTime { get; set; }
        public TimeOnly DepartureTime { get; set; }
        public DayOfWeek DepartureDay { get; set; }
        public DayOfWeek ArrivalDay { get; set; }
        public TimeSpan Duration { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public int Order { get; set; }

    }
}
