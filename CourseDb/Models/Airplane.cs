using CourseDb.Models;

namespace CourseProjectDb.Models
{
    public class Airplane
    {
        public int Id { get; set; }
        public AirplaneType AirplaneType { get; set; }
        public List<Inspection> Inspections { get; set; }
        public string Name { get; set; }
        public List<Flight> Flights { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public AirplaneStatus AirplaneStatus { get; set; }
        public int FlightsCount { get; set; }
        public int SeatsCount { get; set; }
        public TimeOnly ArrivalTime { get; set; }
        public TimeOnly DepartureTime { get; set; }
    }
}
