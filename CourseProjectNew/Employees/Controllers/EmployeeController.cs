using CourseProjectDb;
using CourseProjectNew.Common.Extensions;
using CourseProjectNew.Common.Filters;
using CourseProjectNew.Employees.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        [HttpGet("departmentHeads")]
        public async Task<IActionResult> GetDepartmentHeads()
        {
            var departmentHeads = await _context.Departments.Select(d => d.DepartmentHead).ToListAsync();
            return Ok(departmentHeads);
        }
        [HttpGet("allEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _context.Employees.ToListAsync();
            return Ok(employees);
        }
        [HttpPost("filtered")]
        public async Task<IActionResult> GetEmployeesFiltered(Dictionary<EmployeeFilterFields, Filter> map)
        {
            var converted = ConvertMapToDictionary(map);
            var employees = await _context.Employees.FilterByDictionary(converted).ToListAsync();
            return Ok(employees);
        }
        [HttpGet("byFlight/{flightId}")]
        public async Task<IActionResult> GetEmployeesByFlight(int flightId)
        {
            var employees = await _context.Flights.Select(x => x.Brigade.Employees).ToListAsync();
            return Ok(employees);
        }
        [HttpGet("byBrigade/{brigadeId}")]
        public async Task<IActionResult> GetEmployeesByBrigade(int brigadeId, [FromQuery]int age)
        {
            var query = _context.Brigades.Where(x => x.Id == brigadeId).SelectMany(x => x.Employees);
            if (age != 0)
            {
                query = query.Where(x => x.DateOfBirth.AddYears(-age) > x.DateOfBirth);
            }
            var employees = await query.ToListAsync();
            return Ok(employees);
        }
        [HttpGet("pilotsByMedicalWatch")]
        public async Task<IActionResult> GetPilotsByMedicalWatch(Dictionary<EmployeeFilterFields, Filter> map, bool hasPassedMedicalWatch, int year)
        {
            var converted = ConvertMapToDictionary(map);
            var query = _context.Employees.Join(_context.MedicalWatches, e => e.Id, mw => mw.EmployeeId, (e, mw) => new { e, mw })
                .Where(x => x.mw.BeginTime.Year == year)
                .FilterByDictionary(converted);
            query = (hasPassedMedicalWatch) ? query.Where(x => x.mw == null) : query.Where(x => x.mw != null);
            var employees = await query.Select(x => x.e).ToListAsync();
            return Ok(employees);
        }

        private Dictionary<string, Filter> ConvertMapToDictionary(Dictionary<EmployeeFilterFields, Filter> map)
        {
            var dictionary = new Dictionary<string, Filter>();
            foreach (var key in map.Keys)
            {
                var newKey = GetKey(key);
                var filter = map[key];
                if (key == EmployeeFilterFields.WorkExperience)
                {
                    foreach (var filterValue in filter.FilterValues)
                    {
                        var years = Convert.ToInt32(filterValue.Value);
                        filterValue.Value = DateTime.Now.AddYears(-years).ToLongDateString();
                    }
                }
            }
            return dictionary;
        }
        private string GetKey(EmployeeFilterFields key)
        {
            switch (key)
            {
                case EmployeeFilterFields.Salary:
                    return "Salary";
                case EmployeeFilterFields.WorkExperience:
                    return "WorkingScince";
                case EmployeeFilterFields.Gender:
                    return "Gender";
                case EmployeeFilterFields.AmountOfKids:
                    return "AmountOfKids";
                case EmployeeFilterFields.Department:
                    return "DepartmentId";
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
