using CourseProjectDb.Models;

namespace CourseDb.Models
{
    public class Inspection
    {
        public int Id { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public string InspectionResult { get; set; }
    }
}
