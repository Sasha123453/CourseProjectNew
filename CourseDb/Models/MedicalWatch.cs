
using CourseProjectDb.Models;

namespace CourseDb.Models
{
    public class MedicalWatch
    {
        public int Id { get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public string Diagnosis { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
