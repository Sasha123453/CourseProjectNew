﻿

namespace CourseDb.Models
{
    public class FlightTime
    {
        public int Id { get; set; }
        public TimeOnly DepartureTime { get; set; }
        public TimeOnly ArrivalTime { get; set; }
    }
}
