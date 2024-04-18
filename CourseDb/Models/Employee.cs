namespace CourseProjectDb.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string SecondName { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string JobTitle { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }
        public DateOnly WorkingScince { get; set; }
        public string Gender { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public int AmountOfKids { get; set; }
        public decimal Salary { get; set; }
    }
}
