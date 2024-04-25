using CourseProjectDb.Models;

namespace CourseDb.Models
{
    public class Inspection
    {
        public int Id { get; set; }
        public Airplane Airplane { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
