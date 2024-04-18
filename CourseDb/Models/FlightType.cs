using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectDb.Models
{
    public class FlightType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Flight> Flights { get; set; }
    }
}
