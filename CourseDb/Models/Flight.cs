using CourseDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectDb.Models
{
    public class Flight
    {
        public int Id { get; set; }
        public DaysOfWeek DepartureDays { get; set; }
        public int AirplaneId { get; set; }
        public int BrigadeId { get; set; }
        public Airplane Airplane { get; set; }
        public Brigade Brigade { get; set; }
        public FlightType FlightType { get; set; }
        public List<Ticket> Tickets { get; set; }
        public List<FlightTime> FlightTimes { get; set; }
        public FlightTime CurrentFlightTime { get; set; }
        public int FlightTypeId { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public int FlightId { get; set; }
    }

}
