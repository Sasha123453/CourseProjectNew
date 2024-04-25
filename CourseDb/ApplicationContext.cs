using Microsoft.EntityFrameworkCore;
using CourseProjectDb.Models;
using CourseDb.Models;

namespace CourseProjectDb
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<AirplaneType> AirplaneTypes { get; set; }
        public DbSet<Airplane> Airplanes { get; set; }
        public DbSet<Brigade> Brigades { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<MedicalWatch> MedicalWatches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .HasOne(d => d.DepartmentHead)
                .WithOne()
                .HasForeignKey<Department>()
                .OnDelete(DeleteBehavior.NoAction);
            base.OnModelCreating(modelBuilder);
        }
    }
}
