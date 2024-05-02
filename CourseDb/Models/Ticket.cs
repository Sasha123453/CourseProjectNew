using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectDb.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int PassengerId { get; set; }
        public Passenger? Passenger { get; set; }
        public DateTime? SoldTime { get; set; }
        public TicketStatus Status { get; set; }
        public decimal Price { get; set; }
    }
}
