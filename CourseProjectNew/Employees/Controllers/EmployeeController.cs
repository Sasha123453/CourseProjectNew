using CourseProjectDb;
using CourseProjectNew.Common.Extensions;
using CourseProjectNew.Common.Filters;
using CourseProjectNew.Employees.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace CourseProjectNew.Employees.Controllers
{
    namespace CourseProjectNew.Employees.Controllers
    {
        [Route("api/[controller]")]
        [ApiController]
        public class EmployeeController : ControllerBase
        {
            private readonly ApplicationContext _context;
            public EmployeeController(ApplicationContext context)
            {
                _context = context;
            }

            [HttpGet("allEmployees")]
            public async Task<IActionResult> GetAllEmployees()
            {
                var employees = await _context.Employees.ToListAsync();
                return Ok(employees);
            }

            [HttpGet("departmentHeads")]
            public async Task<IActionResult> GetDepartmentHeads()
            {
                var departmentHeads = await _context.Departments.Select(d => d.DepartmentHead).ToListAsync();
                return Ok(departmentHeads);
            }

            [HttpGet("byDepartment/{departmentId}")]
            public async Task<IActionResult> GetEmployeesByDepartment(int departmentId)
            {
                var employees = await _context.Departments.Where(x => x.Id == departmentId).SelectMany(x => x.Employees).ToListAsync();
                return Ok(employees);
            }

            [HttpGet("byExperience/{years}")]
            public async Task<IActionResult> GetEmployeesByExperience(int years)
            {
                var date = DateOnly.FromDateTime(DateTime.Now.AddYears(-years));
                var employees = await _context.Employees.Where(e => e.WorkingSince <= date).ToListAsync();
                return Ok(employees);
            }

            [HttpGet("byGender/{gender}")]
            public async Task<IActionResult> GetEmployeesByGender(string gender)
            {
                var employees = await _context.Employees.Where(e => e.Gender == gender).ToListAsync();
                return Ok(employees);
            }

            [HttpGet("byAge/{age}")]
            public async Task<IActionResult> GetEmployeesByAge(int age)
            {
                var date = DateOnly.FromDateTime(DateTime.Now.AddYears(-age));
                var employees = await _context.Employees.Where(e => e.DateOfBirth <= date).ToListAsync();
                return Ok(employees);
            }

            [HttpGet("byChildren/{hasChildren}")]
            public async Task<IActionResult> GetEmployeesByChildren(bool hasChildren)
            {
                var employees = hasChildren ?
                    await _context.Employees.Where(e => e.AmountOfKids > 0).ToListAsync() :
                    await _context.Employees.Where(e => e.AmountOfKids == 0).ToListAsync();
                return Ok(employees);
            }

            [HttpGet("bySalary/{salary}")]
            public async Task<IActionResult> GetEmployeesBySalary(decimal salary)
            {
                var employees = await _context.Employees.Where(e => e.Salary == salary).ToListAsync();
                return Ok(employees);
            }
            [HttpGet("byBrigade/{brigadeId}")]
            public async Task<IActionResult> GetEmployeesByBrigade(int brigadeId)
            {
                var employees = await _context
                    .Brigades
                    .Include(x => x.Employees)
                    .Where(x => x.Id == brigadeId)
                    .SelectMany(x => x.Employees)
                    .ToListAsync();
                return Ok(employees);
            }

            [HttpGet("byFlight/{flightId}")]
            public async Task<IActionResult> GetEmployeesByFlight(int flightId)
            {
                var employees = await _context
                    .Flights
                    .Where(x => x.Id == flightId)
                    .SelectMany(x => x.Brigade.Employees)
                    .ToListAsync();
                return Ok(employees);
            }
            [HttpGet("byBrigadeSalary/{brigadeId}/{salary}")]
            public async Task<IActionResult> GetEmployeesByBrigadeAndSalary(int brigadeId, decimal salary)
            {
                var employees = await _context
                    .Brigades
                    .Include(x => x.Employees)
                    .Where(x => x.Id == brigadeId)
                    .SelectMany(x => x.Employees.Where(e => e.Salary == salary))
                    .ToListAsync();
                return Ok(employees);
            }

            [HttpGet("byBrigadeAge/{brigadeId}/{age}")]
            public async Task<IActionResult> GetEmployeesByBrigadeAndAge(int brigadeId, int age)
            {
                var date = DateOnly.FromDateTime(DateTime.Now.AddYears(-age));
                var employees = await _context
                    .Brigades
                    .Include(x => x.Employees)
                    .Where(x => x.Id == brigadeId)
                    .SelectMany(x => x.Employees.Where(e => e.DateOfBirth <= date))
                    .ToListAsync();
                return Ok(employees);
            }
            [HttpGet("pilotsByMedicalCheck/{year}/{passed}")]
            public async Task<IActionResult> GetPilotsByMedicalCheck(int year, bool passed)
            {
                var pilots = await _context.Employees
                    .Where(x => x.JobTitle == "Pilot")
                    .Join(_context.MedicalWatches, x => x.Id, y => y.Employee.Id, (x, y) => new { x, y })
                    .Where(pair => pair.y.BeginTime.Date.Year == year)
                    .Where(pair => (pair.y != null) == passed)
                    .Select(d => d.x)
                    .ToListAsync();
                return Ok(pilots);
            }

            [HttpGet("pilotsByGender/{gender}")]
            public async Task<IActionResult> GetPilotsByGender(string gender)
            {
                var pilots = await _context.Employees
                    .Where(e => e.JobTitle == "Pilot" && e.Gender == gender)
                    .ToListAsync();
                return Ok(pilots);
            }

            [HttpGet("pilotsByAge/{age}")]
            public async Task<IActionResult> GetPilotsByAge(int age)
            {
                var date = DateOnly.FromDateTime(DateTime.Now.AddYears(-age));
                var pilots = await _context.Employees
                    .Where(e => e.JobTitle == "Pilot" && e.DateOfBirth <= date)
                    .ToListAsync();
                return Ok(pilots);
            }

            [HttpGet("pilotsBySalary/{salary}")]
            public async Task<IActionResult> GetPilotsBySalary(decimal salary)
            {
                var pilots = await _context.Employees
                    .Where(e => e.JobTitle == "Pilot" && e.Salary == salary)
                    .ToListAsync();
                return Ok(pilots);
            }

        }
    }
}
