namespace CourseProjectDb.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string SecondName { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string JobTitle { get; set; }
        public DateOnly WorkingSince { get; set; }
        public string Gender { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public int AmountOfKids { get; set; }
        public decimal Salary { get; set; }
    }
}
