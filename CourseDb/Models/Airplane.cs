using CourseDb.Models;

namespace CourseProjectDb.Models
{
    public class Airplane
    {
        public int Id { get; set; }
        public AirplaneType AirplaneType { get; set; }
        public string Name { get; set; }
        public DateOnly ReleaseDate { get; set; }
        public AirplaneStatus AirplaneStatus { get; set; }
        public int FlightsCount { get; set; }
    }
}
