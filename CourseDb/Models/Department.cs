namespace CourseProjectDb.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string DepartmentName { get; set; }
        public Employee DepartmentHead { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Brigade> Brigades { get; set; }
    }
}
