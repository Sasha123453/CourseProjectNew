using CourseDb.Models;

namespace CourseProjectDb.Models
{
    public class Brigade
    {
        public int Id { get; set; }
        public string BrigadeName { get; set; }
        public BrigadeType BrigadeType { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
