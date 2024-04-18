using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseProjectDb.Models
{
    public class Passenger
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Ticket> Tickets { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public int PassportNumber { get; set; }
        public int InternationalPassport { get; set; }
    }
}
